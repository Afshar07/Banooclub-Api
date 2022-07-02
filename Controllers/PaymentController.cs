using BanooClub.Models;
using BanooClub.Services.PaymentServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IPaymentService paymentService;

        public PaymentController(BanooClubDBContext context, IPaymentService paymentService)
        {
            this.context = context;
            this.paymentService = paymentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Payment inputDto)
        {
            await this.paymentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Payment> Update([FromBody] Payment inputDto)
        {
            var result = await this.paymentService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<Payment>> GetAll()
        {
            return paymentService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Payment> Get(long id)
        {
            return paymentService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await paymentService.Delete(id);
        }
    }
}
