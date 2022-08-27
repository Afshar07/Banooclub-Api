using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.Common;
using BanooClub.Services.MediaServices;
using BanooClub.Services.SocialMediaServices;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private readonly IBanooClubEFRepository<Blog> _BlogRepository;
        private readonly IBanooClubEFRepository<BlogCategory> _BlogCategoryRepository;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        private readonly ISocialMediaService _mediaService;
        private readonly IBanooClubEFRepository<BlogComment> _blogCommentRepository;
        private readonly IBanooClubEFRepository<Tag> _tagRepository;
        private readonly IBanooClubEFRepository<View> viewRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<Like> likeRepository;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;
        public BlogService(IBanooClubEFRepository<Blog> BlogRepository, IBanooClubEFRepository<Like> likeRepository, IBanooClubEFRepository<User> userRepository, IBanooClubEFRepository<View> viewRepository, IHttpContextAccessor accessor, IUserService userService, IBanooClubEFRepository<Tag> tagRepository, IBanooClubEFRepository<BlogComment> blogCommentRepository, ISocialMediaService mediaService, IBanooClubEFRepository<BlogCategory> BlogCategoryRepository, IBanooClubEFRepository<SocialMedia> mediaRepository)
        {
            _BlogRepository = BlogRepository;
            _BlogCategoryRepository = BlogCategoryRepository;
            _mediaRepository = mediaRepository;
            _mediaService = mediaService;
            _blogCommentRepository = blogCommentRepository;
            _tagRepository = tagRepository;
            _userService = userService;
            _accessor = accessor;
            this.viewRepository = viewRepository;
            this.userRepository = userRepository;
            this.likeRepository = likeRepository;
        }

        public IServiceResult<object> GetAll(string categoryName, long? tagId, string searchByTitle, int pageNumber, int count, DateTime? date)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var blogs = _BlogRepository.GetQuery().Where(x => x.Status == BlogStatus.Published);
            if (date != null)
            {
                blogs = blogs.Where(z => z.CreateDate.Date == date.Value.Date);
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                var categoryId = _BlogCategoryRepository.GetQuery().FirstOrDefault(z => z.SEOURL.Trim().Equals(categoryName.Trim())).BlogCategoryId;
                blogs = blogs.Where(z => z.BlogCategoryId == categoryId);
            }
            if (tagId != 0)
            {
                var blogtags = _tagRepository.GetQuery().Where(z => z.TagId == tagId).Select(x => x.ObjectId).ToList();
                blogs = blogs.Where(z => blogtags.Contains(z.BlogId));
            }
            if (!string.IsNullOrEmpty(searchByTitle))
            {
                blogs = blogs.Where(z => z.Title.Contains(searchByTitle));
            }
            var dbcounts = blogs.Count();
            var dbBlogs = blogs.OrderByDescending(x => x.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            foreach (var item in dbBlogs)
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.BlogId && z.Type == MediaTypes.Blog);
                item.FileData = dbMedia == null ? "" : "media/gallery/blog/" + dbMedia.PictureUrl;
                item.CommentsCount = _blogCommentRepository.GetQuery().Where(z => z.BlogId == item.BlogId).Count();
                item.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == item.UserId);
                var blogTags = _tagRepository.GetQuery().Where(z => z.ObjectId == item.BlogId && z.Type == TagType.Blog).ToList();
                List<Tag> BLOGTAGS = new List<Tag>();
                //foreach (var blogTag in blogTags)
                //{
                //    var dbTag = _tagRepository.GetQuery().FirstOrDefault(z => z.TagId == blogTag.TagId);
                //    var BT = new BlogTagDTO() { TagId = dbTag.TagId, TagName = dbTag.Value, BlogTagId = blogTag.BlogTagId };
                //    BLOGTAGS.Add(BT);
                //}
                item.Tags = new List<Tag>();
                item.Tags = blogTags;

                var dbViews = viewRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.BlogId && z.Type == ViewType.Blog);
                if (dbViews != null)
                {
                    item.ViewsCount = dbViews.Count;
                }
                else
                {
                    item.ViewsCount = 0;
                }

                if (userId != 0)
                {
                    var dbLike = likeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.Type == LikeType.Blog && z.ObjectId == item.BlogId);

                    if (dbLike != null)
                    {
                        item.MyLikeStatus = (LikeStatus)dbLike.Status;
                    }
                    else
                    {
                        item.MyLikeStatus = null;
                    }
                }
                item.LikeCount = likeRepository.GetQuery().Where(z => z.Type == LikeType.Blog && z.ObjectId == item.BlogId).Count();
            }

            var obj = new
            {
                Blogs = dbBlogs,
                BlogsCount = dbcounts
            };
            return new ServiceResult<object>().Ok(obj);
        }


        public async Task<IServiceResult> DeleteBlog(long id)
        {
            var Blog = _BlogRepository.GetQuery().FirstOrDefault(z => z.BlogId == id);

            await _BlogRepository.Delete(Blog);
            var blogComments = _blogCommentRepository.GetQuery().Where(z => z.BlogId == id).ToList();
            foreach (var cmnt in blogComments)
            {
                await _blogCommentRepository.Delete(cmnt);
            }
            return new ServiceResult().Ok();
        }

        public async Task<IServiceResult<long>> CreateBlog(Blog model)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            model.CreateDate = DateTime.Now;
            model.UserId = userId;
            model.SEOURL = model.Title.Trim().Replace(' ', '-');
            model.SEODescription = model.Description;
            model.SEOTitle = model.Title;
            var result = _BlogRepository.Insert(model);
            if (!string.IsNullOrEmpty(model.FileData))
            {
                var outPut = _mediaService.SaveImage(model.FileData, EntityUrls.Blog);
                SocialMedia dbMedia = new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = result.BlogId,
                    PictureUrl = outPut.ImageName,
                    Type = MediaTypes.Blog,
                    MediaId = 0,
                    Priority = 0,
                    UpdateDate = DateTime.Now,
                };
                await _mediaRepository.InsertAsync(dbMedia);
            }
            foreach (var tag in model.Tags)
            {
                _tagRepository.Insert(new Tag()
                {
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    ObjectId = result.BlogId,
                    TagId = 0,
                    Title = tag.Title,
                    Type = TagType.Service

                });
            }

            return new ServiceResult<long>().Ok(model.BlogId);
        }

        public async Task<IServiceResult<Blog>> UpdateBlog(Blog model)
        {
            await _BlogRepository.Update(model);
            var dbLastMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == model.BlogId && z.Type == MediaTypes.Blog);
            if (model.FileData.ToUpper() == "DELETE")
            {
                dbLastMedia.PictureUrl = "";
                await _mediaRepository.Update(dbLastMedia);
                await _mediaRepository.Delete(dbLastMedia);
            }
            else if (!string.IsNullOrEmpty(model.FileData))
            {
                var outPut = _mediaService.SaveImage(model.FileData, EntityUrls.Blog);
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
                        ObjectId = model.BlogId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Blog,
                        MediaId = 0,
                        UpdateDate = DateTime.Now,
                        Priority = 0
                    };
                    await _mediaRepository.InsertAsync(dbMedia);
                }
            }
            return new ServiceResult<Blog>().Ok(model);
        }

        public async Task<IServiceResult<Blog>> UpdateBlogSEO(Blog model)
        {
            var dbBlog = _BlogRepository.GetQuery().FirstOrDefault(z => z.BlogId == model.BlogId);
            dbBlog.SEODescription = model.SEODescription != null ? model.SEODescription : dbBlog.SEODescription;
            dbBlog.SEOTitle = model.SEOTitle != null ? model.SEOTitle : dbBlog.SEOTitle;
            dbBlog.SEOURL = model.SEOURL != null ? model.SEOURL : dbBlog.SEOURL;
            await _BlogRepository.Update(dbBlog);

            return new ServiceResult<Blog>().Ok(model);
        }

        public async Task<IServiceResult<Blog>> GetByName(string blogName)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var id = _BlogRepository.GetQuery().FirstOrDefault(z => z.SEOURL.Trim().Equals(blogName.Trim()) && z.Status == BlogStatus.Published).BlogId;

            var dbView = viewRepository.GetQuery().FirstOrDefault(z => /*z.UserId == userId &&*/ z.Type == ViewType.Blog && z.ObjectId == id);
            if (dbView == null)
            {
                var dbViewCreation = new View()
                {
                    IsDeleted = false,
                    Count = 1,
                    ObjectId = id,
                    Type = ViewType.Blog,
                    ViewId = 0,
                    CreateDate = DateTime.Now
                };
                viewRepository.Insert(dbViewCreation);
            }
            else
            {
                dbView.Count++;
                await viewRepository.Update(dbView);
            }
            var dbBlog = _BlogRepository.GetQuery().FirstOrDefault(z => z.BlogId == id);
            var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Blog);
            dbBlog.FileData = dbMedia == null ? "" : "media/gallery/blog/" + dbMedia.PictureUrl;
            dbBlog.CommentsCount = _blogCommentRepository.GetQuery().Where(z => z.BlogId == dbBlog.BlogId).ToList().Count();
            dbBlog.UserInfo = _userService.Get(dbBlog.UserId);
            var blogTags = _tagRepository.GetQuery().Where(z => z.ObjectId == id && z.Type == TagType.Blog).ToList();
            List<Tag> BLOGTAGS = new List<Tag>();
            //foreach (var blogTag in blogTags)
            //{
            //    var dbTag = _tagRepository.GetQuery().FirstOrDefault(z => z.TagId == blogTag.TagId);
            //    var BT = new BlogTagDTO() { TagId = dbTag.TagId, TagName = dbTag.Value, BlogTagId = blogTag.BlogTagId };
            //    BLOGTAGS.Add(BT);
            //}
            dbBlog.Tags = new List<Tag>();
            dbBlog.Tags = blogTags;
            var dbLike = likeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.Type == LikeType.Blog && z.ObjectId == id);

            if (dbLike != null)
            {
                dbBlog.MyLikeStatus = (LikeStatus)dbLike.Status;
            }
            else
            {
                dbBlog.MyLikeStatus = null;
            }
            dbBlog.LikeCount = likeRepository.GetQuery().Where(z => z.Type == LikeType.Blog && z.ObjectId == id).Count();
            return new ServiceResult<Blog>().Ok(dbBlog);
        }

        public async Task<IServiceResult<Blog>> GetById(long id)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var dbView = viewRepository.GetQuery().FirstOrDefault(z => /*z.UserId == userId &&*/ z.Type == ViewType.Blog && z.ObjectId == id);

            if (dbView == null)
            {
                var dbViewCreation = new View()
                {
                    IsDeleted = false,
                    Count = 1,
                    ObjectId = id,
                    Type = ViewType.Blog,
                    ViewId = 0,
                    CreateDate = DateTime.Now
                };
                viewRepository.Insert(dbViewCreation);
            }
            else
            {
                dbView.Count++;
                await viewRepository.Update(dbView);
            }
            var dbBlog = _BlogRepository.GetQuery().FirstOrDefault(z => z.BlogId == id);
            var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == id && z.Type == MediaTypes.Blog);
            dbBlog.FileData = dbMedia == null ? "" : "media/gallery/blog/" + dbMedia.PictureUrl;
            dbBlog.CommentsCount = _blogCommentRepository.GetQuery().Where(z => z.BlogId == dbBlog.BlogId).ToList().Count();
            dbBlog.UserInfo = _userService.Get(dbBlog.UserId);
            var blogTags = _tagRepository.GetQuery().Where(z => z.ObjectId == id && z.Type == TagType.Blog).ToList();
            List<Tag> BLOGTAGS = new List<Tag>();
            //foreach (var blogTag in blogTags)
            //{
            //    var dbTag = _tagRepository.GetQuery().FirstOrDefault(z => z.TagId == blogTag.TagId);
            //    var BT = new BlogTagDTO() { TagId = dbTag.TagId, TagName = dbTag.Value, BlogTagId = blogTag.BlogTagId };
            //    BLOGTAGS.Add(BT);
            //}
            dbBlog.Tags = new List<Tag>();
            dbBlog.Tags = blogTags;
            if (userId != 0)
            {
                var dbLike = likeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.Type == LikeType.Blog && z.ObjectId == id);

                if (dbLike != null)
                {
                    dbBlog.MyLikeStatus = (LikeStatus)dbLike.Status;
                }
                else
                {
                    dbBlog.MyLikeStatus = null;
                }
            }
            dbBlog.LikeCount = likeRepository.GetQuery().Where(z => z.Type == LikeType.Blog && z.ObjectId == id).Count();

            return new ServiceResult<Blog>().Ok(dbBlog);
        }
        public async Task<IServiceResult<object>> GetMostPopular(int pageNumber, int count)
        {
            var dbViews = viewRepository.GetQuery().Where(z => z.Type == ViewType.Blog).ToList();
            var views = from p in dbViews
                        group p by p.ObjectId into g
                        where g.Count() > 0
                        select new { g.Key, myCount = g.Count() };
            var result = views.ToList();
            var blogsCount = result.Count();
            var FinalResult = result.OrderByDescending(z => z.myCount).Skip((pageNumber - 1) * count).Take(count).Select(x => x.Key).ToList();
            var dbBlogs = _BlogRepository.GetQuery().Where(z => FinalResult.Contains(z.BlogId) && z.Status == BlogStatus.Published).ToList();
            foreach (var dbBlog in dbBlogs)
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == dbBlog.BlogId && z.Type == MediaTypes.Blog);
                dbBlog.FileData = dbMedia == null ? "" : "media/gallery/blog/" + dbMedia.PictureUrl;
                dbBlog.CommentsCount = _blogCommentRepository.GetQuery().Where(z => z.BlogId == dbBlog.BlogId).Count();
            }
            var obj = new
            {
                Blogs = dbBlogs,
                BlogsCount = blogsCount
            };
            return new ServiceResult<object>().Ok(obj); ;
        }
        public async Task<IServiceResult<object>> GetAllWithCategory()
        {
            var categories = await _BlogCategoryRepository.GetAll();
            var blogs = _BlogRepository.GetQuery().Where(x => x.Status == BlogStatus.Published).ToList();
            var blogIds = blogs.Select(p => p.BlogId).ToList();
            var medias = _mediaRepository.GetQuery().Where(z => blogIds.Contains(z.ObjectId) && z.Type == MediaTypes.Blog);

            var model2 = categories.Select(p =>

                new
                {
                    category = p,
                    Blogs = blogs.Where(o => o.BlogCategoryId == p.BlogCategoryId).Select(o =>
                        new
                        {
                            o.BlogCategoryId,
                            o.Title,
                            o.Summary,
                            o.BlogId,
                            o.Description,
                            image = medias.FirstOrDefault(m => m.ObjectId == o.BlogId)
                        })
                });
            return new ServiceResult<object>().Ok(model2);
        }

        public async Task<IServiceResult<bool>> ChangeBlogStatus(BlogStatus status, params long[] ids)
        {
            foreach (var id in ids)
            {
                var blog = _BlogRepository.GetQuery().Where(x => x.BlogId == id).FirstOrDefault();
                if (blog != null)
                    blog.Status = status;
            }

            await _BlogRepository.Save();
            return new ServiceResult<bool>().Ok(true);
        }

        public IServiceResult<object> GetAllArchived(short pageNumber, byte count, string searchCommand, long categoryId)
        {
            var userId = _accessor.HttpContext.User?.GetUserId() ?? 0;

            var blogs = _BlogRepository.GetQuery().Where(x => x.Status == BlogStatus.Archived);
            var dbBlogs = blogs.OrderByDescending(x => x.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            foreach (var item in dbBlogs)
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.BlogId && z.Type == MediaTypes.Blog);
                item.FileData = dbMedia == null ? "" : "media/gallery/blog/" + dbMedia.PictureUrl;
                item.CommentsCount = _blogCommentRepository.GetQuery().Where(z => z.BlogId == item.BlogId).Count();
                item.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == item.UserId);
                var blogTags = _tagRepository.GetQuery().Where(z => z.ObjectId == item.BlogId && z.Type == TagType.Blog).ToList();
                List<Tag> BLOGTAGS = new List<Tag>();

                item.Tags = blogTags;

                var dbViews = viewRepository.GetQuery().FirstOrDefault(z => z.ObjectId == item.BlogId && z.Type == ViewType.Blog);
                if (dbViews != null)
                {
                    item.ViewsCount = dbViews.Count;
                }
                else
                {
                    item.ViewsCount = 0;
                }

                if (userId != 0)
                {
                    var dbLike = likeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.Type == LikeType.Blog && z.ObjectId == item.BlogId);

                    if (dbLike != null)
                    {
                        item.MyLikeStatus = (LikeStatus)dbLike.Status;
                    }
                    else
                    {
                        item.MyLikeStatus = null;
                    }
                }
                item.LikeCount = likeRepository.GetQuery().Where(z => z.Type == LikeType.Blog && z.ObjectId == item.BlogId).Count();
            }

            var obj = new
            {
                Blogs = dbBlogs,
                BlogsCount = blogs.Count()
            };

            return new ServiceResult<object>().Ok(obj);
        }
    }
}
