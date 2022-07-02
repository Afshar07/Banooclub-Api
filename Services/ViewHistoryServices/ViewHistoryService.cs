using BanooClub.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ViewHistoryServices
{
    public class ViewHistoryService : IViewHistoryService
    {
        private readonly IBanooClubEFRepository<ViewHistory> viewHistoryRepository;

        public ViewHistoryService(IBanooClubEFRepository<ViewHistory> viewHistoryRepository)
        {
            this.viewHistoryRepository = viewHistoryRepository;
        }
        public async Task Create(ViewHistory inputDto)
        {
            inputDto.ViewHistoryId = 0;
            inputDto.IsDeleted = false;
            inputDto.UpdateDate = DateTime.Now;
            viewHistoryRepository.Insert(inputDto);
            await viewHistoryRepository.Save();
        }

        public async Task<ViewHistory> Update(ViewHistory item)
        {
            await viewHistoryRepository.Update(item);
            return item;
        }

        public async Task<List<ViewHistory>> GetAll()
        {
            List<ViewHistory> reviews = new List<ViewHistory>();
            reviews = viewHistoryRepository.GetQuery().ToList();
            return reviews;
        }

        public async Task Delete(long id)
        {
            var review = viewHistoryRepository.GetQuery().FirstOrDefault(z => z.ViewHistoryId == id);
            await viewHistoryRepository.Delete(review);
        }

        public async Task<ViewHistory> Get(long id)
        {
            var review = viewHistoryRepository.GetQuery().FirstOrDefault(z => z.ViewHistoryId == id);
            return review;
        }
        public List<ViewHistory> GetByUserId(long userId)
        {
            return viewHistoryRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }
    }
}
