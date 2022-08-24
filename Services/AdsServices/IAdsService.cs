using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsServices
{
    public interface IAdsService
    {
        Task Create(Ads inputDto);
        Task<Ads> Update(Ads item);
        Task<bool> Delete(long id);
        Task<object> GetAll(int pageNumber, int count, string search);
        Task<Ads> Get(long id);
        Task<object> GetByUserId(long userId, int pageNumber, int count);
        Task<object> GetMyAds(int pageNumber, int count);
        Task<object> GetNotConfirmed(int pageNumber, int count, string search);
        Task<Ads> ChangeAdsStatus(Ads item);
        Task<object> GetRejected(int pageNumber, int count, string search);
        Task<object> GetAdsByCategory(long categoryId, long firstSearchadsId, int count);
        Task<bool> Ladder(long AdsId);
        Task<object> GetAdsByFilter(long? priceFrom, long? priceTo, string title, string tag,
            long? city, long? state, long firstSearchadsId, int count,long? categoryId, int planType, bool? exchangeability = null);

    }
}
