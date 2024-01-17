using BanooClub.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.MessageServices
{
    public interface IMessageService
    {
        Task Create(Message inputDto);
        Task<Message> Update(Message item);
        Task Delete(long id);
        Task<List<Message>> GetAll();
        Task<Message> Get(long id);
        Task<List<MessageDTO>> GetByUserId();
        Task<bool> ReadMessage(long userId);
        Task<long> SendMessage(Message item);
        Task DeliverMessage();
        Task<object> GetUserConversation(long userId, long messageId,int count);
        Task<object> GetGroupConversation(long groupId, long messageId,int count);
    }
}
