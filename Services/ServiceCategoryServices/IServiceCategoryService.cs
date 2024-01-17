using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ServiceCategoryServices
{
    public interface IServiceCategoryService
    {
        Task<long> Create(ServiceCategory inputDto);
        Task<ServiceCategory> Update(ServiceCategory item);
        object GetAll(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<ServiceCategory> Get(long id);
    }
}
