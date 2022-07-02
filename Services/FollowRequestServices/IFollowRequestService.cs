using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.FollowRequestServices
{
    public interface IFollowRequestService
    {
        Task<bool> Create(long followingUserId);
        Task<FollowRequest> Update(FollowRequest item);
        Task Delete(long id);
        Task<List<FollowRequest>> GetAll();
        Task<FollowRequest> Get(long id);
        List<FollowRequest> GetByUserId();
        Task<bool> QuestRespond(long followRequestId, bool Respond);
    }
}
