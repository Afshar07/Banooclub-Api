using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.FollowerServices
{
    public interface IFollowerService
    {
        Task Create(Follower inputDto);
        Task<Follower> Update(Follower item);
        Task Delete(long id);
        void DeleteByFollowerUserId(long id);
        Task<List<Follower>> GetAll();
        Task<Follower> Get(long id);
        List<Follower> GetByUserId(long userId);
        List<Follower> GetMyFollowers();
    }
}
