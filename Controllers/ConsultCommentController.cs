using BanooClub.Models;
using BanooClub.Services.ConcultCommentServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultCommentController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IConsultCommentService consultCommentService;

        public ConsultCommentController(BanooClubDBContext context, IConsultCommentService consultCommentService)
        {
            this.context = context;
            this.consultCommentService = consultCommentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] ConsultComment inputDto)
        {
            await this.consultCommentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ConsultComment> Update([FromBody] ConsultComment inputDto)
        {
            var result = await this.consultCommentService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public object GetAll(int pageNumber, int count)
        {
            return consultCommentService.GetAll(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<ConsultComment> Get(long id)
        {
            return consultCommentService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await consultCommentService.Delete(id);
        }
    }
}
