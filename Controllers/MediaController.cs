using BanooClub.Models;
using BanooClub.Services.MediaServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IMediaService mediaService;

        public MediaController(BanooClubDBContext context, IMediaService mediaService)
        {
            this.context = context;
            this.mediaService = mediaService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Media inputDto)
        {
            await this.mediaService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Media> Update([FromBody] Media inputDto)
        {
            var result = await this.mediaService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<Media>> GetAll()
        {
            return mediaService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Media> Get(long id)
        {
            return mediaService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await mediaService.Delete(id);
        }
    }
}
