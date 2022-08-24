using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.ServicePackServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using BanooClub.Models.Enums;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicePackController : ControllerBase
    {
        private readonly IServicePackService servicePackService;

        public ServicePackController(IServicePackService servicePackService)
        {
            this.servicePackService = servicePackService;
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Vendor")]
        public async Task<long> Create([FromBody] ServicePack inputDto)
        {
            return await this.servicePackService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Vendor")]
        public async Task<ServicePack> Update([FromBody] ServicePack inputDto)
        {
            return await this.servicePackService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<ServicePack> Get(long servicePackId)
        {
            return await servicePackService.Get(servicePackId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Vendor")]
        public async Task<bool> Delete(long id)
        {
            return await servicePackService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Vendor")]
        public async Task<object> GetDeactiveServices()
        {
            return await servicePackService.GetDeactiveServices();
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Vendor")]
        public async Task<bool> ChangeServiceStatus(long servicePackId, ServicePackStatus status)
        {
            return await servicePackService.ChangeServiceStatus(servicePackId, status);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetAll(ServicePackStatus? status, int pageNumber, int count, 
            string searchCommand, ServiceFilter serviceFilter = ServiceFilter.All)
        {
            return await servicePackService.GetAll(pageNumber, count, searchCommand, serviceFilter, status);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetMyServices(int pageNumber, int count, string searchCommand)
        {
            return await servicePackService.GetMyServices(pageNumber, count, searchCommand);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetUserServices(long lastId, int count, string searchCommand, long userId)
        {
            return await servicePackService.GetUserServices(lastId, count, searchCommand, userId);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetUserServicesByUserName(long lastId, int count, string searchCommand, string userName)
        {
            return await servicePackService.GetUserServicesByUserName(lastId, count, searchCommand, userName);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Vendor")]
        public async Task<object> GetwithView(long servicePackId)
        {
            return await servicePackService.GetwithView(servicePackId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<int> GetMaintainedByServiceId(long serviceId)
        {
            return await servicePackService.GetMaintainedByServiceId(serviceId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<List<ServicePack>> GetAllOrderedService()
        {
            return await servicePackService.GetAllOrderedService();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<List<ServicePack>> GetOrderedServiceForVendor()
        {
            return await servicePackService.GetOrderedServiceForVendor();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<List<User>> GetServicePayedMember(long serviceId)
        {
            return await servicePackService.GetServicePayedMember(serviceId);
        }
    }
}
