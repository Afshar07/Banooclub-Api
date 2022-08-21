using BanooClub.Models;
using BanooClub.Services.PostCommentServices;
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
    public class PostCommentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IPostCommentService postCommentService;

        public PostCommentController(BanooClubDBContext context, IPostCommentService postCommentService)
        {
            this.context = context;
            this.postCommentService = postCommentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] PostComment inputDto)
        {
            await this.postCommentService.Create(inputDto);

        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public List<PostComment> GetByUserId(long userId)
        {
            return postCommentService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public List<PostComment> GetByPostId(long postId)
        {
            return postCommentService.GetByPostId(postId);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public List<PostComment> GetChildrenByPostId(long postId, long commentId)
        {
            return postCommentService.GetChildrenByPostId(postId, commentId);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<byte> ChangePostCommentActivation(long postId)
        {
            return await postCommentService.ChangePostCommentActivation(postId);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public Task<PostComment> Get(long postCommentId)
        {
            return postCommentService.Get(postCommentId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await postCommentService.Delete(id);
        }
    }
}
