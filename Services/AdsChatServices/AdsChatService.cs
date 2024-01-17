using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsChatServices
{
    public class AdsChatService : IAdsChatService
    {
        private readonly IBanooClubEFRepository<AdsChat> adsChatRepository;

        public AdsChatService(IBanooClubEFRepository<AdsChat> adsChatRepository)
        {
            this.adsChatRepository = adsChatRepository;
        }
        public async Task Create(AdsChat inputDto)
        {
            adsChatRepository.Insert(inputDto);
        }

        public async Task<AdsChat> Update(AdsChat item)
        {
            await adsChatRepository.Update(item);
            return item;
        }



        public object GetAll(int pageNumber,int count)
        {
            List<AdsChat> dbAds = new List<AdsChat>();
            dbAds = adsChatRepository.GetQuery().Skip((pageNumber-1)*count).Take(count).ToList();
            var adsChatCount= adsChatRepository.GetQuery().Count();
            var obj = new
            {
                AdsChatCount = adsChatCount,
                AdsChat = dbAds
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var dbAds = adsChatRepository.GetQuery().FirstOrDefault(z => z.AdsChatId == id);
            await adsChatRepository.Delete(dbAds);
        }

        public async Task<AdsChat> Get(long id)
        {
            var dbAds = adsChatRepository.GetQuery().FirstOrDefault(z => z.AdsChatId == id);
            return dbAds;
        }

    }
}
