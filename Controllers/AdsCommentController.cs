using BanooClub.Models;
using BanooClub.Services.AdsCommentServices;
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
    public class AdsCommentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IAdsCommentService adsCommentService;

        public AdsCommentController(BanooClubDBContext context, IAdsCommentService adsCommentService)
        {
            this.context = context;
            this.adsCommentService = adsCommentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] AdsComment inputDto)
        {
            await this.adsCommentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<AdsComment> Update([FromBody] AdsComment inputDto)
        {
            var result = await this.adsCommentService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count)
        {
            return adsCommentService.GetAll( pageNumber,count);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public Task<AdsComment> Get(long id)
        {
            return adsCommentService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task  Delete(long id)
        {
            await adsCommentService.Delete(id);
        }
    }
}
