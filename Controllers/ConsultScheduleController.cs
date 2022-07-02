using BanooClub.Models;
using BanooClub.Services.ConsultScheduleServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultScheduleController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IConsultScheduleService consultScheduleService;

        public ConsultScheduleController(BanooClubDBContext context, IConsultScheduleService consultScheduleService)
        {
            this.context = context;
            this.consultScheduleService = consultScheduleService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] ConsultSchedule inputDto)
        {
            await this.consultScheduleService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ConsultSchedule> Update([FromBody] ConsultSchedule inputDto)
        {
            var result = await this.consultScheduleService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public object GetAll(int pageNumber, int count)
        {
            return consultScheduleService.GetAll(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<ConsultSchedule> Get(long id)
        {
            return consultScheduleService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await consultScheduleService.Delete(id);
        }
    }
}
