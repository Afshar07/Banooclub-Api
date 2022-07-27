using BanooClub.Models;
using BanooClub.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ServicePackServices
{
    public interface IServicePackService
    {
        Task<long> Create(ServicePack inputDto);
        Task<ServicePack> Update(ServicePack item);
        Task<object> GetAll(int pageNumber, int count, string searchCommand, ServiceFilter serviceFilter, ServicePackStatus? status);
        Task<object> GetMyServices(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<ServicePack> Get(long id);
        Task<object> GetUserServices(long lastId, int count, string searchCommand, long userId);
        Task<object> GetUserServicesByUserName(long lastId, int count, string searchCommand, string userName);
        Task<object> GetDeactiveServices();
        Task<bool> ChangeServiceStatus(long servicePackId, ServicePackStatus status);
        Task<ServicePack> GetwithView(long servicePackId);
        Task<int> GetMaintainedByServiceId(long serviceId);
        Task<List<ServicePack>> GetAllOrderedService();
        Task<List<ServicePack>> GetOrderedServiceForVendor();
        Task<List<User>> GetServicePayedMember(long serviceId);
    }
}
