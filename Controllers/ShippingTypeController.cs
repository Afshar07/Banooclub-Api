using BanooClub.Models;
using BanooClub.Services.ShippingTypeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ShippingTypeController : ControllerBase
    {
        private readonly IShippingTypeService shippingTypeService;

        public ShippingTypeController(IShippingTypeService shippingTypeService)
        {
            this.shippingTypeService = shippingTypeService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] ShippingType inputDto)
        {
            return await this.shippingTypeService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ShippingType> Update([FromBody] ShippingType inputDto)
        {
            return await this.shippingTypeService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"),AllowAnonymous]
        public async Task<ShippingType> Get(long shippingId)
        {
            return await shippingTypeService.Get(shippingId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await shippingTypeService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"),AllowAnonymous]
        public object GetAll(int pageNumber, int count)
        {
            return shippingTypeService.GetAll(pageNumber, count);
        }
    }
}
