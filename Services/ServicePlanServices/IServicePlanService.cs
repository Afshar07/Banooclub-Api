using BanooClub.Models;
using BanooClub.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ServicePlanServices
{
    public interface IServicePlanService
    {
        Task<long> Create(ServicePlan inputDto);
        Task<ServicePlan> Update(ServicePlan item);
        object GetAll(int pageNumber, int count);
        Task Delete(long id);
        Task<ServicePlan> Get(long id);
        List<ServicePlan> GetByObjectId(long objectId, ServicePlanType type);
    }
}
