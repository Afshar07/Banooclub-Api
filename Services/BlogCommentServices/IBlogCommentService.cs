using BanooClub.Models;
using BanooClub.Services.Common;
using System.Threading.Tasks;

namespace BanooClub.Services.BlogCommentServices
{
    public interface IBlogCommentService
    {
        Task<IServiceResult<object>> GetAll(int pageNumber, int count, string searchCommand, bool? IsActive);
        Task<IServiceResult> DeleteBlogComment(long id);
        Task<IServiceResult<long>> CreateBlogComment(long userId, string message, long blogId, long? parentId);
        Task<IServiceResult<BlogComment>> UpdateBlogComment(BlogComment model);
        IServiceResult<object> GetAllAsyncByBlogId(long blogId);
        Task<IServiceResult<object>> ChangeCommentActivation(long blogCommnetId);
    }
}
