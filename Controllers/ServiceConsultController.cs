using BanooClub.Models;
using BanooClub.Services.ServiceConsultServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceConsultController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IServiceConsultService serviceConsultService;

        public ServiceConsultController(BanooClubDBContext context, IServiceConsultService serviceConsultService)
        {
            this.context = context;
            this.serviceConsultService = serviceConsultService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] ServiceConsult inputDto)
        {
            await this.serviceConsultService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ServiceConsult> Update([FromBody] ServiceConsult inputDto)
        {
            var result = await this.serviceConsultService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<ServiceConsult>> GetAll()
        {
            return serviceConsultService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<ServiceConsult> Get(long id)
        {
            return serviceConsultService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await serviceConsultService.Delete(id);
        }
    }
}
