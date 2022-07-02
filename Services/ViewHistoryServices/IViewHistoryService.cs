using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ViewHistoryServices
{
    public interface IViewHistoryService
    {
        Task Create(ViewHistory inputDto);
        Task<ViewHistory> Update(ViewHistory item);
        Task Delete(long id);
        Task<List<ViewHistory>> GetAll();
        Task<ViewHistory> Get(long id);
        List<ViewHistory> GetByUserId(long userId);
    }
}
