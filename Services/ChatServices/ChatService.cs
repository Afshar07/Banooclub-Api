using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ChatServices
{
    public class ChatService : IChatService
    {
        private readonly IBanooClubEFRepository<Chat> ChatRepository;

        public ChatService(IBanooClubEFRepository<Chat> ChatRepository)
        {
            this.ChatRepository = ChatRepository;
        }
        public async Task Create(Chat inputDto)
        {
            ChatRepository.Insert(inputDto);
        }

        public async Task<Chat> Update(Chat item)
        {
            await ChatRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<Chat> dbChat = new List<Chat>();
            dbChat = ChatRepository.GetQuery().OrderByDescending(x => x.Date).Skip((pageNumber-1)*count).Take(count).ToList();
            var ChatCount = ChatRepository.GetQuery().Count();
            var obj = new
            {
                Chats = dbChat,
                ChatCount = ChatCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var dbChat = ChatRepository.GetQuery().FirstOrDefault(z => z.ChatId == id);
            await ChatRepository.Delete(dbChat);
        }

        public async Task<Chat> Get(long id)
        {
            var dbChat = ChatRepository.GetQuery().FirstOrDefault(z => z.ChatId == id);
            return dbChat;
        }
    }
}
