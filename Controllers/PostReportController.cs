using BanooClub.Models;
using BanooClub.Services.PostReportServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostReportController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IPostReportService postReportService;

        public PostReportController(BanooClubDBContext context, IPostReportService postReportService)
        {
            this.context = context;
            this.postReportService = postReportService;
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task Create([FromBody] PostReport inputDto)
        {
            await this.postReportService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public List<PostReport> GetByUserId(long userId)
        {
            return postReportService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public Task<object> Get(long postReportId)
        {
            return postReportService.Get(postReportId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetAll(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return postReportService.GetAll(pageNumber,count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await postReportService.Delete(id);
        }
    }
}
