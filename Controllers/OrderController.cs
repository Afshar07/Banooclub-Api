using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] Order inputDto)
        {
            return await orderService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public object GetByUserId(short pageNumber, byte count)
        {
            return orderService.GetByUserId(pageNumber, count);
        }

        [HttpPost]
        [Route("[action]")]
        public object GetByUserIdForAdmin(short pageNumber, byte count, long userId)
        {
            return orderService.GetByUserIdForAdmin(pageNumber, count, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Order> Get(long orderId)
        {
            return orderService.Get(orderId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await orderService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<object> GetAll(int pageNumber, int count, string searchCommand)
        {
            return await orderService.GetAll(pageNumber, count, searchCommand);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> ChangeOrderStatus(long orderId, OrderStatus status)
        {
            return await orderService.ChangeOrderStatus(orderId, status);
        }
    }
}
