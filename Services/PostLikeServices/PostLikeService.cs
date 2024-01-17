using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.PostLikeServices
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IBanooClubEFRepository<PostLike> postLikeRepository;
        private readonly IBanooClubEFRepository<Activity> activityRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor _accessor;

        public PostLikeService(IBanooClubEFRepository<PostLike> postLikeRepository, IUserService userService,
            IBanooClubEFRepository<User> userRepository,
            IBanooClubEFRepository<Activity> activityRepository, IHttpContextAccessor accessor)
        {
            this.postLikeRepository = postLikeRepository;
            _accessor = accessor;
            this.userService = userService;
            this.activityRepository = activityRepository;
            this.userRepository = userRepository;
        }
        public bool Create(long postId)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.PostId == postId);

            if (dbLike == null)
            {
                var dbPostLike = new PostLike
                {
                    UserId = userId,
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    PostId = postId,
                    PostLikeId = 0
                };

                var like=postLikeRepository.Insert(dbPostLike);

                activityRepository.Insert(new Activity()
                {
                    ActivityId = 0,
                    CreateDate= DateTime.Now,
                    IsDeleted= false,
                    ObjectId=like.PostLikeId,
                    Type=ActivityTypes.LikePost,
                    UserId=userId,
                });
                return true;
            }
            else
            {
                postLikeRepository.Erase(dbLike);
                return false;
            }
        }

        public async Task<PostLike> Update(PostLike item)
        {
            await postLikeRepository.Update(item);
            return item;
        }

        public async Task<List<PostLike>> GetAll()
        {
            List<PostLike> postLikes = new List<PostLike>();
            postLikes = postLikeRepository.GetQuery().ToList();
            return postLikes;
        }

        public async Task Delete(long id)
        {
            var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.PostLikeId == id);
            await postLikeRepository.Delete(postLike);
        }

        public async Task<PostLike> Get(long id)
        {
            var postLike = postLikeRepository.GetQuery().FirstOrDefault(z => z.PostLikeId == id);
            return postLike;
        }
        public List<PostLike> GetByUserId(long userId)
        {
            return postLikeRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }
        public List<User> GetLikesByPostId(long postId)
        {
            var postLikes = postLikeRepository.GetQuery().Where(z => z.PostId == postId).ToList();
            List<User> users = new List<User>();
            foreach (var pL in postLikes)
            {
                var dbUser = userService.Get(pL.UserId);
                users.Add(dbUser);
            }
            return users;
        }
    }
}
