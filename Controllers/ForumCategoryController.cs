using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.ForumCategoryServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumCategoryController : ControllerBase
    {
        private readonly IForumCategoryService forumCategoryService;

        public ForumCategoryController(IForumCategoryService forumCategoryService)
        {
            this.forumCategoryService = forumCategoryService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] ForumCategory inputDto)
        {
            return await this.forumCategoryService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ForumCategory> Update([FromBody] ForumCategory inputDto)
        {
            return await this.forumCategoryService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<ForumCategory> Get(long forumCategoryId)
        {
            return await forumCategoryService.Get(forumCategoryId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await forumCategoryService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            return forumCategoryService.GetAll(pageNumber, count, searchCommand);
        }
    }
}
