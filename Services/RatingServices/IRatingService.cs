using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using System.Threading.Tasks;

namespace BanooClub.Services.RatingServices
{
    public interface IRatingService
    {
        Task<IServiceResult<RateOveral>> GetByObjectIdAndType(long objectId, RatingType type);
        IServiceResult<object> GetAll();
        Task<IServiceResult<bool>> DeleteProductRate(long id);
        Task<IServiceResult<long>> CreateObjectRate(int rate, long objectId, RatingType type);
        Task<IServiceResult<Rating>> UpdateProductRate(Rating model);
        Task<IServiceResult<RateOveral>> GetLastWeekByObjectIdAndType(long forumId, RatingType forum);
    }
}
