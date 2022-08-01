using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.Common;
using BanooClub.Services.CryptographyServices;
using BanooClub.Services.RatingServices;
using BanooClub.Services.SocialMediaServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BanooClub.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<UserSetting> userSettingRepository;
        private readonly IBanooClubEFRepository<Follower> followerRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        private readonly IBanooClubEFRepository<Activity> _activityRepository;
        private readonly IBanooClubEFRepository<FollowRequest> _followRequestRepository;
        private readonly IBanooClubEFRepository<Post> postRepository;
        private readonly IBanooClubEFRepository<PostLike> postLikeRepository;
        private readonly IBanooClubEFRepository<PostComment> postCommentRepository;
        private readonly IBanooClubEFRepository<Rating> ratingRepository;
        private readonly IBanooClubEFRepository<Forum> forumRepository;
        private readonly IBanooClubEFRepository<ForumComment> forumCommentRepository;
        private readonly IBanooClubEFRepository<ServicePack> servicePackRepository;
        private readonly IBanooClubEFRepository<Order> orderRepository;
        private readonly IBanooClubEFRepository<OrderItem> orderItemRepository;
        private readonly IBanooClubEFRepository<Payment> paymentRepository;
        private readonly ISocialMediaService _mediaService;
        private readonly IRatingService ratingService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEncryptService _encryptService;

        public UserService(IBanooClubEFRepository<User> userRepository,
            IBanooClubEFRepository<UserSetting> userSettingRepository,
            IBanooClubEFRepository<Follower> followerRepository,
            IBanooClubEFRepository<Following> followingRepository,
            IBanooClubEFRepository<SocialMedia> mediaRepository,
            IBanooClubEFRepository<Activity> activityRepository,
            IBanooClubEFRepository<FollowRequest> followRequestRepository,
            IBanooClubEFRepository<Post> postRepository,
            IBanooClubEFRepository<PostLike> postLikeRepository,
            IBanooClubEFRepository<PostComment> postCommentRepository,
            IBanooClubEFRepository<Rating> ratingRepository,
            IBanooClubEFRepository<Forum> forumRepository,
            IBanooClubEFRepository<ForumComment> forumCommentRepository,
            IBanooClubEFRepository<ServicePack> servicePackRepository,
            IBanooClubEFRepository<Order> orderRepository,
            IBanooClubEFRepository<OrderItem> orderItemRepository,
            IBanooClubEFRepository<Payment> paymentRepository,
            IHttpContextAccessor accessor,
            ISocialMediaService socialMediaService,
            IRatingService ratingService,
            IEncryptService encryptService)
        {
            this.followerRepository = followerRepository;
            this.followingRepository = followingRepository;
            this.userRepository = userRepository;
            this.postLikeRepository = postLikeRepository;
            this.postCommentRepository = postCommentRepository;
            this.ratingRepository = ratingRepository;
            this.forumRepository = forumRepository;
            this.forumCommentRepository = forumCommentRepository;
            this.servicePackRepository = servicePackRepository;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.paymentRepository = paymentRepository;
            _activityRepository = activityRepository;
            this.userSettingRepository = userSettingRepository;
            _mediaRepository = mediaRepository;
            _mediaService = socialMediaService;
            this.ratingService = ratingService;
            _accessor = accessor;
            _encryptService = encryptService;
            _followRequestRepository = followRequestRepository;
            this.postRepository = postRepository;
            
        }
        public async Task Create(User inputDto)
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$");
            Match match = regex.Match(inputDto.Password);
            if (match.Success)
            {
                inputDto.Password = _encryptService.Encrypt(inputDto.Password).Data;
            }
            else
            {
                throw new Exception("Password format not correct");
            }
            userRepository.Insert(inputDto);
            await userRepository.Save();
        }
        public async Task<bool> UploadMediaGallery(FileData fileData)
        {

            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var url = EntityUrls.GalleryImagesMediaUrl;
            if (fileData.Priority == 3)
            {
                url = EntityUrls.GalleryVideosMediaUrl;
            }
            var outPut = new OutPutSaveImage
            {
                ImageName="",
                IsSuccess = false
            };

            #region DELETE Media

            var dbLastMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.Post);
            if (fileData.Base64.ToUpper() == "DELETE")
            {
                dbLastMedia.PictureUrl = "";
                await _mediaRepository.Update(dbLastMedia);
                await _mediaRepository.Delete(dbLastMedia);
            }
            else if (!string.IsNullOrEmpty(fileData.Base64))
            {
                outPut= _mediaService.SaveImageNew(fileData.Base64, url, fileData.Priority);
                if (dbLastMedia != null)
                {
                    dbLastMedia.PictureUrl = outPut.ImageName;
                    await _mediaRepository.Update(dbLastMedia);
                }
                else
                {
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = userId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Post,
                        MediaId = 0,
                        Priority = fileData.Priority
                    };
                    await _mediaRepository.InsertAsync(dbMedia);
                }
            }

            #endregion


            if (outPut.IsSuccess)
            {
                SocialMedia dbMedia = new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = userId,
                    PictureUrl = outPut.ImageName,
                    Type = MediaTypes.GalleryImages,
                    MediaId = 0,
                    UpdateDate = DateTime.Now,
                    Priority=fileData.Priority
                };
                await _mediaRepository.InsertAsync(dbMedia);
                return true;
            }
            return false;

        }
        public async Task<object> GetMyMediaGallery()
        {

            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;
            var dbMedia = _mediaRepository.GetQuery().Where(z => (z.Type == MediaTypes.GalleryImages || z.Type==MediaTypes.GalleryVideos) && z.ObjectId == userId).ToList();
            List<FileData> result = new List<FileData>();
            foreach (var media in dbMedia)
            {
                FileData mediaObj = new FileData()
                {
                    Base64 = media.PictureUrl,
                    Priority = media.Priority
                };
                result.Add(mediaObj);
            }
            return result;
        }
        public async Task<IServiceResult<User>> Update(User item)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);


            var dbLastBanner = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.Banner);

            if (item.BannerFileData == "Delete")
            {
                if (dbLastBanner != null)
                {
                    dbLastBanner.PictureUrl = "";
                    await _mediaRepository.Update(dbLastBanner);
                    await _mediaRepository.Delete(dbLastBanner);
                }

            }
            else if (!string.IsNullOrEmpty(item.BannerFileData))
            {
                var outPut = _mediaService.SaveImage(item.BannerFileData, EntityUrls.BannerMediaUrl);
                if (outPut.IsSuccess)
                {
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = userId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Banner,
                        MediaId = 0,
                        UpdateDate =DateTime.Now
                    };

                    if (dbLastBanner != null)
                    {
                        dbLastBanner.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLastBanner);
                        dbUser.BannerFileData = outPut.ImageName;
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                        dbUser.BannerFileData = outPut.ImageName;
                    }
                }
            }

            var dbLastSelfie = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.Profile);

            if (item.SelfieFileData == "Delete")
            {
                if (dbLastSelfie != null)
                {
                    dbLastSelfie.PictureUrl = "";
                    await _mediaRepository.Update(dbLastSelfie);
                    await _mediaRepository.Delete(dbLastSelfie);
                }
            }
            else if (!string.IsNullOrEmpty(item.SelfieFileData))
            {
                var outPut = _mediaService.SaveImage(item.SelfieFileData, EntityUrls.ProfileMediaUrl);
                if (outPut.IsSuccess)
                {
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = userId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Profile,
                        MediaId = 0,
                        UpdateDate = DateTime.Now
                    };

                    if (dbLastSelfie != null)
                    {
                        dbLastSelfie.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLastSelfie);
                        dbUser.SelfieFileData = outPut.ImageName;
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                        dbUser.SelfieFileData = outPut.ImageName;
                    }
                }

            }


            dbUser.Name = string.IsNullOrEmpty(item.Name) ? dbUser.Name : item.Name;
            dbUser.Email = string.IsNullOrEmpty(item.Email) ? dbUser.Email : item.Email;
            dbUser.Mobile = string.IsNullOrEmpty(item.Mobile) ? dbUser.Mobile : item.Mobile;
            dbUser.FamilyName = string.IsNullOrEmpty(item.FamilyName) ? dbUser.FamilyName : item.FamilyName;
            dbUser.CityId = item.CityId == null ? dbUser.CityId : item.CityId;
            dbUser.StateId = item.StateId == null ? dbUser.StateId : item.StateId;
            dbUser.RelationState =  item.RelationState == null ? dbUser.RelationState : item.RelationState;

            if (!string.IsNullOrEmpty(item.Password))
            {
                var dbCurrentFromUser = _encryptService.Encrypt(item.CurrentPassword).Data;
                if (dbCurrentFromUser == dbUser.Password)
                {
                    Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$");
                    Match match = regex.Match(item.Password);
                    if (match.Success)
                    {
                        dbUser.Password = _encryptService.Encrypt(item.Password).Data;
                    }
                    else
                    {
                        return new ServiceResult<User>().Failure("Password format not matched");
                    }
                }
                else
                {
                    return new ServiceResult<User>().Failure("Password Is not correct");
                }
            }
            dbUser.Status=item.Status ==0 ? dbUser.Status : item.Status;
            await userRepository.Update(dbUser);


            _activityRepository.Insert(new Activity()
            {
                ActivityId=0,
                CreateDate=DateTime.Now,
                IsDeleted=false,
                ObjectId=userId,
                Type=ActivityTypes.UpdateProfile,
                UserId=userId
            });


            dbUser.Password = null;
            return new ServiceResult<User>().Ok(dbUser);
        }
        public async Task<bool> UploadPhotoGallery(FileData fileData)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var outPut = _mediaService.SaveImage(fileData.Base64, EntityUrls.GalleryImagesMediaUrl);
            if (outPut.IsSuccess)
            {
                SocialMedia dbMedia = new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = userId,
                    PictureUrl = outPut.ImageName,
                    Type = MediaTypes.GalleryImages,
                    MediaId = 0,
                    UpdateDate =DateTime.Now
                };
                await _mediaRepository.InsertAsync(dbMedia);
                return true;
            }
            return false;
        }



        public async Task<bool> DeleteMedia(string mediaName)
        {
            try
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.PictureUrl == mediaName);
                if (dbMedia == null)
                {
                    return false;
                }
                else
                {
                    await _mediaRepository.Delete(dbMedia);
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<string> GetMyPhotos()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var medias=_mediaRepository.GetQuery().Where(z=>z.Type==MediaTypes.GalleryImages && z.ObjectId==userId).Select(x=>x.PictureUrl).ToList();
            List<string> result = new List<string>();
            foreach(var media in medias)
            {
                var NEWMedia=media;
                result.Add(NEWMedia);
            }
            return result;
        }

        public List<string> GetMyVideos()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var medias = _mediaRepository.GetQuery().Where(z => z.Type==MediaTypes.GalleryVideos && z.ObjectId==userId).Select(x => x.PictureUrl).ToList();
            List<string> result = new List<string>();
            foreach (var media in medias)
            {
                var NEWMedia = media;
                result.Add(NEWMedia);
            }
            return result;
        }

        public IServiceResult<object> GetPhotosByUserId(long userId)
        {
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateSocial;
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            if (IsPrivate == false)
            {
                var medias = _mediaRepository.GetQuery().Where(z => z.Type==MediaTypes.GalleryImages && z.ObjectId==userId).Select(x => x.PictureUrl).ToList();
                List<string> result = new List<string>();
                foreach (var media in medias)
                {
                    var NEWMedia = media;
                    result.Add(NEWMedia);
                }
                var obj = new
                {
                    Status = (int)PostVisibility.Visible,
                    Medias = result
                };
                return new ServiceResult<object>().Ok(obj);
            }
            else
            {
                if (MYselfId > 0)
                {
                    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId & z.FollowingUserId == MYselfId);
                    if (dbFollowing != null)
                    {
                        var medias = _mediaRepository.GetQuery().Where(z => z.Type==MediaTypes.GalleryImages && z.ObjectId==userId).Select(x => x.PictureUrl).ToList();
                        List<string> result = new List<string>();
                        foreach (var media in medias)
                        {
                            var NEWMedia = media;
                            result.Add(NEWMedia);
                        }
                        var obj = new
                        {
                            Status = (int)PostVisibility.Visible,
                            Medias = result
                        };
                        return new ServiceResult<object>().Ok(obj);
                    }
                    else
                    {
                        var obj2 = new
                        {
                            Status = (int)PostVisibility.PrivateAndNotFollowing,
                            Medias = ""
                        };
                        return new ServiceResult<object>().Ok(obj2);
                    }
                }

                var obj3 = new
                {
                    Status = (int)PostVisibility.PrivateAndNotLogged,
                    Medias = ""
                };
                return new ServiceResult<object>().Ok(obj3);

            }
        }

        public IServiceResult<object> GetVideosByUserId(long userId)
        {
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateSocial;
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            if (IsPrivate == false)
            {
                var medias = _mediaRepository.GetQuery().Where(z => z.Type==MediaTypes.GalleryVideos && z.ObjectId==userId).Select(x => x.PictureUrl).ToList();
                List<string> result = new List<string>();
                foreach (var media in medias)
                {
                    var NEWMedia = media;
                    result.Add(NEWMedia);
                }
                var obj = new
                {
                    Status = (int)PostVisibility.Visible,
                    Medias = result
                };
                return new ServiceResult<object>().Ok(obj);
            }
            else
            {
                if (MYselfId > 0)
                {
                    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId & z.FollowingUserId == MYselfId);
                    if (dbFollowing != null)
                    {
                        var medias = _mediaRepository.GetQuery().Where(z => z.Type==MediaTypes.GalleryVideos && z.ObjectId==userId).Select(x => x.PictureUrl).ToList();
                        List<string> result = new List<string>();
                        foreach (var media in medias)
                        {
                            var NEWMedia = media;
                            result.Add(NEWMedia);
                        }
                        var obj = new
                        {
                            Status = (int)PostVisibility.Visible,
                            Medias = result
                        };
                        return new ServiceResult<object>().Ok(obj);
                    }
                    else
                    {
                        var obj2 = new
                        {
                            Status = (int)PostVisibility.PrivateAndNotFollowing,
                            Medias = ""
                        };
                        return new ServiceResult<object>().Ok(obj2);
                    }
                }

                var obj3 = new
                {
                    Status = (int)PostVisibility.PrivateAndNotLogged,
                    Medias = ""
                };
                return new ServiceResult<object>().Ok(obj3);
            }
        }

        public async Task<bool> UploadVideoGallery(FileData fileData)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var outPut = _mediaService.SaveImage(fileData.Base64, EntityUrls.GalleryVideosMediaUrl);
            if (outPut.IsSuccess)
            {
                SocialMedia dbMedia = new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = userId,
                    PictureUrl = outPut.ImageName,
                    Type = MediaTypes.GalleryVideos,
                    MediaId = 0,
                    UpdateDate = DateTime.Now
                };
                await _mediaRepository.InsertAsync(dbMedia);
                return true;
            }
            return false;
        }

        public async Task<IServiceResult<User>> UpdateUserByAdmin(User item)
        {
            
            var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == item.UserId);


            var dbLastBanner = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == item.UserId && z.Type == MediaTypes.Banner);

            if (item.BannerFileData == "Delete")
            {
                if (dbLastBanner != null)
                {
                    dbLastBanner.PictureUrl = "";
                    await _mediaRepository.Update(dbLastBanner);
                    await _mediaRepository.Delete(dbLastBanner);
                }

            }
            else if (!string.IsNullOrEmpty(item.BannerFileData))
            {
                var outPut = _mediaService.SaveImage(item.BannerFileData, EntityUrls.BannerMediaUrl);
                if (outPut.IsSuccess)
                {
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.UserId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Banner,
                        MediaId = 0
                    };

                    if (dbLastBanner != null)
                    {
                        dbLastBanner.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLastBanner);
                        dbUser.BannerFileData = outPut.ImageName;
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                        dbUser.BannerFileData = outPut.ImageName;
                    }
                }
            }

            var dbLastSelfie = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == item.UserId && z.Type == MediaTypes.Profile);

            if (item.SelfieFileData == "Delete")
            {
                if (dbLastSelfie != null)
                {
                    dbLastSelfie.PictureUrl = "";
                    await _mediaRepository.Update(dbLastSelfie);
                    await _mediaRepository.Delete(dbLastSelfie);
                }
            }
            else if (!string.IsNullOrEmpty(item.SelfieFileData))
            {
                var outPut = _mediaService.SaveImage(item.SelfieFileData, EntityUrls.ProfileMediaUrl);
                if (outPut.IsSuccess)
                {
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.UserId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Profile,
                        MediaId = 0
                    };

                    if (dbLastSelfie != null)
                    {
                        dbLastSelfie.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLastSelfie);
                        dbUser.SelfieFileData = outPut.ImageName;
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                        dbUser.SelfieFileData = outPut.ImageName;
                    }
                }

            }


            dbUser.Name = string.IsNullOrEmpty(item.Name) ? dbUser.Name : item.Name;
            dbUser.Email = string.IsNullOrEmpty(item.Email) ? dbUser.Email : item.Email;
            dbUser.Mobile = string.IsNullOrEmpty(item.Mobile) ? dbUser.Mobile : item.Mobile;
            dbUser.FamilyName = string.IsNullOrEmpty(item.FamilyName) ? dbUser.FamilyName : item.FamilyName;
            dbUser.CityId = item.CityId == null ? dbUser.CityId : item.CityId;
            dbUser.StateId = item.StateId == null ? dbUser.StateId : item.StateId;
            dbUser.RelationState =  item.RelationState == null ? dbUser.RelationState : item.RelationState;
            if (!string.IsNullOrEmpty(item.Password))
            {
                var dbCurrentFromUser = _encryptService.Encrypt(item.CurrentPassword).Data;
                if (dbCurrentFromUser == dbUser.Password)
                {
                    Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$");
                    Match match = regex.Match(item.Password);
                    if (match.Success)
                    {
                        dbUser.Password = _encryptService.Encrypt(item.Password).Data;
                    }
                    else
                    {
                        return new ServiceResult<User>().Failure("Password format not matched");
                    }
                }
                else
                {
                    return new ServiceResult<User>().Failure("Password Is not correct");
                }
            }
            dbUser.Status=item.Status ==0 ? dbUser.Status : item.Status;
            await userRepository.Update(dbUser);


            _activityRepository.Insert(new Activity()
            {
                ActivityId=0,
                CreateDate=DateTime.Now,
                IsDeleted=false,
                ObjectId=item.UserId,
                Type=ActivityTypes.UpdateProfile,
                UserId=item.UserId
            });


            dbUser.Password = null;
            return new ServiceResult<User>().Ok(dbUser);
        }

        public object GetAll(int pageNumber, int count,string search)
        {
            List<User> users = new List<User>();
            users = userRepository.GetQuery().Where(z=>z.Type!=2 && (z.Name.Contains(search) || z.FamilyName.Contains(search) || z.UserName.Contains(search))).Skip((pageNumber-1)*count).Take(count).ToList();
            var usersCount=userRepository.GetQuery().Where(z=>z.Type !=2 && (z.Name.Contains(search) || z.FamilyName.Contains(search) || z.UserName.Contains(search))).Count();
            users.ForEach(z => z.Password = null);
            users.ForEach(z => z.UserSetting = userSettingRepository.GetQuery().AsNoTracking().FirstOrDefault(x => x.UserId == z.UserId));
            users.ForEach(z => z.SelfieFileData = _mediaRepository.GetQuery().
            FirstOrDefault(x => x.ObjectId == z.UserId && x.Type == MediaTypes.Profile) == null ? ""
            : _mediaRepository.GetQuery().
            FirstOrDefault(x => x.ObjectId == z.UserId && x.Type == MediaTypes.Profile).PictureUrl);
            var obj = new
            {
                Users = users,
                UsersCount = usersCount
            };
            return obj;
        }
        public async Task<object> GetAllSP()
        {
            var cmd = "select M.PictureUrl, U.UserId, U.Name, U.FamilyName, U.Email, U.Mobile, U.Type, U.Status, U.FormalId, US.Bio, US.BirthDate, US.Gender, US.UserTag, US.Flag, US.IsPrivate from[User].Users as U left join[User].UserSettings  as US on U.UserId = US.UserId left join Social.Medias as M on (M.ObjectId = U.UserId and M.Type = 2)";
            var res = await userRepository.DapperSqlQuery(cmd);
            var Des = JsonSerializer.Serialize<object>(res);
            //var sdf = JsonSerializer.Deserialize<UserAndUserSetting>(Des);
            return Des;
        }

        public async Task Delete(long id)
        {
            var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == id && z.IsDeleted == false);
            if (dbUser != null)
            {
                await userRepository.Delete(dbUser);
            }
        }
        public IServiceResult<object> GetMediaGalleryByUserName(string userName)
        {
            var userId = userRepository.GetQuery().FirstOrDefault(z => z.UserName == userName).UserId;
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateGalleryFriend;
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            if (IsPrivate == false)
            {
                var medias = _mediaRepository.GetQuery().Where(z => (z.Type == MediaTypes.GalleryImages || z.Type== MediaTypes.GalleryVideos) && z.ObjectId == userId).ToList();
                List<FileData> result = new List<FileData>();
                foreach (var media in medias)
                {
                    //var url = "media/gallery/GalleryImages/";
                    //if (media.Priority == 3)
                    //{
                    //    url = "media/gallery/GalleryVideos/";
                    //}
                    var NEWMedia =  media.PictureUrl;
                    var obj1 = new FileData() { Base64 = NEWMedia, Priority = media.Priority, UploadType = 1 };
                    result.Add(obj1);
                }
                var obj = new
                {
                    Status = (int)PostVisibility.Visible,
                    Medias = result
                };
                return new ServiceResult<object>().Ok(obj);
            }
            else
            {
                if (MYselfId > 0)
                {
                    //آیا کاربر وارد شده یوزر آی دی ذکر شده رو فالو دارد؟
                    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                    //اگر فالو دارد
                    if (dbFollowing != null)
                    {
                        var medias = _mediaRepository.GetQuery().Where(z => z.Type == MediaTypes.GalleryImages && z.ObjectId == userId).ToList();
                        List<FileData> result = new List<FileData>();
                        foreach (var media in medias)
                        {
                            //var url = "media/gallery/GalleryImages/";
                            //if (media.Priority == 3)
                            //{
                            //    url = "media/gallery/GalleryVideos/";
                            //}
                            var NEWMedia =  media.PictureUrl;
                            var obj2 = new FileData() { Base64 = NEWMedia, Priority = media.Priority, UploadType = 1 };
                            result.Add(obj2);
                        }
                        var obj = new
                        {
                            Status = (int)PostVisibility.Visible,
                            Medias = result
                        };
                        return new ServiceResult<object>().Ok(obj);
                    }
                    //اگر فالو ندارد
                    else
                    {
                        var obj2 = new
                        {
                            Status = (int)PostVisibility.PrivateAndNotFollowing,
                            Medias = ""
                        };
                        return new ServiceResult<object>().Ok(obj2);
                    }
                }

                var obj3 = new
                {
                    Status = (int)PostVisibility.PrivateAndNotLogged,
                    Medias = ""
                };
                return new ServiceResult<object>().Ok(obj3);
            }
        }
        public User GetByUserName(string userName)
        {
            var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserName == userName);
            if(dbUser != null)
            {
                var id = dbUser.UserId;
                var dbFollowingsCount = followingRepository.GetQuery().Where(z => z.UserId == dbUser.UserId).Count();
                var dbFollowersCount = followerRepository.GetQuery().Where(z => z.UserId == dbUser.UserId).Count();
                dbUser.FollowersCount = dbFollowersCount;
                dbUser.FollowingsCount = dbFollowingsCount;

                var dbBanner = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Banner);
                if (dbBanner != null)
                {
                    dbUser.BannerFileData =  dbBanner.PictureUrl;
                }

                var dbProfile = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Profile);
                if (dbProfile != null)
                {
                    dbUser.SelfieFileData =  dbProfile.PictureUrl;
                }
                dbUser.UserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == id);

                var KartMelliImage = _mediaRepository.GetQuery()
                    .FirstOrDefault(z => z.ObjectId == dbUser.UserSetting.UserSettingId
                    && z.Type == MediaTypes.KartMelliDoc);

                dbUser.UserSetting.KartMelliDoc = KartMelliImage == null ? "" : KartMelliImage.PictureUrl;

                var passportImage = _mediaRepository.GetQuery()
                    .FirstOrDefault(z => z.ObjectId == dbUser.UserSetting.UserSettingId
                    && z.Type == MediaTypes.PassportDoc);

                dbUser.UserSetting.PassportDoc = passportImage == null ? "" : passportImage.PictureUrl;

                dbUser.Password = null;
                return dbUser;
            }
            return null;
        }

        public User Get(long id)
        {
            var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == id);
            var dbFollowingsCount = followingRepository.GetQuery().Where(z => z.UserId == dbUser.UserId).Count();
            var dbFollowersCount = followerRepository.GetQuery().Where(z => z.UserId == dbUser.UserId).Count();
            dbUser.FollowersCount = dbFollowersCount;
            dbUser.FollowingsCount = dbFollowingsCount;

            var dbBanner = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Banner);
            if (dbBanner != null)
            {
                dbUser.BannerFileData =  dbBanner.PictureUrl;
            }

            var dbProfile = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Profile);
            if (dbProfile != null)
            {
                dbUser.SelfieFileData = dbProfile.PictureUrl;
            }
            dbUser.UserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == id);

            var KartMelliImage = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == dbUser.UserSetting.UserSettingId
                && z.Type == MediaTypes.KartMelliDoc);

            dbUser.UserSetting.KartMelliDoc = KartMelliImage == null ? "" : KartMelliImage.PictureUrl;

            var passportImage = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == dbUser.UserSetting.UserSettingId
                && z.Type == MediaTypes.PassportDoc);

            dbUser.UserSetting.PassportDoc = passportImage == null ? "" :  passportImage.PictureUrl;

            dbUser.Password = null;
            return dbUser;
        }

        public User GetByToken()
        {
            var id = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == id);
            var dbFollowingsCount = followingRepository.GetQuery().Where(z => z.UserId == dbUser.UserId).Count();
            var dbFollowersCount = followerRepository.GetQuery().Where(z => z.UserId == dbUser.UserId).Count();
            dbUser.FollowersCount = dbFollowersCount;
            dbUser.FollowingsCount = dbFollowingsCount;

            var dbBanner = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Banner);
            if (dbBanner != null)
            {
                dbUser.BannerFileData = dbBanner.PictureUrl;
            }

            var dbProfile = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Profile);
            if (dbProfile != null)
            {
                dbUser.SelfieFileData = dbProfile.PictureUrl;
            }
            dbUser.UserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == id);

            var KartMelliImage = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == dbUser.UserSetting.UserSettingId
                && z.Type == MediaTypes.KartMelliDoc);

            dbUser.UserSetting.KartMelliDoc = KartMelliImage == null ? "" :  KartMelliImage.PictureUrl;

            var passportImage = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == dbUser.UserSetting.UserSettingId
                && z.Type == MediaTypes.PassportDoc);

            dbUser.UserSetting.PassportDoc = passportImage == null ? "" :passportImage.PictureUrl;

            dbUser.Password = null;
            return dbUser;
        }


        public List<User> SearchByName(string name)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var cmd = $"select top 10 * from [User].Users where (Name like '%{name}%' or FamilyName like '%{name}%') And UserId !={userId} And type != 2";
            var DeSerializeObj = userRepository.DapperSqlQuery(cmd);
            var objSer = JsonSerializer.Serialize<object>(DeSerializeObj.Result);
            var dbUsers = JsonSerializer.Deserialize<List<User>>(objSer);
            foreach (var user in dbUsers)
            {
                user.Password = null;
                var selfie = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == user.UserId && z.Type == MediaTypes.Profile);
                user.SelfieFileData = selfie == null ? "" :  selfie.PictureUrl;
                user.UserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == user.UserId);
            }
            return dbUsers;
        }

        public object GetAllUserforUser(long userId, int count, string search)
        {
            var mySelfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            int IsRequested = 0;
            var dbFollowRequest = _followRequestRepository.GetQuery().FirstOrDefault(z => z.FollowerUserId == mySelfId && z.FollowingUserId == userId);
            if (dbFollowRequest != null)
            {
                IsRequested = 1;
            }

            var completationCmd = userId ==0 ? "" : $"and U.UserId < {userId}";
            string cmd = $"select SF.UserId as IsFollowing ,{IsRequested} as Requested, U.UserId , U.Name , U.FamilyName , U.UserName , SM.PictureUrl , US.Bio , U.Type , (select Count(*) from Social.Followers where UserId = U.userId) as FollowersCount , (select Count(*) from Social.Followings where UserId = U.userId) as FollowingsCount " +
            "  from[User].Users U "+
            "  join[User].UserSettings US on U.UserId = US.UserId "+
            "  join Social.Medias SM on SM.ObjectId = U.UserId " +
            $" left join Social.Followings SF on SF.FollowingUserId = U.UserId and SF.UserId = {mySelfId}"+
            $"  where(U.Name like N'%{search}%' or U.FamilyName like N'%{search}%' or U.UserName like N'%{search}%') and SM.Type =2 {completationCmd} "+
            $"  order by U.userId Desc OFFSET 0 ROWS FETCH NEXT { count} ROWS ONLY ";
            var dbUsers = userRepository.DapperSqlQuery(cmd).Result;
            var SerializeObject = JsonSerializer.Serialize<object>(dbUsers);

            

            return SerializeObject;
        }

        public async Task<object> UserDashboards()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var offset = DateTime.Now.AddDays(-6);

            var userFollowers = followerRepository.GetQuery().Where(x => x.UserId == userId && x.CreateDate >= offset).Count();

            //پست های این شخص
            List<Post> userPosts = new List<Post>();
            userPosts = postRepository.GetQuery().Where(x => x.UserId == userId).ToList();
            //تعداد لایک هایی که پست های این شخص خورده
            int lastWeekPostLikeCount = 0;
            int lastWeekPostCommnetcount = 0;

            int allPostLikeCount = 0;

            foreach (var post in userPosts)
            {
                #region lastWeekPostLikeCount
                int likesCount = postLikeRepository.GetQuery().Count(z => z.PostId == post.PostId && z.CreateDate >= offset);
                lastWeekPostLikeCount += likesCount;
                #endregion

                #region allPostLikeCount
                int allLikesCount = postLikeRepository.GetQuery().Count(z => z.PostId == post.PostId);
                allPostLikeCount += allLikesCount;
                #endregion

                #region lastWeekPostCommnetcount
                int commnetCount = postCommentRepository.GetQuery().Count(z => z.ParentId == 0 && z.PostId == post.PostId && z.CreateDate >= offset);
                lastWeekPostCommnetcount += commnetCount;
                #endregion
            }


            //فروم های این شخص
            List<Forum> userforums = new List<Forum>();
            userforums = forumRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            //
            double lastWeekFroumsRate = 0;
            double allFroumsRate = 0;
            int lastWeekforumComments = 0;

            foreach (var item in userforums)
            {
                //امتیاز هفته آخر فروم کاربر
                var lastweekdbRate = await ratingService.GetLastWeekByObjectIdAndType(item.ForumId, RatingType.Forum);
                var lastweekRate = lastweekdbRate.Data.Average;
                lastWeekFroumsRate += lastweekRate;

                //امتیاز فروم
                var alldbRate = await ratingService.GetLastWeekByObjectIdAndType(item.ForumId, RatingType.Forum);
                var allRate = lastweekdbRate.Data.Average;
                allFroumsRate += allRate;

                //تعداد کامنت های فروم در هفته آخر
                var lastWeekDbforumComments = forumCommentRepository.GetQuery().Count(z => z.ForumId == item.ForumId && z.CreateDate >= offset);
                lastWeekforumComments += lastWeekDbforumComments;
            }

            // سرویس های کاربر
            List<ServicePack> userServicePacks = new List<ServicePack>();
            userServicePacks = servicePackRepository.GetQuery().Where(z => z.UserId == userId).ToList();

            double lastWeekIncome = 0;
            
            foreach (var service in userServicePacks)
            {
                //پرداخت هایی که به ازای این سرویس در هفته آخر انجام شده

                //var orderItemList = orderItemRepository.GetQuery().Where(z => z.ServiceId == service.ServicePackId);
                //foreach (var item in orderItemList)
                //{
                //    var income = orderRepository.GetQuery().Where(z => z.ServiceId == item.ServicePackId && z.CreateDate>=offset).Sum(x => x.SumPrice);
                //    lastWeekIncome += income;
                //}

                string incomeCmd = " SELECT        SUM([Order].Orders.SumPrice) price " +
" FROM[Order].OrderItems INNER JOIN " +
             " [Order].Orders ON[Order].OrderItems.OrderId = [Order].Orders.OrderId " +
" WHERE([Order].OrderItems.ServiceId = 51 and[Order].Orders.IsPayed = 1 " +
" and( " +

" CAST([Order].Orders.CreateDate as date) between " +
" CAST(DATEADD(dd, -7, GETDATE()) as date) and " +
" CAST(GETDATE() AS DATE) " +
" ) " +
" ) ";
 

                var incomes = await orderRepository.DapperSqlQuery(incomeCmd);
                var incomesSerializeObject = JsonSerializer.Serialize<object>(incomes);
                var serializedincomes = JsonSerializer.Deserialize<List<PurchasedPlansByUserDto>>(incomesSerializeObject);


            }

            //پرداخت های هفته آخر کاربر
            double lastWeekPayed = paymentRepository.GetQuery()
                .Where(z => z.UserId == userId && z.CreateDate>=offset && z.Status==0)
                .Sum(x=>x.Amount);

            // پلن هایی که کاربر خریده به تفکیک تعداد
            string cmd = " SELECT            [Order].Orders.ServiceId, " +
                         " [Order].OrderItems.PlanId, count( [Order].OrderItems.PlanId) PlanCount,Plans.Title " +
                         " FROM Payment.Payments INNER JOIN " +
                          " [Order].Orders ON Payment.Payments.UserId = [Order].Orders.UserId INNER JOIN " +
                          " [Order].OrderItems ON[Order].Orders.OrderId = [Order].OrderItems.OrderId " +
                          " inner JOIN Plans ON Plans.PlanId =[Order].OrderItems.PlanId " +
                          $" WHERE ([Order].Orders.UserId = {userId}) " +
                          " group by [Order].OrderItems.PlanId,[Order].Orders.ServiceId,Plans.Title ";

            var purchasedPlans = await orderRepository.DapperSqlQuery(cmd);
            var SerializeObject = JsonSerializer.Serialize<object>(purchasedPlans);
            var serializedPurchasedPlans = JsonSerializer.Deserialize<List<PurchasedPlansByUserDto>>(SerializeObject);

            var Obj = new
            {
                FollowersCount = userFollowers,
                PostLikeCount = lastWeekPostLikeCount,
                PostCommnet= lastWeekPostCommnetcount,
                ForumRate= lastWeekFroumsRate,
                ForumComments= lastWeekforumComments,
                AllPostLikeCount= allPostLikeCount,
                AllForumRate= allFroumsRate,
                //LastWeekIncome= lastWeekIncome,
                //LastWeekPayed= lastWeekPayed,
                //PurchasedPlans = serializedPurchasedPlans
            };

            return Obj;

        }
    }
}
