using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using System;
using System.Threading.Tasks;

namespace BanooClub.Services.BlogServices
{
    public interface IBlogService
    {
        IServiceResult<object> GetAll(string categoryName, long? tagId, string searchByTitle, int pageNumber, int count, DateTime? date);
        IServiceResult<object> GetAllArchived(short pageNumber, byte count, string searchCommand, long categoryId);
        Task<IServiceResult> DeleteBlog(long id);
        Task<IServiceResult<long>> CreateBlog(Blog model);
        Task<IServiceResult<Blog>> UpdateBlog(Blog model);
        Task<IServiceResult<Blog>> UpdateBlogSEO(Blog model);
        Task<IServiceResult<Blog>> GetById(long id);
        Task<IServiceResult<Blog>> GetByName(string blogName);
        Task<IServiceResult<object>> GetAllWithCategory();
        Task<IServiceResult<object>> GetMostPopular(int pageNumber, int count);
        Task<IServiceResult<bool>> ChangeBlogStatus(BlogStatus status, params long[] ids);
    }
}
