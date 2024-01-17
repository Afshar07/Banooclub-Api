using BanooClub.Models;
using BanooClub.Services.AdsChatServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsChatController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IAdsChatService adsChatService;

        public AdsChatController(BanooClubDBContext context, IAdsChatService adsChatService)
        {
            this.context = context;
            this.adsChatService = adsChatService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] AdsChat inputDto)
        {
            await this.adsChatService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<AdsChat> Update([FromBody] AdsChat inputDto)
        {
            var result = await this.adsChatService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public object GetAll(int pageNumber,int count)
        {
            return adsChatService.GetAll(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<AdsChat> Get(long id)
        {
            return adsChatService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await adsChatService.Delete(id);
        }
    }
}
