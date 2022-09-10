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
using MoreLinq;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BanooClub.Services.ForumServices
{
    public class ForumService : IForumService
    {
        private const string cacheKey = "BanooClubLastNK";

        private readonly IBanooClubEFRepository<Forum> forumRepository;
        private readonly IBanooClubEFRepository<View> viewRepository;
        private readonly IBanooClubEFRepository<Rating> ratingRepository;
        private readonly IBanooClubEFRepository<Tag> tagRepository;
        private readonly IBanooClubEFRepository<ForumComment> commentRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly IBanooClubEFRepository<Like> likeRepository;
        private readonly IBanooClubEFRepository<PostNK> postNKRepository;
        private readonly IRatingService ratingService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IDistributedCache distributedCache;
       
        public ForumService(IBanooClubEFRepository<Forum> forumRepository, IBanooClubEFRepository<PostNK> postNKRepository, IDistributedCache distributedCache, IBanooClubEFRepository<Like> likeRepository, IBanooClubEFRepository<ForumComment> commentRepository, IBanooClubEFRepository<User> userRepository, IBanooClubEFRepository<Rating> ratingRepository, IBanooClubEFRepository<Tag> tagRepository
            , IHttpContextAccessor accessor, IBanooClubEFRepository<SocialMedia> mediaRepository, IBanooClubEFRepository<View> viewRepository, IRatingService ratingService)
        {
            this.forumRepository = forumRepository;
            this.viewRepository = viewRepository;
            this.ratingRepository = ratingRepository;
            this.ratingService = ratingService;
            this.tagRepository = tagRepository;
            this.userRepository = userRepository;
            this.commentRepository = commentRepository;
            this.mediaRepository = mediaRepository;
            this.likeRepository = likeRepository;
            this.distributedCache = distributedCache;
            this.postNKRepository = postNKRepository;
            _accessor = accessor;
        }
       
        public async Task<long> Create(Forum inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

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
            inputDto.UserId = userId;
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            inputDto.Status = ForumStatus.Active;

            var NKs = Nks.Select(z => z.Name).ToList();
            if (NKs.Any(z => (inputDto.Description.Contains(z))))
            {
                var textWithoutChars = Regex.Replace(inputDto.Description, "[^a-zA-Z0-9_]+", " ");

                foreach (var word in textWithoutChars.Split())
                {
                    if (NKs.Any(x => x.Equals(word)))
                    {
                        inputDto.Status = ForumStatus.ReportedByRobot;
                        break;
                    }
                }
            }
            var creation = forumRepository.Insert(inputDto);
            foreach (var tag in inputDto.Tags)
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

        public async Task<Forum> ReportForum(long forumId)
        {
            var dbForum = forumRepository.GetQuery().FirstOrDefault(z => z.ForumId == forumId);
            dbForum.Status = ForumStatus.ReportedByPerson;
            var dbUpdate = await forumRepository.Update(dbForum);
            return dbUpdate;
        }

        public async Task<Forum> ChangeForumStatus(long forumId, ForumStatus status)
        {
            var dbForum = forumRepository.GetQuery().FirstOrDefault(z => z.ForumId == forumId);
            dbForum.Status = status;
            var dbUpdate = await forumRepository.Update(dbForum);
            return dbUpdate;
        }

        public async Task<Forum> Update(Forum item)
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

            item.Status = ForumStatus.Active;
            var NKs = Nks.Select(z => z.Name).ToList();
            if (NKs.Any(z => (item.Description.Contains(z))))
            {
                var textWithoutChars = Regex.Replace(item.Description, "[^a-zA-Z0-9_]+", " ");

                foreach (var word in textWithoutChars.Split())
                {
                    if (NKs.Any(x => x.Equals(word)))
                    {
                        item.Status = ForumStatus.ReportedByRobot;
                        break;
                    }
                }
            }

            await forumRepository.Update(item);
            return item;
        }

        public async Task<object> GetAllForAdmin(int pageNumber, int count, string searchCommand, bool? noComments, bool? mostRated,
            bool? mostComments, ForumStatus? status, long? categoryId, bool? mostViewed)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<Forum> forums = new List<Forum>();
            forums = forumRepository.GetQuery().Where(z => z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            if (status != null)
            {
                forums = forums.Where(z => z.Status == status).ToList();
            }
            if (noComments != null)
            {
                mostViewed = false;
                mostComments = false;
                mostRated = false;

                var dbForumIds = commentRepository.GetQuery().DistinctBy(z => z.ForumId).Select(x => x.ForumId).ToList();
                if (noComments == true)
                {
                    forums = forums.Where(z => !dbForumIds.Contains(z.ForumId)).ToList();
                }
                else
                {
                    forums = forums.Where(z => dbForumIds.Contains(z.ForumId)).ToList();
                }
            }

            if (categoryId != null && categoryId > 0)
                forums = forums.Where(x => x.ForumCategoryId == categoryId).ToList();

            if (mostViewed != null && mostViewed is true)
            {
                mostRated = false;
                mostComments = false;

                var views = viewRepository.GetQuery()
                    .Where(x => x.Type == ViewType.Forum)
                    .Select(x => x.ObjectId);

                var sortedViews = from v in views
                                  group v by v into g
                                  where g.Count() > 0
                                  orderby g.Count()
                                  select new { g.Key }.Key;

                forums = forums.OrderBy(x => sortedViews.ToList().IndexOf(x.ForumId)).ToList();
            }

            if (mostRated != null && mostRated == true)
            {
                mostComments = false;

                var dbRatingIds = ratingRepository.GetQuery().Where(z => z.Type == RatingType.Forum).OrderByDescending(x => x.Rate).Select(x => x.ObjectId).ToList();
                forums = forums.OrderBy(d => dbRatingIds.IndexOf(d.ForumId)).ToList();
            }
            if (mostComments != null && mostComments == true)
            {
                var dbComments = commentRepository.GetQuery().ToList();
                var commentSort = from p in dbComments
                                  group p by p.ForumId into g
                                  where g.Count() > 0
                                  orderby g.Count()
                                  select new { g.Key }.Key;
                forums = forums.OrderBy(d => commentSort.ToList().IndexOf(d.ForumId)).ToList();
            }

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
                item.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == item.UserId);
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.UserId && z.Type == MediaTypes.Profile);
                item.UserInfo.SelfieFileData = dbMedia != null ? dbMedia.PictureUrl : "";

                item.Tags = tagRepository.GetQuery().Where(z => z.ObjectId == item.ForumId && z.Type == TagType.Forum).ToList();

                var dbLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == item.ForumId && z.Status == LikeStatus.Like).Count();
                var dbDisLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == item.ForumId && z.Status == LikeStatus.DisLike).Count();
                item.Vote = dbLikes - dbDisLikes;
            }

            var obj = new
            {
                Forums = forums,
                ForumsCount = forumsCount,
            };
            return obj;
        }

        public async Task<object> GetAll(int pageNumber, int count, string searchCommand, bool? noComments,
            bool? mostRated, bool? mostComments, long? categoryId, bool? mostViewed)
        {
            if (searchCommand == null)
                searchCommand = "";

            List<Forum> forums = new List<Forum>();
            forums = forumRepository.GetQuery().Where(z => z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            if (noComments != null)
            {
                mostViewed = false;
                mostComments = false;
                mostRated = false;

                var dbForumIds = commentRepository.GetQuery().DistinctBy(z => z.ForumId).Select(x => x.ForumId).ToList();
                if (noComments == true)
                {
                    forums = forums.Where(z => !dbForumIds.Contains(z.ForumId)).ToList();
                }
                else
                {
                    forums = forums.Where(z => dbForumIds.Contains(z.ForumId)).ToList();
                }
            }

            if (categoryId != null && categoryId > 0)
                forums = forums.Where(x => x.ForumCategoryId == categoryId).ToList();

            if (mostViewed != null && mostViewed is true)
            {
                mostRated = false;
                mostComments = false;

                var views = viewRepository.GetQuery()
                    .Where(x => x.Type == ViewType.Forum)
                    .Select(x => x.ObjectId);

                var sortedViews = from v in views
                                  group v by v into g
                                  where g.Count() > 0
                                  orderby g.Count()
                                  select new { g.Key }.Key;

                forums = forums.OrderBy(x => sortedViews.ToList().IndexOf(x.ForumId)).ToList();
            }

            if (mostRated != null && mostRated == true)
            {
                mostComments = false;

                var dbRatingIds = ratingRepository.GetQuery().Where(z => z.Type == RatingType.Forum).OrderByDescending(x => x.Rate).Select(x => x.ObjectId).ToList();
                forums = forums.OrderBy(d => dbRatingIds.IndexOf(d.ForumId)).ToList();
            }
            if (mostComments != null && mostComments == true)
            {
                var dbComments = commentRepository.GetQuery().ToList();
                var commentSort = from p in dbComments
                                  group p by p.ForumId into g
                                  where g.Count() > 0
                                  orderby g.Count()
                                  select new { g.Key }.Key;
                forums = forums.OrderBy(d => commentSort.ToList().IndexOf(d.ForumId)).ToList();
            }

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
                item.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == item.UserId);
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.UserId && z.Type == MediaTypes.Profile);
                item.UserInfo.SelfieFileData = dbMedia != null ? dbMedia.PictureUrl : "";

                item.Tags = tagRepository.GetQuery().Where(z => z.ObjectId == item.ForumId && z.Type == TagType.Forum).ToList();

                var dbLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == item.ForumId && z.Status == LikeStatus.Like).Count();
                var dbDisLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == item.ForumId && z.Status == LikeStatus.DisLike).Count();
                item.Vote = dbLikes - dbDisLikes;
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
            forums = forumRepository.GetQuery().Where(z => z.UserId == userId && z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
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
                item.Tags = tagRepository.GetQuery().Where(z => z.ObjectId == item.ForumId && z.Type == TagType.Forum).ToList();
                var dbCommentLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == item.ForumId && z.Status == LikeStatus.Like).Count();
                var dbCommentDisLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == item.ForumId && z.Status == LikeStatus.DisLike).Count();
                item.Vote = dbCommentLikes - dbCommentDisLikes;
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
                //var forum = forumRepository.GetQuery().FirstOrDefault(z => z.ForumId == id);
                //await forumRepository.Delete(forum);

                var cmnd = $" Update Social.ForumComments set IsDeleted = 1 where ForumId = {id} " +
                    $"Update Common.tags set IsDeleted = 1  where Type = 2 and ObjectId = {id} " +
                    $"Update Social.Forums set IsDeleted = 1  where ForumId ={id} ";
                await forumRepository.DapperSqlQuery(cmnd);


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

            var dbLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == id && z.Status == LikeStatus.Like).Count();
            var dbDisLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == id && z.Status == LikeStatus.DisLike).Count();
            forum.Vote = dbLikes - dbDisLikes;

            var dbView = viewRepository.GetQuery().FirstOrDefault(z => z.Type == ViewType.Forum && z.ObjectId == id);
            if (dbView == null)
            {
                var dbViewCreation = new View()
                {
                    IsDeleted = false,
                    Count = 1,
                    ObjectId = id,
                    Type = ViewType.Forum,
                    ViewId = 0,
                    CreateDate = DateTime.Now
                };
                dbView = viewRepository.Insert(dbViewCreation);
            }
            else
            {
                dbView.Count++;
                await viewRepository.Update(dbView);
            }
            forum.ViewsCount = dbView.Count;

            forum.Rate = null;
            if (userId > 0)
            {
                var dbProductRate = ratingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId == id && z.Type == RatingType.Forum);
                forum.Rate = dbProductRate == null ? null : (int?)dbProductRate.Rate;
            }
            forum.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == forum.UserId);
            var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == forum.UserId && z.Type == MediaTypes.Profile);
            forum.UserInfo.SelfieFileData = dbMedia != null ? dbMedia.PictureUrl : "";

            forum.Tags = tagRepository.GetQuery().Where(z => z.Type == TagType.Forum && z.ObjectId == id).ToList();

            forum.Comments = commentRepository.GetQuery().Where(z => z.ForumId == id).ToList();
            foreach (var comment in forum.Comments)
            {
                var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == comment.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == dbUser.UserId && z.Type == MediaTypes.Profile);
                dbUser.Password = null;
                if (dbUserMedia != null)
                {
                    dbUser.SelfieFileData = dbUserMedia.PictureUrl;
                }
                comment.UserInfo = dbUser;

                var dbCommentLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.ForumComment && z.ObjectId == comment.ForumCommentId && z.Status == LikeStatus.Like).Count();
                var dbCommentDisLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.ForumComment && z.ObjectId == comment.ForumCommentId && z.Status == LikeStatus.DisLike).Count();
                comment.Vote = dbCommentLikes - dbCommentDisLikes;
            }

            return forum;
        }

        public async Task<object> GetByUserId(long userId, byte count, short pageNumber)
        {
            var query = forumRepository.GetQuery()
                .Where(x => x.UserId == userId);

            var forums = query.OrderByDescending(x => x.CreateDate)
                .Skip((pageNumber - 1) * count).Take(count).ToList();

            forums.ForEach(x =>
            {
                x.Rate = ratingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId == x.ForumId && z.Type == RatingType.Forum)?.Rate ?? null;
                x.ViewsCount = viewRepository.GetQuery().FirstOrDefault(z => z.Type == ViewType.Forum && z.ObjectId == x.ForumId)?.Count ?? 0;
                var dbLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == x.ForumId && z.Status == LikeStatus.Like).Count();
                var dbDisLikes = likeRepository.GetQuery().Where(z => z.Type == LikeType.Forum && z.ObjectId == x.ForumId && z.Status == LikeStatus.DisLike).Count();
                x.Vote = dbLikes - dbDisLikes;
            });

            var data = new
            {
                Forums = forums,
                ForumCount = query.Count()
            };

            return data;
        }
    }
}
