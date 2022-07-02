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

namespace BanooClub.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly IBanooClubEFRepository<Post> postRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<PostComment> postCommentRepository;
        private readonly IBanooClubEFRepository<PostLike> postLikeRepository;
        private readonly IBanooClubEFRepository<PostNK> postNKRepository;
        private readonly IBanooClubEFRepository<PostReport> postReportRepository;
        private readonly IBanooClubEFRepository<UserSetting> userSettingRepository;
        private readonly IBanooClubEFRepository<ViewHistory> viewHistoryRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService userService;
        private readonly IDistributedCache distributedCache;

        public PostService(IBanooClubEFRepository<Post> postRepository, IBanooClubEFRepository<Following> followingRepository,
            IBanooClubEFRepository<User> userRepository, IBanooClubEFRepository<PostComment> postCommentRepository
            , IBanooClubEFRepository<PostLike> postLikeRepository,
            IUserService userService, IBanooClubEFRepository<UserSetting> userSettingRepository,
            IHttpContextAccessor accessor,
            IDistributedCache distributedCache,
            IBanooClubEFRepository<ViewHistory> viewHistoryRepository,
            IBanooClubEFRepository<PostReport> postReportRepository,
            IBanooClubEFRepository<PostNK> postNKRepository)
        {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.followingRepository = followingRepository;
            this.postCommentRepository = postCommentRepository;
            this.postLikeRepository = postLikeRepository;
            this.userService = userService;
            this.postNKRepository = postNKRepository;
            this.postReportRepository = postReportRepository;
            this.distributedCache = distributedCache;
            this.userSettingRepository = userSettingRepository;
            this.viewHistoryRepository = viewHistoryRepository;

            _accessor = accessor;
        }
        public async Task Create(Post inputDto)
        {
            var cacheKey = "LAST";
            string serializedCustomerList;
            var Nks = new List<PostNK>();
            var PostNksType = distributedCache.GetType();
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
                //var options = new DistributedCacheEntryOptions()
                //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, PostNkList);
            }

            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            inputDto.Status=(int)PostStatus.Published;
            inputDto.UpdateDate = DateTime.Now;
            var NKs = postNKRepository.GetAll().Result.Select(z => z.Name).ToList();
            if (NKs.Any(z => (inputDto.Content.Contains(z))))
            {
                inputDto.Status = (int)PostStatus.NotConfirmed;
            }
            postRepository.Insert(inputDto);
        }

        public async Task<Post> Update(Post item)
        {
            await postRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<Post> posts = new List<Post>();
            posts = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.Published).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var PostCount = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.Published).Count();
            var obj = new
            {
                Posts = posts,
                PostCount = PostCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var post = postRepository.GetQuery().FirstOrDefault(z => z.PostId == id);
            await postRepository.Delete(post);
            await postRepository.Save();
        }

        public async Task<Post> Get(long id)
        {
            var post = postRepository.GetQuery().FirstOrDefault(z => z.PostId == id);
            return post;
        }
        public async Task<long> UpdateByCmd(string content, long postId)
        {
            var updateDate = DateTime.Now;
            string UpdateCmd = $"Update social.posts Set Content = '{content}' , UpdateDate = '{updateDate}' Where PostId={postId}";
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
        public object GetMyPost(int pageNumber, int count)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbPosts = postRepository.GetQuery().Where(z => z.UserId == userId).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
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
            }
            var obj = new
            {
                Posts = dbPosts,
                PostCount = PostCount
            };
            return obj;
        }

        public IServiceResult<object> GetByUserId(long userId)
        {
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateSocial;
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            if (IsPrivate == false)
            {

                var dbPosts = postRepository.GetQuery().Where(z => z.UserId == userId).ToList();
                foreach (var post in dbPosts)
                {
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
                        foreach (var post in dbPosts)
                        {
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
        //public List<object> GetFollowingPosts(long userId)
        //{
        //    var dbFollowings = followingRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        //    List<object> dbFollowingPosts = new List<object>();
        //    foreach (var following in dbFollowings)
        //    {
        //        var followingPosts = postRepository.GetQuery().Where(z => z.UserId == following.FollowingUserId).ToList();
        //        var followingInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == following.FollowingUserId);
        //        foreach (var item in followingPosts)
        //        {
        //            var likes = postLikeRepository.GetQuery().Where(z => z.PostId == item.PostId).Count();
        //            var comments = postCommentRepository.GetQuery().Where(z => z.PostId == item.PostId).Count();
        //            var obj = new
        //            {
        //                UserInfo = followingInfo,
        //                Post = item,
        //                Likes = likes,
        //                Comments = comments
        //            };
        //            dbFollowingPosts.Add(obj);
        //        }
        //    }

        //    return dbFollowingPosts;
        //}
        public  async Task<List<PostDTO>> GetFollowingPosts()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            string Cmd = "declare @A bigint select @A = (select distinct Count(P.PostId) from Social.Posts as P " +
              " left join[User].Users as U on P.UserId = U.UserId" +
              " left join Social.Followings as F on P.UserId = F.FollowingUserId" +
              " left join Social.PostLikes as L on L.PostId = P.PostId" +
              " left join social.PostComments as C on C.PostId = P.PostId" +
              $" left join social.ViewHistories as VH on VH.userId = {userId}" +
              $" where (F.UserId = {userId} or P.UserId={userId}) And P.Status = 1 And P.IsDeleted = 0 And P.PostId > VH.FirstPostId)" +
              " if (@A > 9)" +
              "  begin" +
              "  select distinct top 10  U.UserId, (select count(*) from social.PostLikes where PostId = p.PostId) as LikesCount," +
              " (select count(*) from social.PostComments where PostId = p.PostId) as CommentsCount," +
              " U.Name,U.FamilyName,U.Status,P.Content,P.PostId,P.CreateDate,P.UpdateDate from" +
              " Social.Posts as P " +
              " left join[User].Users as U on P.UserId = U.UserId" +
              " left join Social.Followings as F on P.UserId = F.FollowingUserId" +
              " left join Social.PostLikes as L on L.PostId = P.PostId" +
              " left join social.PostComments as C on C.PostId = P.PostId" +
              $" left join social.ViewHistories as VH on VH.userId = {userId}" +
              $" where (F.UserId = {userId} or P.UserId={userId}) And P.Status = 1 And P.IsDeleted = 0 And P.PostId > VH.FirstPostId  order by  P.CreateDate asc" +
              " End" +
              " else if (@A < 10)" +
              "  begin" +
              "  select Distinct top 10 U.UserId, (select count(*) from social.PostLikes where PostId = p.PostId) as LikesCount," +
              " (select count(*) from social.PostComments where PostId = p.PostId) as CommentsCount," +
              " U.Name,U.FamilyName,U.Status,P.Content,P.PostId,P.CreateDate,P.UpdateDate from" +
              " Social.Posts as P " +
              " left join[User].Users as U on P.UserId = U.UserId" +
              " left join Social.Followings as F on P.UserId = F.FollowingUserId" +
              " left join Social.PostLikes as L on L.PostId = P.PostId" +
              " left join social.PostComments as C on C.PostId = P.PostId" +
              $" left join social.ViewHistories as VH on VH.userId = {userId}" +
              $" where (F.UserId = {userId} or P.UserId={userId}) And P.Status = 1 And P.IsDeleted = 0 And(P.PostId > VH.FirstPostId or P.PostId < VH.LastPostId) order by  P.CreateDate Desc" +
              " End";
            var result = postRepository.SqlObj(Cmd);
            var dbViewHistory = viewHistoryRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);

            if (result.Count == 0)
            {
                dbViewHistory.LastPostId = 0;
                dbViewHistory.FirstPostId = 0;
                dbViewHistory.UpdateDate= DateTime.Now;
                var updated =await viewHistoryRepository.Update(dbViewHistory);
                result=postRepository.SqlObj(Cmd);
            }

            if (result.Count > 0)
            {
                var Descending = result.OrderByDescending(z => z.PostId).ToList();
                long Last=Descending.LastOrDefault().PostId;
                long First=Descending.FirstOrDefault().PostId;
                if (dbViewHistory.LastPostId > Last || dbViewHistory.LastPostId == 0)
                {
                    dbViewHistory.LastPostId = Last;
                }
                if (dbViewHistory.FirstPostId < First)
                {
                    dbViewHistory.FirstPostId = First;
                }
                dbViewHistory.UpdateDate = DateTime.Now;
                await viewHistoryRepository.Update(dbViewHistory);
            }
            foreach (var item in result)
            {
                item.Comments = new List<PostComment>();
                item.Comments = postCommentRepository.GetQuery().Where(z => z.PostId == item.PostId).ToList();
                item.UserInfo = userService.Get(item.UserId);
                foreach (var element in item.Comments)
                {
                    element.UserInfo = userService.Get(element.UserId);
                }
                var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.PostId == item.PostId);
                var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.ReporterUserId == userId && z.PostId == item.PostId);
                item.IsLikedByMe = postLike == null ? false : true;
                item.IsReportedByMe = postReport == null ? false : true;
            }
            return result;
        }
        public object GetNotConfirmed(int pageNumber, int count, string search)
        {
            List<Post> posts = new List<Post>();
            posts = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.NotConfirmed && z.Content.Contains(search)).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var PostCount = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.NotConfirmed && z.Content.Contains(search)).Count();
            posts.ForEach(z=>z.UserInfo= userService.Get(z.UserId));
            var obj = new
            {
                Posts=posts,
                PostCount=PostCount,
            };
            return obj;
        }
        public object GetReported(int pageNumber, int count)
        {
            List<Post> posts = new List<Post>();
            posts = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.Report).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            posts.ForEach(z => z.UserInfo= userService.Get(z.UserId));
            var PostCount = postRepository.GetQuery().Where(z => z.Status == (int)PostStatus.Report).Count();
            var obj = new
            {
                Posts = posts,
                PostCount = PostCount,
            };
            return obj;
        }
        public async Task<Post> ChangePostStatus(Post item)
        {
            var dbPost = postRepository.GetQuery().FirstOrDefault(z => z.PostId == item.PostId);
            if (item.Status == (int)PostStatus.Published)
            {
                dbPost.Status = (int)PostStatus.Published;
                dbPost.Content = item.Content == null ? dbPost.Content : item.Content;
            }
            else if (item.Status == (int)PostStatus.Report)
            {
                dbPost.Status = (int)PostStatus.Report;
                dbPost.Content = item.Content == null ? dbPost.Content : item.Content;

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
            return dbPost;
        }
    }
}
