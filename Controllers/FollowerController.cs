using BanooClub.Models;
using BanooClub.Services.FollowerServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFollowerService followerService;

        public FollowerController(BanooClubDBContext context, IFollowerService followerService)
        {
            this.context = context;
            this.followerService = followerService;
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public async Task Create([FromBody] Follower inputDto)
        {
            await this.followerService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public List<Follower> GetMyFollowers()
        {
            return followerService.GetMyFollowers();
        }

        [HttpPost]
        [Route("[action]")]
        public List<Follower> GetByUserId(long userId)
        {
            return followerService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Follower> Get(long followerId)
        {
            return followerService.Get(followerId);
        }

        [HttpPost]
        [Route("[action]"),Authorize(Roles ="Admin")]
        public async Task Delete(long id)
        {
            await followerService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public void DeleteByFollowerUserId(long followerUserId)
        {
            followerService.DeleteByFollowerUserId(followerUserId);
        }
    }
}
