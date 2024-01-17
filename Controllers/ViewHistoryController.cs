using BanooClub.Models;
using BanooClub.Services.ViewHistoryServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ViewHistoryController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IViewHistoryService viewHistoryService;

        public ViewHistoryController(BanooClubDBContext context, IViewHistoryService viewHistoryService)
        {
            this.context = context;
            this.viewHistoryService = viewHistoryService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] ViewHistory inputDto)
        {
            await this.viewHistoryService.Create(inputDto);
        }

        [HttpPost]                                                                             
        [Route("[action]")]
        public List<ViewHistory> GetByUserId(long userId)
        {
            return viewHistoryService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<ViewHistory> Get(long logId)
        {
            return viewHistoryService.Get(logId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await viewHistoryService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<List<ViewHistory>> GetAll()
        {
            return await viewHistoryService.GetAll();
        }
    }
}
