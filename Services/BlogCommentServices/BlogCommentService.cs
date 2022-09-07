using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using BanooClub.Services.UserServices;
using Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.BlogCommentServices
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IBanooClubEFRepository<BlogComment> _BlogCommentRepository;
        private readonly IBanooClubEFRepository<Blog> _blogRepository;
        private readonly IBanooClubEFRepository<SocialMedia> _MediaRepository;
        private readonly IUserService _userService;
        private readonly IBanooClubEFRepository<User> _userRepository;

        public BlogCommentService(IBanooClubEFRepository<BlogComment> BlogCommentRepository, IBanooClubEFRepository<SocialMedia> mediaRepository, IBanooClubEFRepository<User> userRepository, IUserService userService, IBanooClubEFRepository<Blog> blogRepository)
        {
            _BlogCommentRepository = BlogCommentRepository;
            _userService = userService;
            _userRepository = userRepository;
            _MediaRepository = mediaRepository;
            _blogRepository = blogRepository;
        }

        public async Task<IServiceResult<object>> GetAll(int pageNumber, int count, string searchCommand, bool? IsActive)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            var dbBlogComments = _BlogCommentRepository.GetQuery().Where(z => z.Name.Contains(searchCommand) || z.Message.Contains(searchCommand)).OrderByDescending(x => x.BlogCommentId).ToList();
            if (IsActive != null)
            {
                dbBlogComments = dbBlogComments.Where(z => z.IsActive == IsActive).ToList();
            }
            var dbCmntsCount = dbBlogComments.Count();
            if (pageNumber != 0 && count != 0)
            {
                dbBlogComments = dbBlogComments.Skip((pageNumber - 1) * count).Take(count).ToList();
            }

            var blogs = _blogRepository.GetQuery()
                .Select(x => new
                {
                    x.BlogId,
                    x.Title,
                    x.SEOURL
                }).ToList();

            foreach (var blogComment in dbBlogComments)
            {
                var dbUser = _userRepository.GetQuery().FirstOrDefault(z => z.UserId == blogComment.UserId);
                if (dbUser != null)
                {
                    var dbMedia = _MediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.Profile && z.ObjectId == dbUser.UserId);
                    dbUser.SelfieFileData = dbMedia == null ? "" : dbMedia.PictureUrl;
                    blogComment.UserInfo = dbUser;
                }

                blogComment.BlogTitle = blogs?.FirstOrDefault(x => x.BlogId == blogComment.BlogId)?.Title;
                blogComment.BlogSEOURL = blogs?.FirstOrDefault(x => x.BlogId == blogComment.BlogId)?.SEOURL;
            }

            var obj = new
            {
                Comments = dbBlogComments,
                CommentsCount = dbCmntsCount
            };

            return new ServiceResult<object>().Ok(obj);
        }

        public async Task<IServiceResult> DeleteBlogComment(long id)
        {
            var BlogComment = _BlogCommentRepository.GetQuery().FirstOrDefault(z => z.BlogCommentId == id);

            await _BlogCommentRepository.Delete(BlogComment);

            return new ServiceResult().Ok();
        }

        public async Task<IServiceResult<long>> CreateBlogComment(long userId, string message, long blogId, long? parentId)
        {
            var dbUser = _userRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            var model = new BlogComment { UserId = userId, BlogId = blogId, Message = message, Name = dbUser.UserName, ParentId = parentId, IsActive = false, IsDeleted = false };
            await _BlogCommentRepository.InsertAsync(model);

            return new ServiceResult<long>().Ok(model.BlogCommentId);
        }



        public async Task<IServiceResult<BlogComment>> UpdateBlogComment(BlogComment model)
        {
            await _BlogCommentRepository.Update(model);
            return new ServiceResult<BlogComment>().Ok(model);
        }

        public IServiceResult<object> GetAllAsyncByBlogId(long blogId)
        {
            var dbComments = _BlogCommentRepository.GetQuery().Where(z => z.BlogId == blogId).OrderByDescending(z=>z.BlogCommentId).ToList();
            foreach (var comment in dbComments)
            {
                comment.UserInfo = _userService.Get(comment.UserId);
            }
            return new ServiceResult<object>().Ok(dbComments);
        }
        public async Task<IServiceResult<object>> ChangeCommentActivation(long blogCommnetId)
        {
            var dbComment = _BlogCommentRepository.GetQuery().FirstOrDefault(z => z.BlogCommentId == blogCommnetId);
            dbComment.IsActive = dbComment.IsActive == true ? false : true;
            await _BlogCommentRepository.Update(dbComment);
            return new ServiceResult<object>().Ok(dbComment);
        }
    }
}
