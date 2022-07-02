using Infrastructure;
using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ForumCategoryServices
{
    public class ForumCategoryService : IForumCategoryService
    {
        private readonly IBanooClubEFRepository<ForumCategory> forumRepository;
        public ForumCategoryService(IBanooClubEFRepository<ForumCategory> forumRepository)
        {
            this.forumRepository = forumRepository;
        }
        public async Task<long> Create(ForumCategory inputDto)
        {
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = forumRepository.Insert(inputDto);
            return creation.ForumCategoryId;
        }

        public async Task<ForumCategory> Update(ForumCategory item)
        {
            await forumRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<ForumCategory> forumCategories = new List<ForumCategory>();
            forumCategories = forumRepository.GetQuery().Where(z => z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var forumCategoriesCount = forumCategories.Count();
            if (pageNumber != 0 && count != 0)
            {
                forumCategories = forumCategories.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            var obj = new
            {
                ForumCategories = forumCategories,
                ForumCategoriesCount = forumCategoriesCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var forumCategory = forumRepository.GetQuery().FirstOrDefault(z => z.ForumCategoryId == id);
                await forumRepository.Delete(forumCategory);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ForumCategory> Get(long id)
        {
            var forumCategory = forumRepository.GetQuery().FirstOrDefault(z => z.ForumCategoryId == id);
            return forumCategory;
        }
    }
}
