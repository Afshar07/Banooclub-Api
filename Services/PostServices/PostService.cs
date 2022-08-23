using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanooClub.Services.SocialMediaServices;
using BanooClub.Models.Urls;
using System.IO;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace BanooClub.Services.PostServices
{
    public class PostService : Hub<IPostService>, IPostService
    {
        private const string cacheKey = "BanooClubLastNK";

        private readonly IBanooClubEFRepository<Post> postRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<PostComment> postCommentRepository;
        private readonly IBanooClubEFRepository<PostLike> postLikeRepository;
        private readonly IBanooClubEFRepository<PostNK> postNKRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly IBanooClubEFRepository<PostReport> postReportRepository;
        private readonly IBanooClubEFRepository<UserSetting> userSettingRepository;
        private readonly IBanooClubEFRepository<ViewHistory> viewHistoryRepository;
        private readonly ISocialMediaService mediaService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService userService;
        private readonly IDistributedCache distributedCache;
        private readonly IHubContext<PostService> _hubContext;

        public PostService(IBanooClubEFRepository<Post> postRepository, IBanooClubEFRepository<Following> followingRepository,
            IBanooClubEFRepository<User> userRepository, IBanooClubEFRepository<PostComment> postCommentRepository
            , IBanooClubEFRepository<PostLike> postLikeRepository,
            IUserService userService, IBanooClubEFRepository<UserSetting> userSettingRepository,
            IHttpContextAccessor accessor,
            IDistributedCache distributedCache,
            IBanooClubEFRepository<ViewHistory> viewHistoryRepository,
            IBanooClubEFRepository<PostReport> postReportRepository,
            IBanooClubEFRepository<PostNK> postNKRepository,
            ISocialMediaService mediaService,
            IBanooClubEFRepository<SocialMedia> mediaRepository, IHubContext<PostService> hubContext)
        {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.followingRepository = followingRepository;
            this.postCommentRepository = postCommentRepository;
            this.postLikeRepository = postLikeRepository;
            this.userService = userService;
            this.postNKRepository = postNKRepository;
            this.mediaService = mediaService;
            this.postReportRepository = postReportRepository;
            this.distributedCache = distributedCache;
            this.userSettingRepository = userSettingRepository;
            this.viewHistoryRepository = viewHistoryRepository;
            this.mediaRepository = mediaRepository;
            _hubContext = hubContext;

            _accessor = accessor;
            _hubContext = hubContext;
        }
        public async Task Create(Post inputDto)
        {
            string serializedCustomerList;
            var Nks = new List<PostNK>();
            var PostNkList = await distributedCache.GetAsync(cacheKey);
            if (PostNkList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(PostNkList);
                Nks = JsonConvert.DeserializeObject<List<PostNK>>(serializedCustomerList);
            }
            else
            {
                Nks = await postNKRepository.GetAll();
                serializedCustomerList = JsonConvert.SerializeObject(Nks);
                PostNkList = Encoding.UTF8.GetBytes(serializedCustomerList);
                await distributedCache.SetAsync(cacheKey, PostNkList);
            }

            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            inputDto.UserId = userId;
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            inputDto.Status = (int)PostStatus.Published;
            inputDto.UpdateDate = DateTime.Now;

            var NKs = Nks.Select(z => z.Name).ToList();
            if (NKs.Any(z => (inputDto.Description.Contains(z))))
            {
                var textWithoutChars = Regex.Replace(inputDto.Description, "[^a-zA-Z0-9_]+", " ");

                foreach (var word in textWithoutChars.Split())
                {
                    if (NKs.Any(x => x.Equals(word)))
                    {
                        inputDto.Status = (int)PostStatus.Report;
                        break;
                    }
                }
            }
            var creation = postRepository.Insert(inputDto);

            foreach (var item in inputDto.Medias)
            {
                if (item.UploadType == (int)PostUploadType.Base64)
                {
                    if (!string.IsNullOrEmpty(item.Base64))
                    {
                        var outPut = mediaService.SaveImageNew(item.Base64, EntityUrls.PostMediaUrl, item.Priority);
                        SocialMedia dbMedia = new SocialMedia()
                        {
                            IsDeleted = false,
                            ObjectId = creation.PostId,
                            PictureUrl = outPut.ImageName,
                            Type = MediaTypes.Post,
                            Priority = item.Priority,
                            MediaId = 0,
                            UpdateDate = DateTime.Now,
                        };
                        await mediaRepository.InsertAsync(dbMedia);
                    }
                }
                else if (item.UploadType == (int)PostUploadType.Url)
                {
                    if (!string.IsNullOrEmpty(item.Base64))
                    {

                        #region save media from url
                        string galleryMediaUrl = EntityUrls.GalleryImagesMediaUrl;
                        if (item.Priority == 3)
                        {
                            galleryMediaUrl = EntityUrls.GalleryVideosMediaUrl;
                        }

                        var media = Directory.EnumerateFiles(galleryMediaUrl, item.Base64).FirstOrDefault();

                        string sourcePath = media;
                        string destPath = EntityUrls.PostMediaUrl + "/" + item.Base64;
                        if (media != null)
                        {
                            var find = Directory.EnumerateFiles(EntityUrls.PostMediaUrl, item.Base64).FirstOrDefault();
                            if (find == null)
                            {
                                File.Copy(sourcePath, destPath);
                            }
                        }
                        #endregion

                        SocialMedia dbMedia = new SocialMedia()
                        {
                            IsDeleted = false,
                            ObjectId = creation.PostId,
                            PictureUrl = item.Base64,
                            Type = MediaTypes.Post,
                            Priority = item.Priority,
                            MediaId = 0,
                            UpdateDate = DateTime.Now,
                        };
                        await mediaRepository.InsertAsync(dbMedia);

                    }
                }
            }

            await _hubContext.Clients.All.SendAsync("PostCreated", inputDto.UserId, $"Post created for user with id {creation.UserId}");
        }


        public async Task<Post> Update(Post item)
        {
            string serializedCustomerList;
            var Nks = new List<PostNK>();
            var PostNkList = await distributedCache.GetAsync(cacheKey);
            if (PostNkList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(PostNkList);
                Nks = JsonConvert.DeserializeObject<List<PostNK>>(serializedCustomerList);
            }
            else
            {
                Nks = await postNKRepository.GetAll();
                serializedCustomerList = JsonConvert.SerializeObject(Nks);
                PostNkList = Encoding.UTF8.GetBytes(serializedCustomerList);
                await distributedCache.SetAsync(cacheKey, PostNkList);
            }

            var NKs = Nks.Select(z => z.Name).ToList();
            if (NKs.Any(z => (item.Description.Contains(z))))
            {
                var textWithoutChars = Regex.Replace(item.Description, "[^a-zA-Z0-9_]+", " ");

                foreach (var word in textWithoutChars.Split())
                {
                    if (NKs.Any(x => x.Equals(word)))
                    {
                        item.Status = (int)PostStatus.Report;
                        break;
                    }
                }
            }

            await postRepository.Save();


            var dbLastMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.PostId && z.Type == MediaTypes.Post);


            foreach (var element in item.Medias)
            {
                if (element.Priority == 0)
                {
                    var photo = mediaRepository.GetQuery().FirstOrDefault(z => z.PictureUrl == element.Base64);
                    await mediaRepository.Delete(photo);
                }
                else
                {
                    if (!string.IsNullOrEmpty(element.Base64))
                    {
                        if (element.Priority == 1)
                        {
                            var LastMainMedia = mediaRepository.GetQuery().
                                FirstOrDefault(z => z.ObjectId == item.PostId && z.Type == MediaTypes.Post && z.Priority == 1);
                            if (LastMainMedia != null)
                            {
                                var outPutMain = mediaService.SaveImageNew(element.Base64, EntityUrls.PostMediaUrl, element.Priority);
                                LastMainMedia.PictureUrl = outPutMain.ImageName;
                                LastMainMedia.UpdateDate = DateTime.Now;
                                await mediaRepository.Update(LastMainMedia);
                            }
                            else
                            {
                                var outPut = mediaService.SaveImageNew(element.Base64, EntityUrls.PostMediaUrl, element.Priority);
                                SocialMedia dbMedia = new SocialMedia()
                                {
                                    IsDeleted = false,
                                    ObjectId = item.PostId,
                                    PictureUrl = outPut.ImageName,
                                    Type = MediaTypes.Post,
                                    Priority = element.Priority,
                                    MediaId = 0,
                                    UpdateDate = DateTime.Now
                                };
                                await mediaRepository.InsertAsync(dbMedia);
                            }
                        }
                        else
                        {
                            var outPut = mediaService.SaveImageNew(element.Base64, EntityUrls.PostMediaUrl, element.Priority);
                            SocialMedia dbMedia = new SocialMedia()
                            {
                                IsDeleted = false,
                                ObjectId = item.PostId,
                                PictureUrl = outPut.ImageName,
                                Type = MediaTypes.Post,
                                Priority = element.Priority,
                                MediaId = 0,
                                UpdateDate = DateTime.Now
                            };
                            await mediaRepository.InsertAsync(dbMedia);
                        }

                    }
                }
            }

            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<Post> posts = new List<Post>();
            if (pageNumber != 0 && count != 0)
            {
                posts = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.Published).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            else
            {
                posts = postRepository.GetQuery().OrderByDescending(x => x.CreateDate).ToList();
            }
            var PostCount = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.Published).Count();

            foreach (var post in posts)
            {
                //var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post && z.Priority == 1);
                //if (dbMedia != null)
                //{
                //    post.Medias = new List<FileData>();
                //    post.Medias.Add(new FileData() { Base64 = dbMedia.PictureUrl, Priority = 1 });
                //}
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post);
                if (dbMedia != null)
                {
                    post.Medias = new List<FileData>();
                    post.Medias.Add(new FileData() { Base64 = dbMedia.PictureUrl });
                }
                post.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == post.UserId);
                post.UserInfo.Password = null;
                var userMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == post.UserId && z.Type == MediaTypes.Profile);
                post.UserInfo.SelfieFileData = userMedia == null ? "" : userMedia.PictureUrl;
            }

            var obj = new
            {
                Posts = posts,
                PostCount = PostCount
            };

            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var post = postRepository.GetQuery().FirstOrDefault(z => z.PostId == id);
                await postRepository.Delete(post);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Post> Get(long id)
        {
            var post = postRepository.GetQuery().FirstOrDefault(z => z.PostId == id);

            post.UserInfo = userService.Get(post.UserId);
            post.Comments = postCommentRepository.GetQuery().Where(z => z.ParentId == 0 && z.PostId == post.PostId).ToList();
            post.Comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
            foreach (var element in post.Comments)
            {
                element.UserInfo = userService.Get(element.UserId);
            }
            var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == post.UserId && z.PostId == post.PostId);
            var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == post.UserId && z.PostId == post.PostId);
            post.IsLikedByMe = postLike == null ? false : true;
            post.IsReportedByMe = postReport == null ? false : true;
            post.CommentsCount = postCommentRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();
            post.LikesCount = postLikeRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();

            #region Media
            var dbMedia = mediaRepository.GetQuery().Where(z => z.Type == MediaTypes.Post && z.ObjectId == id).ToList();
            post.Medias = new List<FileData>();
            foreach (var media in dbMedia)
            {
                FileData mediaObj = new FileData()
                {
                    Base64 = media.PictureUrl,
                    Priority = (int)media.Priority
                };
                post.Medias.Add(mediaObj);
            }
            #endregion
            post.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == post.UserId);
            post.UserInfo.Password = null;

            return post;
        }
        public async Task<long> UpdateByCmd(string title, string description, long postId, int status)
        {
            var updateDate = DateTime.Now;
            string UpdateCmd = $"Update social.posts Set Title='{title}', Description = '{description}',Status='{status}' , UpdateDate = '{updateDate}' Where PostId={postId}";
            try
            {
                var result = await postRepository.DapperSqlQuery(UpdateCmd);
                return postId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //public object GetMyPost(int pageNumber, int count)
        //{
        //    var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
        //            ? _accessor.HttpContext.User.Identity.GetUserId()
        //            : 0;
        //    var dbPosts = postRepository.GetQuery().Where(z => z.UserId == userId).
        //        OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
        //    var PostCount = postRepository.GetQuery().Where(z => z.UserId == userId).Count();
        //    foreach (var post in dbPosts)
        //    {
        //        post.UserInfo = userService.Get(post.UserId);
        //        post.Comments = postCommentRepository.GetQuery().Where(z => z.ParentId == 0 && z.PostId == post.PostId).ToList();
        //        post.Comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
        //        foreach (var element in post.Comments)
        //        {
        //            element.UserInfo = userService.Get(element.UserId);
        //        }
        //        var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.PostId == post.PostId);
        //        var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == userId && z.PostId == post.PostId);
        //        post.IsLikedByMe = postLike == null ? false : true;
        //        post.IsReportedByMe = postReport == null ? false : true;
        //        post.CommentsCount = postCommentRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();
        //        post.LikesCount = postLikeRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();

        //        #region Media
        //        var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post);

        //        if (dbMedia != null)
        //        {
        //            post.FileData = new FileData() { Base64 = dbMedia.PictureUrl, Priority = (int)dbMedia.Priority };
        //        }
        //        #endregion
        //    }





        //    var obj = new
        //    {
        //        Posts = dbPosts,
        //        PostCount = PostCount
        //    };
        //    return obj;
        //}

        public object GetMyPost(long lastId, int count)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            List<Post> dbPosts = new List<Post>();
            dbPosts = postRepository.GetQuery().Where(x => x.UserId == userId).OrderByDescending(z => z.PostId).ToList();
            if (lastId != 0)
            {
                dbPosts = dbPosts.Where(z => z.PostId < lastId).Take(count).ToList();
            }
            else
            {
                dbPosts = dbPosts.Take(count).ToList();
            }

            var PostCount = postRepository.GetQuery().Where(z => z.UserId == userId).Count();
            foreach (var post in dbPosts)
            {
                post.UserInfo = userService.Get(post.UserId);
                post.Comments = postCommentRepository.GetQuery().Where(z => z.ParentId == 0 && z.PostId == post.PostId).ToList();
                post.Comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
                foreach (var element in post.Comments)
                {
                    element.UserInfo = userService.Get(element.UserId);
                }
                var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.PostId == post.PostId);
                var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == userId && z.PostId == post.PostId);
                post.IsLikedByMe = postLike == null ? false : true;
                post.IsReportedByMe = postReport == null ? false : true;
                post.CommentsCount = postCommentRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();
                post.LikesCount = postLikeRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();

                #region Media
                var dbMedia = mediaRepository.GetQuery().Where(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post).ToList();
                post.Medias = new List<FileData>();
                foreach (var media in dbMedia)
                {
                    FileData mediaObj = new FileData()
                    {
                        Base64 = media.PictureUrl,
                        Priority = (int)media.Priority
                    };
                    post.Medias.Add(mediaObj);
                }
                #endregion


            }

            var obj = new
            {
                Posts = dbPosts,
                PostCount = PostCount
            };
            return obj;
        }

        public IServiceResult<object> GetByUserName(string userName, long lastId, int count)
        {
            var userId = userRepository.GetQuery().FirstOrDefault(z => z.UserName == userName).UserId;
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivatePost;
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            if (IsPrivate == false)
            {
                var dbPosts = postRepository.GetQuery().Where(z => z.UserId == userId).ToList();
                if (lastId != 0)
                {
                    dbPosts = dbPosts.Where(z => z.PostId < lastId).OrderByDescending(x => x.PostId).Take(count).ToList();
                }
                else
                {
                    dbPosts = dbPosts.OrderByDescending(x => x.PostId).Take(count).ToList();
                }

                foreach (var post in dbPosts)
                {
                    post.Medias = new List<FileData>();
                    post.UserInfo = userService.Get(post.UserId);
                    post.Comments = postCommentRepository.GetQuery().Where(z => z.ParentId == 0 && z.PostId == post.PostId).ToList();
                    post.Comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
                    foreach (var element in post.Comments)
                    {
                        element.UserInfo = userService.Get(element.UserId);
                    }
                    if (MYselfId > 0)
                    {
                        var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId && z.PostId == post.PostId);
                        var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == MYselfId && z.PostId == post.PostId);
                        post.IsLikedByMe = postLike == null ? false : true;
                        post.IsReportedByMe = postReport == null ? false : true;
                    }
                    post.CommentsCount = postCommentRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();
                    post.LikesCount = postLikeRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();


                    var dbMedias = mediaRepository.GetQuery().Where(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post).ToList();
                    foreach (var media in dbMedias)
                    {
                        post.Medias.Add(new FileData() { Base64 = media.PictureUrl, Priority = media.Priority, UploadType = 1 });
                    }
                }
                var obj = new
                {
                    Status = (int)PostVisibility.VisibleAndPublic,
                    Posts = dbPosts
                };
                return new ServiceResult<object>().Ok(obj);
            }
            else
            {
                if (MYselfId > 0)
                {
                    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                    if (dbFollowing != null)
                    {
                        var dbPosts = postRepository.GetQuery().Where(z => z.UserId == userId).ToList();
                        if (lastId != 0)
                        {
                            dbPosts = dbPosts.Where(z => z.PostId < lastId).OrderByDescending(x => x.PostId).Take(count).ToList();
                        }
                        else
                        {
                            dbPosts = dbPosts.OrderByDescending(x => x.PostId).Take(count).ToList();
                        }

                        foreach (var post in dbPosts)
                        {
                            post.Medias = new List<FileData>();
                            post.UserInfo = userService.Get(post.UserId);
                            post.Comments = postCommentRepository.GetQuery().Where(z => z.ParentId == 0 && z.PostId == post.PostId).ToList();
                            post.Comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
                            foreach (var element in post.Comments)
                            {
                                element.UserInfo = userService.Get(element.UserId);
                            }
                            if (MYselfId > 0)
                            {
                                var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId && z.PostId == post.PostId);
                                var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == MYselfId && z.PostId == post.PostId);
                                post.IsLikedByMe = postLike == null ? false : true;
                                post.IsReportedByMe = postReport == null ? false : true;
                            }


                            var dbMedias = mediaRepository.GetQuery().Where(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post).ToList();
                            foreach (var media in dbMedias)
                            {
                                post.Medias.Add(new FileData() { Base64 = media.PictureUrl, Priority = media.Priority, UploadType = 1 });
                            }

                        }
                        var obj1 = new
                        {
                            Status = (int)PostVisibility.Visible,
                            Posts = dbPosts
                        };
                        return new ServiceResult<object>().Ok(obj1);
                    }
                    else
                    {
                        var obj2 = new
                        {
                            Status = (int)PostVisibility.PrivateAndNotFollowing,
                            Posts = ""
                        };
                        return new ServiceResult<object>().Ok(obj2);
                    }
                }

                var obj3 = new
                {
                    Status = (int)PostVisibility.PrivateAndNotLogged,
                    Posts = ""
                };
                return new ServiceResult<object>().Ok(obj3);

            }
        }

        public IServiceResult<object> GetByUserId(long userId, long lastId, int count)
        {
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivatePost;
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            if (IsPrivate == false)
            {
                var dbPosts = postRepository.GetQuery().Where(z => z.UserId == userId).ToList();
                if (lastId != 0)
                {
                    dbPosts = dbPosts.Where(z => z.PostId < lastId).OrderByDescending(x => x.PostId).Take(count).ToList();
                }
                else
                {
                    dbPosts = dbPosts.OrderByDescending(x => x.PostId).Take(count).ToList();
                }

                foreach (var post in dbPosts)
                {
                    post.Medias = new List<FileData>();
                    post.UserInfo = userService.Get(post.UserId);
                    post.Comments = postCommentRepository.GetQuery().Where(z => z.ParentId == 0 && z.PostId == post.PostId).ToList();
                    post.Comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
                    foreach (var element in post.Comments)
                    {
                        element.UserInfo = userService.Get(element.UserId);
                    }
                    if (MYselfId > 0)
                    {
                        var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId && z.PostId == post.PostId);
                        var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == MYselfId && z.PostId == post.PostId);
                        post.IsLikedByMe = postLike == null ? false : true;
                        post.IsReportedByMe = postReport == null ? false : true;
                    }
                    post.CommentsCount = postCommentRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();
                    post.LikesCount = postLikeRepository.GetQuery().Where(z => z.PostId == post.PostId).Count();


                    var dbMedias = mediaRepository.GetQuery().Where(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post).ToList();
                    foreach (var media in dbMedias)
                    {
                        post.Medias.Add(new FileData() { Base64 = media.PictureUrl, Priority = media.Priority, UploadType = 1 });
                    }
                }
                var obj = new
                {
                    Status = (int)PostVisibility.VisibleAndPublic,
                    Posts = dbPosts
                };
                return new ServiceResult<object>().Ok(obj);
            }
            else
            {
                if (MYselfId > 0)
                {
                    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                    if (dbFollowing != null)
                    {
                        var dbPosts = postRepository.GetQuery().Where(z => z.UserId == userId).ToList();
                        if (lastId != 0)
                        {
                            dbPosts = dbPosts.Where(z => z.PostId < lastId).OrderByDescending(x => x.PostId).Take(count).ToList();
                        }
                        else
                        {
                            dbPosts = dbPosts.OrderByDescending(x => x.PostId).Take(count).ToList();
                        }

                        foreach (var post in dbPosts)
                        {
                            post.Medias = new List<FileData>();
                            post.UserInfo = userService.Get(post.UserId);
                            post.Comments = postCommentRepository.GetQuery().Where(z => z.ParentId == 0 && z.PostId == post.PostId).ToList();
                            post.Comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
                            foreach (var element in post.Comments)
                            {
                                element.UserInfo = userService.Get(element.UserId);
                            }
                            if (MYselfId > 0)
                            {
                                var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId && z.PostId == post.PostId);
                                var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == MYselfId && z.PostId == post.PostId);
                                post.IsLikedByMe = postLike == null ? false : true;
                                post.IsReportedByMe = postReport == null ? false : true;
                            }


                            var dbMedias = mediaRepository.GetQuery().Where(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post).ToList();
                            foreach (var media in dbMedias)
                            {
                                post.Medias.Add(new FileData() { Base64 = media.PictureUrl, Priority = media.Priority, UploadType = 1 });
                            }

                        }
                        var obj1 = new
                        {
                            Status = (int)PostVisibility.Visible,
                            Posts = dbPosts
                        };
                        return new ServiceResult<object>().Ok(obj1);
                    }
                    else
                    {
                        var obj2 = new
                        {
                            Status = (int)PostVisibility.PrivateAndNotFollowing,
                            Posts = ""
                        };
                        return new ServiceResult<object>().Ok(obj2);
                    }
                }

                var obj3 = new
                {
                    Status = (int)PostVisibility.PrivateAndNotLogged,
                    Posts = ""
                };
                return new ServiceResult<object>().Ok(obj3);

            }


        }
        public async Task<object> GetFollowingPosts(long lastId, int count)
        {
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbFollowings = followingRepository.GetQuery().Where(z => z.UserId == MYselfId).Select(z => z.FollowingUserId).ToList();
            dbFollowings.Add(MYselfId);
            var dbPosts = postRepository.GetQuery().Where(z => dbFollowings.Contains(z.UserId) && z.Status == (int)PostStatus.Published).OrderByDescending(x => x.PostId).ToList();
            var postCount = dbPosts.Count();
            if (lastId != 0)
            {
                dbPosts = dbPosts.Where(z => z.PostId < lastId).OrderByDescending(x => x.PostId).Take(count).ToList();
            }
            else
            {
                dbPosts = dbPosts.Take(count).ToList();
            }
            foreach (var item in dbPosts)
            {
                var dbPostMedia = mediaRepository.GetQuery().Where(z => z.ObjectId == item.PostId && z.Type == MediaTypes.Post).ToList();
                item.Medias = new List<FileData>();
                foreach (var media in dbPostMedia)
                {
                    item.Medias.Add(new FileData() { Base64 = media.PictureUrl, Priority = media.Priority, UploadType = 0 });
                }
                var dbUserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == item.UserId);
                dbUserInfo.Password = null;
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == dbUserInfo.UserId && z.Type == MediaTypes.Profile);
                dbUserInfo.SelfieFileData = dbUserMedia == null ? "" : dbUserMedia.PictureUrl;
                var likes = postLikeRepository.GetQuery().Where(z => z.PostId == item.PostId).Count();
                var comments = postCommentRepository.GetQuery().Where(z => z.PostId == item.PostId).ToList();
                foreach (var cmnt in comments)
                {
                    cmnt.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == cmnt.UserId);
                    cmnt.UserInfo.Password = null;
                    var dbCmntUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.Profile && z.ObjectId == cmnt.UserId);
                    cmnt.UserInfo.SelfieFileData = dbCmntUserMedia == null ? "" : dbCmntUserMedia.PictureUrl;
                }
                item.UserInfo = dbUserInfo;
                item.LikesCount = likes;
                item.Comments = comments;
                var dbLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId && z.PostId == item.PostId);
                var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == MYselfId && z.PostId == item.PostId);
                item.IsLikedByMe = false;
                item.IsReportedByMe = postReport == null ? false : true;

                if (dbLike != null)
                {
                    item.IsLikedByMe = true;
                }
            }
            var obj = new
            {
                Posts = dbPosts,
                PostCount = postCount
            };

            return obj;
        }
        //public  async Task<List<PostDTO>> GetFollowingPosts()
        //{
        //    var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
        //            ? _accessor.HttpContext.User.Identity.GetUserId()
        //            : 0;
        //    string Cmd = "declare @A bigint select @A = (select distinct Count(P.PostId) from Social.Posts as P " +
        //      " left join[User].Users as U on P.UserId = U.UserId" +
        //      " left join Social.Followings as F on P.UserId = F.FollowingUserId" +
        //      " left join Social.PostLikes as L on L.PostId = P.PostId" +
        //      " left join social.PostComments as C on C.PostId = P.PostId" +
        //      $" left join social.ViewHistories as VH on VH.userId = {userId}" +
        //      $" where (F.UserId = {userId} or P.UserId={userId}) And P.Status = 1 And P.IsDeleted = 0 And P.PostId > VH.FirstPostId)" +
        //      " if (@A > 9)" +
        //      "  begin" +
        //      "  select distinct top 10  U.UserId, (select count(*) from social.PostLikes where PostId = p.PostId) as LikesCount," +
        //      " (select count(*) from social.PostComments where PostId = p.PostId) as CommentsCount," +
        //      " U.Name,U.FamilyName,U.Status,P.Content,P.PostId,P.CreateDate,P.UpdateDate from" +
        //      " Social.Posts as P " +
        //      " left join[User].Users as U on P.UserId = U.UserId" +
        //      " left join Social.Followings as F on P.UserId = F.FollowingUserId" +
        //      " left join Social.PostLikes as L on L.PostId = P.PostId" +
        //      " left join social.PostComments as C on C.PostId = P.PostId" +
        //      $" left join social.ViewHistories as VH on VH.userId = {userId}" +
        //      $" where (F.UserId = {userId} or P.UserId={userId}) And P.Status = 1 And P.IsDeleted = 0 And P.PostId > VH.FirstPostId  order by  P.CreateDate asc" +
        //      " End" +
        //      " else if (@A < 10)" +
        //      "  begin" +
        //      "  select Distinct top 10 U.UserId, (select count(*) from social.PostLikes where PostId = p.PostId) as LikesCount," +
        //      " (select count(*) from social.PostComments where PostId = p.PostId) as CommentsCount," +
        //      " U.Name,U.FamilyName,U.Status,P.Content,P.PostId,P.CreateDate,P.UpdateDate from" +
        //      " Social.Posts as P " +
        //      " left join[User].Users as U on P.UserId = U.UserId" +
        //      " left join Social.Followings as F on P.UserId = F.FollowingUserId" +
        //      " left join Social.PostLikes as L on L.PostId = P.PostId" +
        //      " left join social.PostComments as C on C.PostId = P.PostId" +
        //      $" left join social.ViewHistories as VH on VH.userId = {userId}" +
        //      $" where (F.UserId = {userId} or P.UserId={userId}) And P.Status = 1 And P.IsDeleted = 0 And(P.PostId > VH.FirstPostId or P.PostId < VH.LastPostId) order by  P.CreateDate Desc" +
        //      " End";
        //    var result = postRepository.SqlObj(Cmd);
        //    var dbViewHistory = viewHistoryRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);

        //    if (result.Count == 0)
        //    {
        //        dbViewHistory.LastPostId = 0;
        //        dbViewHistory.FirstPostId = 0;
        //        dbViewHistory.UpdateDate= DateTime.Now;
        //        var updated =await viewHistoryRepository.Update(dbViewHistory);
        //        result=postRepository.SqlObj(Cmd);
        //    }

        //    if (result.Count > 0)
        //    {
        //        var Descending = result.OrderByDescending(z => z.PostId).ToList();
        //        long Last=Descending.LastOrDefault().PostId;
        //        long First=Descending.FirstOrDefault().PostId;
        //        if (dbViewHistory.LastPostId > Last || dbViewHistory.LastPostId == 0)
        //        {
        //            dbViewHistory.LastPostId = Last;
        //        }
        //        if (dbViewHistory.FirstPostId < First)
        //        {
        //            dbViewHistory.FirstPostId = First;
        //        }
        //        dbViewHistory.UpdateDate = DateTime.Now;
        //        await viewHistoryRepository.Update(dbViewHistory);
        //    }
        //    foreach (var item in result)
        //    {
        //        item.Comments = new List<PostComment>();
        //        item.Comments = postCommentRepository.GetQuery().Where(z => z.PostId == item.PostId).ToList();
        //        item.UserInfo = userService.Get(item.UserId);
        //        foreach (var element in item.Comments)
        //        {
        //            element.UserInfo = userService.Get(element.UserId);
        //        }
        //        var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.PostId == item.PostId);
        //        var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == userId && z.PostId == item.PostId);
        //        item.IsLikedByMe = postLike == null ? false : true;
        //        item.IsReportedByMe = postReport == null ? false : true;


        //        var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.PostId && z.Type == MediaTypes.Post && z.Priority == 1);
        //        if (dbMedia != null)
        //        {
        //            item.Medias = new List<FileData>();
        //            item.Medias.Add(new FileData() { Base64 = dbMedia.PictureUrl, Priority = 1 });
        //        }
        //    }
        //    return result;
        //}
        public object GetNotConfirmed(int pageNumber, int count, string search)
        {
            List<Post> posts = new List<Post>();
            posts = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.NotConfirmed && z.Description.Contains(search)).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            var PostCount = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.NotConfirmed && z.Description.Contains(search)).Count();
            posts.ForEach(z => z.UserInfo = userService.Get(z.UserId));
            foreach (var post in posts)
            {
                post.Medias = new List<FileData>();
                var dbMedias = mediaRepository.GetQuery().Where(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post).ToList();
                foreach (var media in dbMedias)
                {
                    post.Medias.Add(new FileData() { Base64 = media.PictureUrl, Priority = media.Priority, UploadType = 1 });
                }
            }
            var obj = new
            {
                Posts = posts,
                PostCount = PostCount,
            };
            return obj;
        }
        public object GetReported(int pageNumber, int count)
        {
            List<Post> posts = new List<Post>();
            posts = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.Report).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            posts.ForEach(z => z.UserInfo = userService.Get(z.UserId));
            var PostCount = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.Report).Count();


            foreach (var post in posts)
            {
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post && z.Priority == 1);
                if (dbMedia != null)
                {
                    post.Medias = new List<FileData>();
                    post.Medias.Add(new FileData() { Base64 = dbMedia.PictureUrl, Priority = 1 });
                }
            }

            var obj = new
            {
                Posts = posts,
                PostCount = PostCount,
            };
            return obj;
        }
        public async Task<bool> ChangePostStatusForAdmin(long postId, PostStatus status)
        {
            try
            {
                var dbPost = postRepository.GetQuery().FirstOrDefault(z => z.PostId == postId);
                dbPost.Status = (int)status;
                await postRepository.Update(dbPost);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Post> ChangePostStatus(Post item)
        {
            var dbPost = postRepository.GetQuery().FirstOrDefault(z => z.PostId == item.PostId);
            if (item.Status == (int)PostStatus.Published)
            {
                dbPost.Status = (int)PostStatus.Published;
                dbPost.Description = item.Description == null ? dbPost.Description : item.Description;
            }
            else if (item.Status == (int)PostStatus.Report)
            {
                dbPost.Status = (int)PostStatus.Report;
                dbPost.Description = item.Description == null ? dbPost.Description : item.Description;

                var adminId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
                var dbPostReportCreation = new PostReport
                {
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    IsConfirmed = true,
                    Reason = "Negative Keyword",
                    PostId = item.PostId,
                    PostReportId = 0,
                    ReporterUserId = adminId,
                    UserId = item.UserId
                };
                postReportRepository.Insert(dbPostReportCreation);
            }
            await postRepository.Update(dbPost);



            #region Media
            var dbMedia = mediaRepository.GetQuery().Where(z => z.Type == MediaTypes.Post && z.ObjectId == item.PostId).ToList();
            dbPost.Medias = new List<FileData>();
            foreach (var media in dbMedia)
            {
                FileData mediaObj = new FileData()
                {
                    Base64 = media.PictureUrl,
                    Priority = (int)media.Priority
                };
                dbPost.Medias.Add(mediaObj);
            }
            #endregion

            return dbPost;
        }

        public object GetAllByInfiniteScroll(long lastId, int count, string search)
        {
            if (count == 0)
            {
                count = 10;
            }

            var completationCmd = lastId == 0 ? "" : $"and P.PostId < {lastId}";
            string cmd = "select P.PostId , P.UserId , P.Title , P.Description , P.CreateDate, P.UpdateDate, P.IsDeleted, P.Status " +
            "  from [Social].[Posts] P " +
            //"  left join Social.Medias SM on SM.ObjectId = P.PostId and SM.Type = 16" +
            $"  where(P.Title like N'%{search}%' or P.Description like N'%{search}%') and P.IsDeleted = 0 {completationCmd} " +
            $"  order by P.PostId Desc OFFSET 0 ROWS FETCH NEXT {count} ROWS ONLY ";
            var DeSerializeObj = postRepository.DapperSqlQuery(cmd).Result;

            var objSer = System.Text.Json.JsonSerializer.Serialize<object>(DeSerializeObj);
            List<Post> posts = System.Text.Json.JsonSerializer.Deserialize<List<Post>>(objSer);

            foreach (var post in posts)
            {

                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == post.PostId && z.Type == MediaTypes.Post);
                if (dbMedia != null)
                {
                    post.Medias = new List<FileData>();
                    post.Medias.Add(new FileData() { Base64 = dbMedia.PictureUrl });
                }
            }


            return posts;
        }
    }
}
