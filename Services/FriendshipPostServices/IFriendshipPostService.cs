using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.FriendshipPostServices
{
    public interface IFriendshipPostService
    {
        Task Create(FriendshipPost inputDto);
        Task<FriendshipPost> Update(FriendshipPost item);
        Task Delete(long id);
        Task<List<FriendshipPost>> GetAll();
        Task<FriendshipPost> Get(long id);
    }
}
