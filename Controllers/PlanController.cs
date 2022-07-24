using BanooClub.Models;
using BanooClub.Services.PlanServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService PlanService;

        public PlanController(IPlanService PlanService)
        {
            this.PlanService = PlanService;
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task Create([FromBody] Plan inputDto)
        {
            await PlanService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task Update([FromBody] Plan inputDto)
        {
            await PlanService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public Task<Plan> Get(long PlanId)
        {
            return PlanService.Get(PlanId);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await PlanService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public object GetAll(int pageNumber, int count)
        {
            return PlanService.GetAll(pageNumber, count);
        }
    }
}
