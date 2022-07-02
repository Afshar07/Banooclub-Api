using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.TagServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] Tag inputDto)
        {
            return await this.tagService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Tag> Update([FromBody] Tag inputDto)
        {
            return await this.tagService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<Tag> Get(long tagId)
        {
            return await tagService.Get(tagId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await tagService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count,string searchCommand)
        {
            return tagService.GetAll(pageNumber, count,searchCommand);
        }
    }
}
