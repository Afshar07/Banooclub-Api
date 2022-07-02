using BanooClub.Models;
using BanooClub.Services.FriendshipCommentServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipCommentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFriendshipCommentService friendshipCommentService;

        public FriendshipCommentController(BanooClubDBContext context, IFriendshipCommentService friendshipCommentService)
        {
            this.context = context;
            this.friendshipCommentService = friendshipCommentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] FriendshipComment inputDto)
        {
            await this.friendshipCommentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<FriendshipComment> Update([FromBody] FriendshipComment inputDto)
        {
            var result = await this.friendshipCommentService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<FriendshipComment>> GetAll()
        {
            return friendshipCommentService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<FriendshipComment> Get(long id)
        {
            return friendshipCommentService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await friendshipCommentService.Delete(id);
        }
    }
}
