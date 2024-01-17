using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsCategoryServices
{
    public interface IAdsCategoryService
    {
        Task Create(AdsCategory inputDto);
        Task<AdsCategory> Update(AdsCategory item);
        Task<bool> Delete(long id);
        object GetAll(int pageNumber, int count, string search);
        Task<AdsCategory> Get(long id);
    }
}
