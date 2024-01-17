using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Services.BlogCommentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentController : ControllerBase
    {
        private readonly IBlogCommentService _crudBlogCommentService;

        public BlogCommentController(IBlogCommentService crudBlogCommentService)
        {
            _crudBlogCommentService = crudBlogCommentService;
        }

        [HttpPost("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int pageNumber, int count, string searchCommand, bool? IsActive)
        {
            var result = await _crudBlogCommentService.GetAll(pageNumber, count, searchCommand, IsActive);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("CreateBlogComment")]
        public async Task<IActionResult> CreateBlogComment([FromBody] BlogCommentDTO model)
        {
            var result = await _crudBlogCommentService.CreateBlogComment(User.Identity.GetUserId(), model.Message, model.BlogId, model.ParentId);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("UpdateBlogComment")]
        public async Task<IActionResult> UpdateBlogComment(BlogComment model)
        {
            var result = await _crudBlogCommentService.UpdateBlogComment(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("DeleteBlogComment")]
        public async Task<IActionResult> DeleteBlogComment(int BlogCommentId)
        {
            var result = await _crudBlogCommentService.DeleteBlogComment(BlogCommentId);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }
        [HttpPost("GetAllByBlogId")]
        [AllowAnonymous]
        public IActionResult GetAllByBlogId(int blogId)
        {
            var result = _crudBlogCommentService.GetAllAsyncByBlogId(blogId);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("ChangeCommentActivation"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeCommentActivation(int blogCommentId)
        {
            var result = await _crudBlogCommentService.ChangeCommentActivation(blogCommentId);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }
    }
}
