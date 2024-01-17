using BanooClub.Models;
using BanooClub.Services.LogServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly ILogService logService;

        public LogController(BanooClubDBContext context, ILogService logService)
        {
            this.context = context;
            this.logService = logService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Log inputDto)
        {
            await this.logService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public List<Log> GetByUserId(long userId)
        {
            return logService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Log> Get(long logId)
        {
            return logService.Get(logId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await logService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetAll(int pageNumber, int count)
        {
            return  logService.GetAll(pageNumber,count);
        }
    }
}
