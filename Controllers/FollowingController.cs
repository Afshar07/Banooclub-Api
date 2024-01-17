using BanooClub.Models;
using BanooClub.Services.FollowingServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFollowingService followingService;


        public FollowingController(BanooClubDBContext context, IFollowingService followingService)
        {
            this.context = context;
            this.followingService = followingService;
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public async Task Create(long followingUserId)
        {
            await this.followingService.Create(followingUserId);
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public List<Following> GetMyFollowings()
        {
            return followingService.GetMyFollowings();
        }
        [HttpPost]
        [Route("[action]")]
        public List<Following> GetByUserId(long userId)
        {
            return followingService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Following> Get(long followingId)
        {
            return followingService.Get(followingId);
        }

        [HttpPost]
        [Route("[action]"),Authorize(Roles ="Admin")]
        public async Task Delete(long id)
        {
            await followingService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public void DeleteByFollowingUserId(long followingUserId)
        {
            followingService.DeleteByFollowingUserId(followingUserId);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public bool IsFollowing(long userId, long followingUserId)
        {
            return followingService.IsFollowing(userId, followingUserId);
        }
    }
}
