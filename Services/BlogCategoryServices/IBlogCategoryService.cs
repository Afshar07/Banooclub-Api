using BanooClub.Models;
using BanooClub.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.BlogCategoryServices
{
    public interface IBlogCategoryService
    {
        Task<IServiceResult<List<BlogCategory>>> GetAll();
        Task<IServiceResult<long>> DeleteBlogCategory(long id);
        Task<IServiceResult<long>> CreateBlogCategory(BlogCategory model);
        Task<IServiceResult<BlogCategory>> UpdateBlogCategory(BlogCategory model);
        Task<IServiceResult<BlogCategory>> UpdateBlogCategorySEO(BlogCategory model);
    }
}
