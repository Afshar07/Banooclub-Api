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

namespace BanooClub.Services.PostCommentServices
{
    public class PostCommentService : IPostCommentService
    {
        private readonly IBanooClubEFRepository<PostComment> postCommentRepository;
        private readonly IBanooClubEFRepository<Activity> activityRepository;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor _accessor;

        public PostCommentService(IBanooClubEFRepository<PostComment> postCommentRepository
            , IUserService userService,
            IBanooClubEFRepository<Activity> activityRepository,
            IHttpContextAccessor accessor)
        {
            this.postCommentRepository = postCommentRepository;
            this.userService = userService;
            this.activityRepository = activityRepository;
            _accessor = accessor;
        }
        public async Task Create(PostComment inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            inputDto.CreateDate = DateTime.Now;
            var dbPost=postCommentRepository.Insert(inputDto);

            activityRepository.Insert(new Activity()
            {
                UserId = userId,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                ActivityId=0,
                ObjectId=dbPost.PostCommentId,
                Type=ActivityTypes.CommentPost,
            });

            await postCommentRepository.Save();
        }

        public async Task<PostComment> Update(PostComment item)
        {
            await postCommentRepository.Update(item);
            return item;
        }

        public async Task<List<PostComment>> GetAll()
        {
            List<PostComment> postComments = new List<PostComment>();
            postComments = postCommentRepository.GetQuery().ToList();
            postComments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
            return postComments;
        }

        public async Task Delete(long id)
        {
            var postComment = postCommentRepository.GetQuery().FirstOrDefault(z => z.PostCommentId == id);
            await postCommentRepository.Delete(postComment);
        }

        public async Task<PostComment> Get(long id)
        {
            var postComment = postCommentRepository.GetQuery().FirstOrDefault(z => z.PostCommentId == id);
            postComment.UserInfo = userService.Get(postComment.UserId);
            return postComment;
        }
        public List<PostComment> GetByUserId(long userId)
        {
            var comments = postCommentRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
            return comments;
        }
        public List<PostComment> GetByPostId(long postId)
        {
            var comments = postCommentRepository.GetQuery().Where(z => z.PostId == postId).ToList();
            comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
            return comments;
        }

        public List<PostComment> GetChildrenByPostId(long postId, long commnetId)
        {
            var comments = postCommentRepository.GetQuery().Where(z => z.PostId == postId && z.ParentId == commnetId).ToList();
            comments.ForEach(z => z.UserInfo = userService.Get(z.UserId));
            return comments;
        }
    }
}
