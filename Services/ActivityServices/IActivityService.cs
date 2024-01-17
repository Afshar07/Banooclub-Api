using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ActivityServices
{
    public interface IActivityService
    {
        Task Create(Activity inputDto);
        Task<Activity> Update(Activity item);
        Task Delete(long id);
        Task<List<Activity>> GetAll();
        Task<Activity> Get(long id);
        object GetByUserId(long userId, int pageNumber, int count);
        object GetMyActivities(int pageNumber, int count);
    }
}
