using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.LogServices
{
    public interface ILogService
    {
        Task Create(Log inputDto);
        Task<Log> Update(Log item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count);
        Task<Log> Get(long id);
        List<Log> GetByUserId(long userId);
    }
}
