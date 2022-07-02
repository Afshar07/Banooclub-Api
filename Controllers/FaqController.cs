using BanooClub.Models;
using BanooClub.Services.FaqServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFaqService faqService;

        public FaqController(BanooClubDBContext context, IFaqService faqService)
        {
            this.context = context;
            this.faqService = faqService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Faq inputDto)
        {
            await this.faqService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Faq> Update([FromBody] Faq inputDto)
        {
            var result = await this.faqService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public object GetAll(int pageNumber, int count)
        {
            return faqService.GetAll(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Faq> Get(long id)
        {
            return faqService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await faqService.Delete(id);
        }
    }
}
