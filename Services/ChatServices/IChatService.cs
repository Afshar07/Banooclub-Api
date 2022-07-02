using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ChatServices
{
    public interface IChatService
    {
        Task Create(Chat inputDto);
        Task<Chat> Update(Chat item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count);
        Task<Chat> Get(long id);
    }
}
