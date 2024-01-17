using BanooClub.Models;
using BanooClub.Services.PostLikeServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikeController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IPostLikeService postLikeService;

        public PostLikeController(BanooClubDBContext context, IPostLikeService postLikeService)
        {
            this.context = context;
            this.postLikeService = postLikeService;
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public bool Create(long postId)
        {
            return this.postLikeService.Create(postId);
        }

        [HttpPost]
        [Route("[action]")]
        public List<PostLike> GetByUserId(long userId)
        {
            return postLikeService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<PostLike> Get(long postLikeId)
        {
            return postLikeService.Get(postLikeId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await postLikeService.Delete(id);
        }
        [HttpPost]
        [Route("[action]")]
        public List<User> GetLikesByPostId(long postId)
        {
            return postLikeService.GetLikesByPostId(postId);
        }
    }
}
