using BanooClub.Models;
using BanooClub.Models.Enums;
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
        private readonly IPostService postService;
        public PostController( IPostService postService)
        {
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
            var result = await this.postService.UpdateByCmd(inputDto.Title, inputDto.Description, inputDto.PostId, inputDto.Status);
            return result;
        }

        //[HttpPost]
        //[Route("[action]"), AllowAnonymous]
        //public object GetMyPost(int pageNumber, int count)
        //{
        //    return postService.GetMyPost(pageNumber,count);
        //}

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetMyPost(long lastId, int count)
        {
            return postService.GetMyPost(lastId, count);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public IActionResult GetByUserId(long userId, long lastId, int count)
        {
            var result = postService.GetByUserId(userId, lastId, count);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public IActionResult GetByUserName(string userName, long lastId, int count)
        {
            var result = postService.GetByUserName(userName, lastId, count);
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

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<bool> Delete(params long[] ids)
        {
            return await postService.Delete(ids);
        }



        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetFollowingPosts(long lastId, int count)
        {

            return await postService.GetFollowingPosts(lastId, count);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetAll(int pageNumber, int count)
        {
            return postService.GetAll(pageNumber, count);
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAllByInfiniteScroll(long lastId, int count, string search = "")
        {
            search = search == null ? "" : search;
            return postService.GetAllByInfiniteScroll(lastId, count, search);
        }

        //[HttpPost]
        //[Route("[action]"), Authorize]
        //public object GetAllUserforUser(long userId, int count, string search)
        //{
        //    search = search == null ? "" : search;
        //    return userService.GetAllUserforUser(userId, count, search);
        //}



        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetNotConfirmed(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return postService.GetNotConfirmed(pageNumber, count, search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetReported(int pageNumber, int count)
        {
            return postService.GetReported(pageNumber, count);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<bool> ChangePostStatusForAdmin(PostStatus status, params long[] ids)
        {
            return await this.postService.ChangePostStatusForAdmin(status, ids);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<Post> ChangePostStatus([FromBody] Post inputDto)
        {
            return await this.postService.ChangePostStatus(inputDto);
        }
    }
}
