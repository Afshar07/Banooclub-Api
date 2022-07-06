using BanooClub.Models;
using BanooClub.Services.PostServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PostController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IPostService postService;
        public PostController(BanooClubDBContext context, IPostService postService)
        {
            this.context = context;
            this.postService = postService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Post inputDto)
        {
            await this.postService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Update([FromBody] Post inputDto)
        {
            var result = await this.postService.UpdateByCmd(inputDto.Title,inputDto.Description, inputDto.PostId,inputDto.Status);
            return result;

           
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetMyPost(int pageNumber, int count)
        {
            return postService.GetMyPost(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public IActionResult GetByUserId(long userId)
        {
            var result = postService.GetByUserId(userId);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public Task<Post> Get(long postId)
        {
            return postService.Get(postId);
        }

        //[HttpPost]
        //[Route("[action]")]
        //public async Task Delete(long id)
        //{
        //    await postService.Delete(id);
        //}

        //[HttpPost]
        //[Route("[action]")]
        //public async Task<List<PostDTO>> GetFollowingPosts()
        //{
        //    return await postService.GetFollowingPosts();
        //}

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetAll(int pageNumber, int count)
        {
            return postService.GetAll(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetNotConfirmed(int pageNumber, int count,string search)
        {
            search = search ==null ? "" : search;
            return postService.GetNotConfirmed(pageNumber,count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetReported(int pageNumber, int count)
        {
            return postService.GetReported(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task ChangePostStatus([FromBody] Post inputDto)
        {
            await this.postService.ChangePostStatus(inputDto);
        }
    }
}
