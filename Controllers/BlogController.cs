using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.BlogServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _crudBlogService;

        public BlogController(IBlogService crudBlogService)
        {
            _crudBlogService = crudBlogService;
        }

        [HttpPost("GetAll")]
        [AllowAnonymous]
        public IActionResult GetAll(string categoryName, long? tagId, string searchByTitle, int pageNumber, int count, DateTime? date)
        {
            var result = _crudBlogService.GetAll(categoryName, tagId, searchByTitle, pageNumber, count, date);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("CreateBlog"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBlog(Blog model)
        {
            var result = await _crudBlogService.CreateBlog(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("UpdateBlog"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBlog(Blog model)
        {
            var result = await _crudBlogService.UpdateBlog(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("UpdateBlogSEO"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBlogSEO(Blog model)
        {
            var result = await _crudBlogService.UpdateBlogSEO(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("DeleteBlog"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBlog(long BlogId)
        {
            var result = await _crudBlogService.DeleteBlog(BlogId);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }
        [HttpPost("GetByName")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByName(string blogName)
        {
            var result = await _crudBlogService.GetByName(blogName);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }


        [HttpPost("GetById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _crudBlogService.GetById(id);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }
        [HttpPost("GetAllWithCategory"), AllowAnonymous]
        public async Task<IActionResult> GetAllWithCategory()
        {
            var result = await _crudBlogService.GetAllWithCategory();

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("GetMostPopular"), AllowAnonymous]
        public async Task<IActionResult> GetMostPopular(int pageNumber, int count)
        {
            var result = await _crudBlogService.GetMostPopular(pageNumber, count);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("ChangeBlogStatus"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeBlogStatus(BlogStatus status, params long[] ids)
        {
            var result = await _crudBlogService.ChangeBlogStatus(status, ids);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }
    }
}
