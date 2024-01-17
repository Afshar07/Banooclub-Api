using BanooClub.Models;
using BanooClub.Services.MessageServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase 
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Message inputDto)
        {
            await messageService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> SendMessage([FromBody] Message inputDto)
        {
            return await messageService.SendMessage(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> ReadMessage(long userId)
        {
            return await messageService.ReadMessage(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task DeliverMessage()
        {
            await messageService.DeliverMessage();
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<List<MessageDTO>> GetMenu()
        {
            return await messageService.GetByUserId();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Message> Get(long logId)
        {
            return messageService.Get(logId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await messageService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<List<Message>> GetAll()
        {
            return await messageService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetConversation(long? groupId, long? userId, long messageId, int count)
        {
            messageId = messageId == null ? 0 : messageId;
            if (groupId != null && groupId != 0)
            {
                return await messageService.GetGroupConversation((long)groupId, messageId, count);
            }
            if (userId != null && userId != 0)
            {
                return await messageService.GetUserConversation((long)userId, messageId, count);
            }
            return null;
        }



    }
}
