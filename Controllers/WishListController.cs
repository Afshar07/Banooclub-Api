using BanooClub.Models;
using BanooClub.Services.WishListServices;
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
    public class WishListController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IWishListService wishListService;

        public WishListController(BanooClubDBContext context, IWishListService wishListService)
        {
            this.context = context;
            this.wishListService = wishListService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] WishList inputDto)
        {
            await this.wishListService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public List<WishList> GetByUserId(long userId)
        {
            return wishListService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<WishList> Get(long wishId)
        {
            return wishListService.Get(wishId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await wishListService.Delete(id);
        }
    }
}
