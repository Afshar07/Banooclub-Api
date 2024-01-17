using BanooClub.Models;
using BanooClub.Services.FollowRequestServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FollowRequestController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFollowRequestService followRequestService;

        public FollowRequestController(BanooClubDBContext context, IFollowRequestService followRequestService)
        {
            this.context = context;
            this.followRequestService = followRequestService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Create(long followingUserId)
        {
            return await followRequestService.Create(followingUserId);
        }

        [HttpPost]
        [Route("[action]")]
        public List<FollowRequest> GetByUserId()
        {
            return followRequestService.GetByUserId();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<FollowRequest> Get(long followRequestId)
        {
            return followRequestService.Get(followRequestId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long followRequestId)
        {
            await followRequestService.Delete(followRequestId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<bool> QuestRespond(long followRequestId, bool Respond)
        {
            return this.followRequestService.QuestRespond(followRequestId, Respond);
        }
    }
}
