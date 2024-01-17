using BanooClub.Models;
using BanooClub.Services.ServiceTypeServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IServiceTypeService serviceTypeService;

        public ServiceTypeController(BanooClubDBContext context, IServiceTypeService serviceTypeService)
        {
            this.context = context;
            this.serviceTypeService = serviceTypeService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] ServiceType inputDto)
        {
            await this.serviceTypeService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ServiceType> Update([FromBody] ServiceType inputDto)
        {
            var result = await this.serviceTypeService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Subscriber")]
        public Task<List<ServiceType>> GetAll()
        {
            return serviceTypeService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<ServiceType> Get(long id)
        {
            return serviceTypeService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await serviceTypeService.Delete(id);
        }
    }
}
