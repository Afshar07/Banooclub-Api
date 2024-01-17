using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.FriendshipPostCommentServices
{
    public interface IFriendshipPostCommentService
    {
        Task Create(FriendshipPostComment inputDto);
        Task<FriendshipPostComment> Update(FriendshipPostComment item);
        Task Delete(long id);
        Task<List<FriendshipPostComment>> GetAll();
        Task<FriendshipPostComment> Get(long id);
    }
}
