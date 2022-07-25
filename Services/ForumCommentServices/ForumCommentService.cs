using Infrastructure;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ForumCommentServices
{
    public class ForumCommentService : IForumCommentService
    {
        private readonly IBanooClubEFRepository<ForumComment> forumCommentRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IHttpContextAccessor _accessor;
        public ForumCommentService(IBanooClubEFRepository<ForumComment> forumCommentRepository, IBanooClubEFRepository<User> userRepository, IHttpContextAccessor accessor)
        {
            this.forumCommentRepository = forumCommentRepository;
            this.userRepository = userRepository;
            _accessor = accessor;
        }
        public async Task<long> Create(ForumComment inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = forumCommentRepository.Insert(inputDto);
            return creation.ForumCommentId;
        }

        public async Task<ForumComment> Update(ForumComment item)
        {
            await forumCommentRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<ForumComment> forumComments = new List<ForumComment>();
            forumComments = forumCommentRepository.GetQuery().Where(z => z.Text.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var forumCommentsCount = forumComments.Count();
            if (pageNumber != 0 && count != 0)
            {
                forumComments = forumComments.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            var obj = new
            {
                ForumComments = forumComments,
                ForumCommentsCount = forumCommentsCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var forumComment = forumCommentRepository.GetQuery().FirstOrDefault(z => z.ForumCommentId == id);
                await forumCommentRepository.Delete(forumComment);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ForumComment> Get(long id)
        {
            var forumComment = forumCommentRepository.GetQuery().FirstOrDefault(z => z.ForumCommentId == id);
            return forumComment;
        }
        public async Task<object> TopCommenters(int count)
        {
            var dbComments = forumCommentRepository.GetQuery().ToList();
            var commentSort = from p in dbComments
                              group p by p.UserId into g
                              where g.Count() > 0
                              orderby g.Count() descending
                              select new { g.Key, myCount = g.Count() };

            var NewList = commentSort.Take(count).ToList();
            List<object> result = new List<object>();
            foreach(var comment in NewList)
            {
                var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == comment.Key);
                var obj = new
                {
                    UserInfo = dbUser,
                    CommentsCount = comment.myCount
                };
                result.Add(obj);
            }
            return result;
        }
    }
}
