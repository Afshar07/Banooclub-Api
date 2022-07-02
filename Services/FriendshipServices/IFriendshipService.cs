using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.FriendshipServices
{
    public interface IFriendshipService
    {
        Task Create(Friendship inputDto);
        Task<Friendship> Update(Friendship item);
        Task Delete(long id);
        Task<List<Friendship>> GetAll();
        Task<Friendship> Get(long id);
    }
}
