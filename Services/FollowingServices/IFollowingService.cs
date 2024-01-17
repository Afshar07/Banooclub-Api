using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.FollowingServices
{
    public interface IFollowingService
    {
        Task Create(long followingUserId);
        Task<Following> Update(Following item);
        Task Delete(long id);
        void DeleteByFollowingUserId(long id);
        Task<List<Following>> GetAll();
        Task<Following> Get(long id);
        List<Following> GetByUserId(long userId);
        List<Following> GetMyFollowings();
        bool IsFollowing(long userId, long followingUserId);
    }
}
