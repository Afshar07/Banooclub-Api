using BanooClub.Models;
using BanooClub.Services.FriendshipServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFriendshipService friendshipService;

        public FriendshipController(BanooClubDBContext context, IFriendshipService friendshipService)
        {
            this.context = context;
            this.friendshipService = friendshipService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Friendship inputDto)
        {
            await this.friendshipService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Friendship> Update([FromBody] Friendship inputDto)
        {
            var result = await this.friendshipService.Update(inputDto);
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public Task<List<Friendship>> GetAll()
        {
            return friendshipService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Friendship> Get(long id)
        {
            return friendshipService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await friendshipService.Delete(id);
        }
    }
}
