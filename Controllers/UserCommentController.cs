using BanooClub.Models;
using BanooClub.Services.UserCommentServices;
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
    public class UserCommentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IUserCommentService userCommentService;

        public UserCommentController(BanooClubDBContext context, IUserCommentService userCommentService)
        {
            this.context = context;
            this.userCommentService = userCommentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] UserComment inputDto)
        {
            await this.userCommentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<UserComment> Update([FromBody] UserComment inputDto)
        {
            var result = await this.userCommentService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public Task<List<UserComment>> GetAll()
        {
            return userCommentService.GetAll();
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public Task<UserComment> Get(long id)
        {
            return userCommentService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await userCommentService.Delete(id);
        }
    }
}
