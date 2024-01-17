using BanooClub.Models;
using BanooClub.Services.Common;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.BlogCategoryServices
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBanooClubEFRepository<BlogCategory> _BlogCategoryRepository;
        private readonly IBanooClubEFRepository<Blog> _BlogRepository;
        public BlogCategoryService(IBanooClubEFRepository<BlogCategory> BlogCategoryRepository, IBanooClubEFRepository<Blog> blogRepository)
        {
            _BlogCategoryRepository = BlogCategoryRepository;
            _BlogRepository = blogRepository;
        }

        public async Task<IServiceResult<List<BlogCategory>>> GetAll()
            => new ServiceResult<List<BlogCategory>>().Ok(await _BlogCategoryRepository.GetAll());

        public async Task<IServiceResult<long>> DeleteBlogCategory(long id)
        {
            var BlogCategory = _BlogCategoryRepository.GetQuery().FirstOrDefault(z => z.BlogCategoryId==id);
            var blogs = _BlogRepository.GetQuery().FirstOrDefault(z => z.BlogCategoryId == BlogCategory.BlogCategoryId);
            if (blogs == null)
            {
                await _BlogCategoryRepository.Delete(BlogCategory);
                return new ServiceResult<long>().Ok(id);
            }
            return new ServiceResult<long>().Ok(0);
        }

        public async Task<IServiceResult<long>> CreateBlogCategory(BlogCategory model)
        {
            model.SEOTitle = model.Name;
            model.SEOURL = model.Name.Trim().Replace(' ', '-');
            await _BlogCategoryRepository.InsertAsync(model);

            return new ServiceResult<long>().Ok(model.BlogCategoryId);
        }

        public async Task<IServiceResult<BlogCategory>> UpdateBlogCategory(BlogCategory model)
        {
            await _BlogCategoryRepository.Update(model);

            return new ServiceResult<BlogCategory>().Ok(model);
        }

        public async Task<IServiceResult<BlogCategory>> UpdateBlogCategorySEO(BlogCategory model)
        {
            var dbBlogCategory = _BlogCategoryRepository.GetQuery().FirstOrDefault(z => z.BlogCategoryId==model.BlogCategoryId);
            dbBlogCategory.SEOTitle = model.SEOTitle != null ? model.SEOTitle : dbBlogCategory.SEOTitle;
            dbBlogCategory.SEODescription = model.SEODescription != null ? model.SEODescription : dbBlogCategory.SEODescription;
            dbBlogCategory.SEOURL = model.SEOURL != null ? model.SEOURL : dbBlogCategory.SEOURL;
            await _BlogCategoryRepository.Update(dbBlogCategory);

            return new ServiceResult<BlogCategory>().Ok(model);
        }
    }
}
