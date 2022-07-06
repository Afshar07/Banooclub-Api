using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ServicePackServices
{
    public interface IServicePackService
    {
        Task<long> Create(ServicePack inputDto);
        Task<ServicePack> Update(ServicePack item);
        Task<object> GetAll(int pageNumber, int count, string searchCommand);
        Task<object> GetMyServices(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<ServicePack> Get(long id);
        Task<object> GetUserServices(int pageNumber, int count, string searchCommand, long userId);
    }
}
