using BanooClub.Models;
using BanooClub.Services.SystemPaymentServices;
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
    public class SystemPaymentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly ISystemPaymentService systemPaymentService;

        public SystemPaymentController(BanooClubDBContext context, ISystemPaymentService systemPaymentService)
        {
            this.context = context;
            this.systemPaymentService = systemPaymentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] SystemPayment inputDto)
        {
            await this.systemPaymentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<SystemPayment> Update([FromBody] SystemPayment inputDto)
        {
            var result = await this.systemPaymentService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<SystemPayment>> GetAll()
        {
            return systemPaymentService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<SystemPayment> Get(long id)
        {
            return systemPaymentService.Get(id);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await systemPaymentService.Delete(id);
        }
    }
}
