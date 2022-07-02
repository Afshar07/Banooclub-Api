using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ChatNKServices
{
    public interface IChatNKService
    {
        Task Create(ChatNK inputDto);
        Task<ChatNK> Update(ChatNK item);
        Task Delete(long id);
        Task<object> GetAll(int pageNumber, int count,string search);
        Task<ChatNK> Get(long id);
    }
}
