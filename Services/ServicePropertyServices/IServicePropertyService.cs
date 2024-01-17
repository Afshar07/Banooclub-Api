using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ServicePropertyServices
{
    public interface IServicePropertyService
    {
        Task<long> Create(ServiceProperty inputDto);
        Task<ServiceProperty> Update(ServiceProperty item);
        object GetAll(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<ServiceProperty> Get(long id);
    }
}
