using BanooClub.Models;
using BanooClub.Services.FriendshipPostServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipPostController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFriendshipPostService friendshipPostService;

        public FriendshipPostController(BanooClubDBContext context, IFriendshipPostService friendshipPostService)
        {
            this.context = context;
            this.friendshipPostService = friendshipPostService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] FriendshipPost inputDto)
        {
            await this.friendshipPostService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<FriendshipPost> Update([FromBody] FriendshipPost inputDto)
        {
            var result = await this.friendshipPostService.Update(inputDto);
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public Task<List<FriendshipPost>> GetAll()
        {
            return friendshipPostService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<FriendshipPost> Get(long id)
        {
            return friendshipPostService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await friendshipPostService.Delete(id);
        }
    }
}
