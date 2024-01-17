using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.ServicePlanServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePlanController : ControllerBase
    {
        private readonly IServicePlanService servicePlanService;

        public ServicePlanController(IServicePlanService servicePlanService)
        {
            this.servicePlanService = servicePlanService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] ServicePlan inputDto)
        {
            return await this.servicePlanService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ServicePlan> Update([FromBody] ServicePlan inputDto)
        {
            return await this.servicePlanService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public List<ServicePlan> GetByObjectId(long objectId, ServicePlanType type)
        {
            return servicePlanService.GetByObjectId(objectId,type);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<ServicePlan> Get(long servicePlanId)
        {
            return servicePlanService.Get(servicePlanId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await servicePlanService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetAll(int pageNumber, int count)
        {
            return servicePlanService.GetAll(pageNumber, count);
        }
    }
}
