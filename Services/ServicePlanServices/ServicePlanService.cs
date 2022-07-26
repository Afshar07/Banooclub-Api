using BanooClub.Models;
using BanooClub.Models.Enums;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ServicePlanServices
{
    public class ServicePlanService : IServicePlanService
    {
        private readonly IBanooClubEFRepository<ServicePlan> servicePlanRepository;
        public ServicePlanService(IBanooClubEFRepository<ServicePlan> servicePlanRepository)
        {
            this.servicePlanRepository = servicePlanRepository;
        }
        public async Task<long> Create(ServicePlan inputDto)
        {
            var dbCreation = servicePlanRepository.Insert(inputDto);
            return dbCreation.ServicePlanId;
        }

        public async Task<ServicePlan> Update(ServicePlan item)
        {
            await servicePlanRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<ServicePlan> servicePlans = new List<ServicePlan>();
            servicePlans = servicePlanRepository.GetQuery().OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var servicePlansCount = servicePlanRepository.GetQuery().Count();
            var obj = new
            {
                ServicePlans = servicePlans,
                ServicePlansCount = servicePlansCount,
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var servicePlan = servicePlanRepository.GetQuery().FirstOrDefault(z => z.ServicePlanId == id);
            await servicePlanRepository.Delete(servicePlan);
        }

        public async Task<ServicePlan> Get(long id)
        {
            var servicePlan = servicePlanRepository.GetQuery().FirstOrDefault(z => z.ServicePlanId == id);
            return servicePlan;
        }
        public List<ServicePlan> GetByObjectId(long objectId,ServicePlanType type)
        {
            return servicePlanRepository.GetQuery().Where(z => z.ObjectId == objectId && z.Type == type).ToList();
        }
    }
}
