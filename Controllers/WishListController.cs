using BanooClub.Models;
using BanooClub.Models.Enums;
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
        public async Task Toggle(long objectId,WishListType type)
        {
            await this.wishListService.Toggle(objectId,type);
        }

        [HttpPost]
        [Route("[action]")]
        public object GetByUserId(long userId)
        {
            return wishListService.GetByUserId(userId);
        }
        [HttpPost]
        [Route("[action]")]
        public List<WishList> GetByWishList()
        {
            return wishListService.GetByWishList();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<WishList> Get(long wishId)
        {
            return wishListService.Get(wishId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long objectId,WishListType type)
        {
            await wishListService.Delete(objectId,type);
        }
    }
}
