using BanooClub.Models;
using BanooClub.Services.AdsServices;
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
    public class AdsController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IAdsService adsService;

        public AdsController(BanooClubDBContext context, IAdsService adsService)
        {
            this.context = context;
            this.adsService = adsService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Ads inputDto)
        {
            await this.adsService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Ads> Update([FromBody] Ads inputDto)
        {
            var result = await adsService.Update(inputDto);
            return result;
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetAll(int pageNumber, int count, string search)
        {
            search = search == null ? "" : search;
            return await adsService.GetAll(pageNumber, count, search);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<Ads> Get(long id)
        {
            return await adsService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await adsService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetMyAds(int pageNumber, int count)
        {
            return await adsService.GetMyAds(pageNumber, count);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetByUserId(long userId, int pageNumber, int count)
        {
            return await adsService.GetByUserId(userId, pageNumber, count);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<object> GetNotConfirmed(int pageNumber, int count, string search)
        {
            search = search == null ? "" : search;
            return await adsService.GetNotConfirmed(pageNumber, count, search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<object> GetRejected(int pageNumber, int count, string search)
        {
            search = search == null ? "" : search;
            return await adsService.GetRejected(pageNumber, count, search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<Ads> ChangeStatus([FromBody] Ads inputDto)
        {
            var result = await adsService.ChangeAdsStatus(inputDto);
            return result;
        }
        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetAdsByCategory(long categoryId, long firstSearchadsId, int count)
        {
            return await adsService.GetAdsByCategory(categoryId, firstSearchadsId, count);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetAdsByFilter(long? priceFrom, long? priceTo, string title, string tag, 
            long? city, long? state, long firstSearchadsId, int count, long? categoryId)
        {
            return await adsService.GetAdsByFilter(priceFrom, priceTo, title, tag, 
                city, state, firstSearchadsId, count, categoryId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Ladder(long adsId)
        {
            return await adsService.Ladder(adsId);
        }
    }
}
