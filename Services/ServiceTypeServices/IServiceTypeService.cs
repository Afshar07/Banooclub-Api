using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ServiceTypeServices
{
    public interface IServiceTypeService
    {
        Task Create(ServiceType inputDto);
        Task<ServiceType> Update(ServiceType item);
        Task Delete(long id);
        Task<List<ServiceType>> GetAll();
        Task<ServiceType> Get(long id);
    }
}
