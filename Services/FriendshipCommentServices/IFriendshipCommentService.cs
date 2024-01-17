using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.FriendshipCommentServices
{
    public interface IFriendshipCommentService
    {
        Task Create(FriendshipComment inputDto);
        Task<FriendshipComment> Update(FriendshipComment item);
        Task Delete(long id);
        Task<List<FriendshipComment>> GetAll();
        Task<FriendshipComment> Get(long id);
    }
}
