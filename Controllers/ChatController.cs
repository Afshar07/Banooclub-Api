using BanooClub.Models;
using BanooClub.Services.ChatServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IChatService chatService;

        public ChatController(BanooClubDBContext context, IChatService chatService)
        {
            this.context = context;
            this.chatService = chatService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Chat inputDto)
        {
            await this.chatService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Chat> Update([FromBody] Chat inputDto)
        {
            var result = await this.chatService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public object GetAll(int pageNumber, int count)
        {
            return chatService.GetAll(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Chat> Get(long id)
        {
            return chatService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await chatService.Delete(id);
        }
    }
}
