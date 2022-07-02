using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsChatServices
{
    public interface IAdsChatService
    {
        Task Create(AdsChat inputDto);
        Task<AdsChat> Update(AdsChat item);
        Task Delete(long id);
        object GetAll(int pageNumber,int count);
        Task<AdsChat> Get(long id);
    }
}
