using BanooClub.Models;
using BanooClub.Services.PostNKServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostNKController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IPostNKService postNKService;

        public PostNKController(BanooClubDBContext context, IPostNKService postNKService)
        {
            this.context = context;
            this.postNKService = postNKService;
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Create([FromBody] PostNK inputDto)
        {
            await this.postNKService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<PostNK> Get(long postNKId)
        {
            return postNKService.Get(postNKId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await postNKService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetAll(int pageNumber, int count,string search)
        {
            search = search ==null ? "" : search;
            return await postNKService.GetAll(pageNumber,count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Update([FromBody] PostNK inputDto)
        {
            await this.postNKService.Update(inputDto);
        }

        //[HttpPost]
        //[Route("[action]")]
        //public async Task CreateAll()
        //{
        //    var BadWords = new List<string>()
        //    {

        //    };
        //    foreach (var item in BadWords)
        //    {
        //        var Creation = new PostNK()
        //        {
        //            IsDeleted = false,
        //            Name = item,
        //            PostNkId = 0
        //        };
        //        await this.postNKService.Create(Creation);
        //    }
        //}
    }
}
