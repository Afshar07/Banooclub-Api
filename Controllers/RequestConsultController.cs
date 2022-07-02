using BanooClub.Models;
using BanooClub.Services.RequestConsultServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestConsultController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IRequestConsultService requestConsultService;

        public RequestConsultController(BanooClubDBContext context, IRequestConsultService requestConsultService)
        {
            this.context = context;
            this.requestConsultService = requestConsultService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] RequestConsult inputDto)
        {
            await this.requestConsultService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<RequestConsult> Update([FromBody] RequestConsult inputDto)
        {
            var result = await this.requestConsultService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<RequestConsult>> GetAll()
        {
            return requestConsultService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<RequestConsult> Get(long id)
        {
            return requestConsultService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await requestConsultService.Delete(id);
        }
    }
}
