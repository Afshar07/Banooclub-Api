using BanooClub.Models;
using BanooClub.Services.BlogCategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryService _crudBlogCategoryService;

        public BlogCategoryController(IBlogCategoryService crudBlogCategoryService)
        {
            _crudBlogCategoryService = crudBlogCategoryService;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _crudBlogCategoryService.GetAll();

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("CreateBlogCategory"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBlogCategory(BlogCategory model)
        {
            var result = await _crudBlogCategoryService.CreateBlogCategory(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("UpdateBlogCategory"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBlogCategory(BlogCategory model)
        {
            var result = await _crudBlogCategoryService.UpdateBlogCategory(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("UpdateBlogCategorySEO"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBlogCategorySEO(BlogCategory model)
        {
            var result = await _crudBlogCategoryService.UpdateBlogCategorySEO(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("DeleteBlogCategory"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBlogCategory(long BlogCategoryId)
        {
            var result = await _crudBlogCategoryService.DeleteBlogCategory(BlogCategoryId);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }
    }
}
