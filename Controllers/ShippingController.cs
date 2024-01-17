using BanooClub.Models;
using BanooClub.Services.ShippingServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService shippingService;

        public ShippingController(IShippingService shippingService)
        {
            this.shippingService = shippingService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] Shipping inputDto)
        {
            return await this.shippingService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Shipping> Update([FromBody] Shipping inputDto)
        {
            return await this.shippingService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"),AllowAnonymous]
        public async Task<Shipping> Get(long shippingId)
        {
            return await shippingService.Get(shippingId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await shippingService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"),AllowAnonymous]
        public object GetAll(int pageNumber, int count)
        {
            return shippingService.GetAll(pageNumber, count);
        }
    }
}
