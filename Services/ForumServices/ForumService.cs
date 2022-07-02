using Infrastructure;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.RatingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ForumServices
{
    public class ForumService : IForumService
    {
        private readonly IBanooClubEFRepository<Forum> forumRepository;
        private readonly IBanooClubEFRepository<View> viewRepository;
        private readonly IBanooClubEFRepository<Rating> ratingRepository;
        private readonly IBanooClubEFRepository<Tag> tagRepository;
        private readonly IBanooClubEFRepository<ForumComment> commentRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly IRatingService ratingService;
        private readonly IHttpContextAccessor _accessor;
        public ForumService(IBanooClubEFRepository<Forum> forumRepository, IBanooClubEFRepository<ForumComment> commentRepository, IBanooClubEFRepository<User> userRepository, IBanooClubEFRepository<Rating> ratingRepository, IBanooClubEFRepository<Tag> tagRepository
            , IHttpContextAccessor accessor, IBanooClubEFRepository<SocialMedia> mediaRepository, IBanooClubEFRepository<View> viewRepository , IRatingService ratingService)
        {
            this.forumRepository = forumRepository;
            this.viewRepository = viewRepository;
            this.ratingRepository = ratingRepository;
            this.ratingService = ratingService;
            this.tagRepository = tagRepository;
            this.userRepository = userRepository;
            this.commentRepository = commentRepository;
            this.mediaRepository = mediaRepository;
            _accessor = accessor;
        }
        public async Task<long> Create(Forum inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = forumRepository.Insert(inputDto);


            foreach(var tag in inputDto.Tags)
            {
                tagRepository.Insert(new Tag()
                {
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    ObjectId = creation.ForumId,
                    TagId = 0,
                    Title = tag.Title,
                    Type = TagType.Forum
                });
            }
            return creation.ForumId;
        }

        public async Task<Forum> Update(Forum item)
        {
            await forumRepository.Update(item);
            return item;
        }

        public async Task<object> GetAll(int pageNumber, int count, string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<Forum> forums = new List<Forum>();
            forums = forumRepository.GetQuery().Where(z => z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var forumsCount = forums.Count();
            if (pageNumber != 0 && count != 0)
            {
                forums = forums.Skip((pageNumber - 1) * count).Take(count).ToList();
            }

            foreach(var item in forums)
            {
                var dbRate = await ratingService.GetByObjectIdAndType(item.ForumId, RatingType.Forum);
                item.Rate = dbRate.Data.Average;
                var dbView = viewRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.ForumId && z.Type == ViewType.Forum);
                item.ViewsCount = dbView == null ? 0 : dbView.Count;

            }

            var obj = new
            {
                Forums = forums,
                ForumsCount = forumsCount,
            };
            return obj;
        }

        public async Task<object> GetMyForums(int pageNumber, int count, string searchCommand)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<Forum> forums = new List<Forum>();
            forums = forumRepository.GetQuery().Where(z =>z.UserId == userId && z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var forumsCount = forums.Count();
            if (pageNumber != 0 && count != 0)
            {
                forums = forums.Skip((pageNumber - 1) * count).Take(count).ToList();
            }

            foreach (var item in forums)
            {
                var dbRate = await ratingService.GetByObjectIdAndType(item.ForumId, RatingType.Forum);
                item.Rate = dbRate.Data.Average;
                var dbView = viewRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.ForumId && z.Type == ViewType.Forum);
                item.ViewsCount = dbView == null ? 0 : dbView.Count;

            }

            var obj = new
            {
                Forums = forums,
                ForumsCount = forumsCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var forum = forumRepository.GetQuery().FirstOrDefault(z => z.ForumId == id);
                await forumRepository.Delete(forum);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Forum> Get(long id)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var forum = forumRepository.GetQuery().FirstOrDefault(z => z.ForumId == id);
            var dbView = viewRepository.GetQuery().FirstOrDefault(z => z.Type == ViewType.Forum && z.ObjectId ==id);
            if (dbView == null)
            {
                var dbViewCreation = new View()
                {
                    IsDeleted=false,
                    Count = 1,
                    ObjectId = id,
                    Type = ViewType.Forum,
                    ViewId = 0,
                    CreateDate = DateTime.Now
                };
                dbView =viewRepository.Insert(dbViewCreation);
            }
            else
            {
                dbView.Count++;
                await viewRepository.Update(dbView);
            }
            forum.ViewsCount = dbView.Count;

            forum.Rate = null;
            if (userId >0)
            {
                var dbProductRate = ratingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId==id && z.Type == RatingType.Forum);
                forum.Rate = dbProductRate == null ? null : (int?)dbProductRate.Rate;
            }
            forum.Tags = tagRepository.GetQuery().Where(z=>z.Type == TagType.Forum && z.ObjectId == id).ToList();

            forum.Comments = commentRepository.GetQuery().Where(z => z.ForumId == id).ToList();
            foreach(var comment in forum.Comments)
            {
                var dbUser = userRepository.GetQuery().FirstOrDefault(z=>z.UserId == comment.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == dbUser.UserId && z.Type == MediaTypes.Profile);
                dbUser.Password = null;
                if(dbUserMedia != null)
                {
                    dbUser.SelfieFileData = dbUserMedia.PictureUrl;
                }
                comment.UserInfo = dbUser;
            }

            return forum;
        }
    }
}
