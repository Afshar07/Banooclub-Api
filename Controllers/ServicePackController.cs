using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.ServicePackServices;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [Route("[action]"),Authorize(Roles ="Admin,Vendor")]
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
        [Route("[action]") , AllowAnonymous]
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
        [Route("[action]") , AllowAnonymous]
        public async Task<object> GetAll(int pageNumber, int count , string searchCommand)
        {
            return await servicePackService.GetAll(pageNumber, count , searchCommand);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetMyServices(int pageNumber, int count, string searchCommand)
        {
            return await servicePackService.GetMyServices(pageNumber, count, searchCommand);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetUserServices(int pageNumber, int count, string searchCommand,long userId)
        {
            return await servicePackService.GetUserServices(pageNumber, count, searchCommand,userId);
        }

    }
}
