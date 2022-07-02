using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.ForumCommentServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumCommentController : ControllerBase
    {
        private readonly IForumCommentService forumCommentService;

        public ForumCommentController(IForumCommentService forumCommentService)
        {
            this.forumCommentService = forumCommentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] ForumComment inputDto)
        {
            return await this.forumCommentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ForumComment> Update([FromBody] ForumComment inputDto)
        {
            return await this.forumCommentService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<ForumComment> Get(long forumCommentId)
        {
            return await forumCommentService.Get(forumCommentId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await forumCommentService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            return forumCommentService.GetAll(pageNumber, count, searchCommand);
        }
    }
}
