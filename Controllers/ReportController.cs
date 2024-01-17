using BanooClub.Models;
using BanooClub.Services.ReportServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IReportService reportService;

        public ReportController(BanooClubDBContext context, IReportService reportService)
        {
            this.context = context;
            this.reportService = reportService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Report inputDto)
        {
            await this.reportService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Report> Update([FromBody] Report inputDto)
        {
            var result = await this.reportService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<Report>> GetAll()
        {
            return reportService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Report> Get(long id)
        {
            return reportService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await reportService.Delete(id);
        }
    }
}
