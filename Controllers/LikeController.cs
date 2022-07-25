using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.LikeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService likeService;

        public LikeController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<LikeResponse> Create([FromBody] Like inputDto)
        {
            return await likeService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<List<Like>> GetAll()
        {
            return await likeService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Like> Get(long likeId)
        {
            return likeService.Get(likeId);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task Delete(long id)
        {
            await likeService.Delete(id);
        }
    }
}
