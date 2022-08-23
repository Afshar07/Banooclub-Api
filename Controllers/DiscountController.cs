using BanooClub.Models;
using BanooClub.Services.DiscountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        #region Constructor

        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        #endregion

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,ProductManager")]
        public async Task<object> Create([FromBody] DiscountDto inputDto)
        {
            return await _discountService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,ProductManager")]
        public async Task Update([FromBody] Discount inputDto)
        {
            await _discountService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetAll(int pageNumber, int count)
        {
            return await _discountService.GetAll(pageNumber, count);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Discount> Get(long discountId)
        {
            return _discountService.Get(discountId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin,ProductManager")]
        public async Task Delete(long id)
        {
            await _discountService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetAllRefraction(int pageNumber, int count)
        {
            return await _discountService.GetAllRefraction(pageNumber, count);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetLatestDiscount(string searchCommand, byte count, short pageNumber)
        {
            return await _discountService.GetLatestDiscount(searchCommand, count, pageNumber);
        }

    }
}
