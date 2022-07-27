using BanooClub.Models;
using BanooClub.Services.AdsCategoryServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdsCategoryController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IAdsCategoryService adsCategoryService;

        public AdsCategoryController(BanooClubDBContext context, IAdsCategoryService adsCategoryService)
        {
            this.context = context;
            this.adsCategoryService = adsCategoryService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] AdsCategory inputDto)
        {
            await this.adsCategoryService.Create(inputDto);
            
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<AdsCategory> Update([FromBody] AdsCategory inputDto)
        {
            var result = await this.adsCategoryService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber,int count,string search)
        {
            search = search ==null ? "" : search;
            return adsCategoryService.GetAll(pageNumber,count,search);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<AdsCategory> Get(long id)
        {
            return adsCategoryService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await adsCategoryService.Delete(id);
        }
    }
}
