using BanooClub.Models;
using BanooClub.Services.AdsPaymentServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsPaymentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IAdsPaymentService adsPaymentService;

        public AdsPaymentController(BanooClubDBContext context, IAdsPaymentService adsPaymentService)
        {
            this.context = context;
            this.adsPaymentService = adsPaymentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] AdsPayment inputDto)
        {
            await this.adsPaymentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<AdsPayment> Update([FromBody] AdsPayment inputDto)
        {
            var result = await this.adsPaymentService.Update(inputDto);
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public object GetAll(int pageNumber, int count)
        {
            return adsPaymentService.GetAll(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<AdsPayment> Get(long id)
        {
            return adsPaymentService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await adsPaymentService.Delete(id);
        }
    }
}
