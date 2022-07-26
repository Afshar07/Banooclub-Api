using Infrastructure;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.CommentLikeServices
{
    public class CommentLikeService : ICommentLikeService
    {
        private readonly IBanooClubEFRepository<CommentLike> commentLikeRepository;
        private readonly IBanooClubEFRepository<Activity> activityRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor _accessor;

        public CommentLikeService(IBanooClubEFRepository<CommentLike> commentLikeRepository, IUserService userService,
            IBanooClubEFRepository<User> userRepository,
            IBanooClubEFRepository<Activity> activityRepository, IHttpContextAccessor accessor)
        {
            this.commentLikeRepository = commentLikeRepository;
            _accessor = accessor;
            this.userService = userService;
            this.activityRepository = activityRepository;
            this.userRepository = userRepository;
        }
        public bool Create(long commentId, CommentType type)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbLike = commentLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId == commentId);

            if (dbLike == null)
            {
                var dbCommentLike = new CommentLike
                {
                    UserId = userId,
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    ObjectId = commentId,
                    Type=type,
                    CommentLikeId = 0
                };

                var like = commentLikeRepository.Insert(dbCommentLike);

                activityRepository.Insert(new Activity()
                {
                    ActivityId = 0,
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    ObjectId = like.CommentLikeId,
                    Type = ActivityTypes.LikeComment,
                    UserId = userId,
                });
                return true;
            }
            else
            {
                commentLikeRepository.Erase(dbLike);
                return false;
            }
        }

        public async Task<CommentLike> Update(CommentLike item)
        {
            await commentLikeRepository.Update(item);
            return item;
        }

        public async Task<List<CommentLike>> GetAll()
        {
            List<CommentLike> commentLikes = new List<CommentLike>();
            commentLikes = commentLikeRepository.GetQuery().ToList();
            return commentLikes;
        }

        public async Task Delete(long id)
        {
            var commentLike = commentLikeRepository.GetQuery().FirstOrDefault(z => z.CommentLikeId == id);
            await commentLikeRepository.Delete(commentLike);
        }

        public async Task<CommentLike> Get(long id)
        {
            var commentLike = commentLikeRepository.GetQuery().FirstOrDefault(z => z.CommentLikeId == id);
            return commentLike;
        }
        public List<CommentLike> GetByUserId(long userId, CommentType type)
        {
            return commentLikeRepository.GetQuery().Where(z => z.UserId == userId && z.Type==type).ToList();
        }
        public List<User> GetLikesByCommentId(long objectId, CommentType type)
        {
            var commentLikes = commentLikeRepository.GetQuery().Where(z => z.ObjectId == objectId && z.Type==type).ToList();
            List<User> users = new List<User>();

            foreach (var pL in commentLikes)
            {
                var dbUser = userService.Get(pL.UserId);
                users.Add(dbUser);
            }
            return users;
        }


    }
}
