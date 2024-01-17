using BanooClub.Models;
using BanooClub.Services.FriendshipPostCommentServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipPostCommentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFriendshipPostCommentService friendshipPostCommentService;

        public FriendshipPostCommentController(BanooClubDBContext context, IFriendshipPostCommentService friendshipPostCommentService)
        {
            this.context = context;
            this.friendshipPostCommentService = friendshipPostCommentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] FriendshipPostComment inputDto)
        {
            await this.friendshipPostCommentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<FriendshipPostComment> Update([FromBody] FriendshipPostComment inputDto)
        {
            var result = await this.friendshipPostCommentService.Update(inputDto);
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public Task<List<FriendshipPostComment>> GetAll()
        {
            return friendshipPostCommentService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<FriendshipPostComment> Get(long id)
        {
            return friendshipPostCommentService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await friendshipPostCommentService.Delete(id);
        }
    }
}
