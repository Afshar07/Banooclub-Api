using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.PaymentServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> Create([FromBody] Payment inputDto)
        {
            return await paymentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Payment> Get(long paymentId)
        {
            return paymentService.Get(paymentId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Accountent")]
        public async Task<object> GetAll(int pageNumber, int count, string searchCommand)
        {
            return await paymentService.GetAll(pageNumber, count, searchCommand);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Accountent")]
        public async Task<object> GetByUserId(long userId, byte count, short pageNumber)
        {
            return await paymentService.GetByUserId(userId, count, pageNumber);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<object> GetMyPayments(int pageNumber, int count)
        {
            return await paymentService.GetMyPayments(pageNumber, count);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,Accountent")]
        public async Task Delete(long id)
        {
            await paymentService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<int> ChangePaymentStatus(string orderId, string transId)
        {
            return await paymentService.ChangePaymentStatus(orderId, transId);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<byte> ChangePaymentStatusForAdmin(long paymentId, PaymentStatus status)
        {
            return await paymentService.ChangePaymentStatus(paymentId, status);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<int> PayByWallet(long orderId)
        {
            return await paymentService.PayByWallet(orderId);
        }
    }
}
