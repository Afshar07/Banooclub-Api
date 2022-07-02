using BanooClub.Models;
using BanooClub.Services.ChatNKServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatNKController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IChatNKService chatNKService;

        public ChatNKController(BanooClubDBContext context, IChatNKService chatNKService)
        {
            this.context = context;
            this.chatNKService = chatNKService;
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Create([FromBody] ChatNK inputDto)
        {
            await chatNKService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<ChatNK> Get(long id)
        {
            return chatNKService.Get(id);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await chatNKService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetAll(int pageNumber, int count,string search)
        {
            search = search ==null ? "" : search;
            return await chatNKService.GetAll(pageNumber,count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Update([FromBody] ChatNK inputDto)
        {
            await chatNKService.Update(inputDto);
        }
    }
}
