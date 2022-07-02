using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsCategoryServices
{
    public class AdsCategoryService : IAdsCategoryService
    {
        private readonly IBanooClubEFRepository<AdsCategory> adsCategoryRepository;

        public AdsCategoryService(IBanooClubEFRepository<AdsCategory> adsCategoryRepository)
        {
            this.adsCategoryRepository = adsCategoryRepository;
        }
        public async Task Create(AdsCategory inputDto)
        {
            adsCategoryRepository.Insert(inputDto);
        }

        public async Task<AdsCategory> Update(AdsCategory item)
        {
            await adsCategoryRepository.Update(item);
            return item;
        }



        public object GetAll(int pageNumber,int count, string search)
        {
            var AdsCount = adsCategoryRepository.GetQuery().Where(z=>z.Name.Contains(search)).Count();
            if (pageNumber ==0 && count ==0)
            {
                string cmd= "select Main.AdsCategoryId as MainAdsCategoryId,Main.Name as MainName, " +
                    " Parent.AdsCategoryId as ParentId,Parent.Name as ParentName from Service.AdsCategories as Main " +
                    $" left join Service.AdsCategories as Parent on Main.ParentId = Parent.AdsCategoryId where Main.IsDeleted = 0 and Main.Name like N'%{search}%' order by Main.AdsCategoryId DESC";
                var AdsCats = adsCategoryRepository.DapperSqlQuery(cmd).Result;
                var obj = new
                {
                    AdsCategories = AdsCats,
                    AdsCategoryCount = AdsCount
                };
                return obj;
            }
            else
            {
                string cmd = "select Main.AdsCategoryId as MainAdsCategoryId,Main.Name as MainName, " +
                    " Parent.AdsCategoryId as ParentId,Parent.Name as ParentName from Service.AdsCategories as Main " +
                    $" left join Service.AdsCategories as Parent on Main.ParentId = Parent.AdsCategoryId where Main.IsDeleted = 0 and Main.Name like N'%{search}%' " +
                    $" order by Main.AdsCategoryId DESC OFFSET {(pageNumber-1)*count} Rows FETCH NEXT {count} ROWS ONLY";
                var AdsCats = adsCategoryRepository.DapperSqlQuery(cmd).Result;
                var obj = new
                {
                    AdsCategories = AdsCats,
                    AdsCategoryCount = AdsCount
                };
                return obj;
            }
        }

        public async Task<bool> Delete(long id)
        {
            var dbAds = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == id);
            var child=adsCategoryRepository.GetQuery().FirstOrDefault(z=>z.ParentId==dbAds.AdsCategoryId);
            if(child !=null)
            {
                return false;
            }
            await adsCategoryRepository.Delete(dbAds);
            return true;
        }

        public async Task<AdsCategory> Get(long id)
        {
            var dbAds = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == id);
            return dbAds;
        }

    }
}
