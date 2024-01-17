using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.ServicePropertyServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePropertyController : ControllerBase
    {
        private readonly IServicePropertyService servicePropertyService;

        public ServicePropertyController(IServicePropertyService servicePropertyService)
        {
            this.servicePropertyService = servicePropertyService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] ServiceProperty inputDto)
        {
            return await this.servicePropertyService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ServiceProperty> Update([FromBody] ServiceProperty inputDto)
        {
            return await this.servicePropertyService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<ServiceProperty> Get(long servicePropertyId)
        {
            return await servicePropertyService.Get(servicePropertyId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await servicePropertyService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            return servicePropertyService.GetAll(pageNumber, count, searchCommand);
        }
    }
}
