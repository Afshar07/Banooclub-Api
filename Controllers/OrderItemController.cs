using BanooClub.Models;
using BanooClub.Services.OrderItemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            this.orderItemService = orderItemService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] OrderItem inputDto)
        {
            await orderItemService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<OrderItem> Get(long orderItemId)
        {
            return orderItemService.Get(orderItemId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await orderItemService.Delete(id);
        }
    }
}
