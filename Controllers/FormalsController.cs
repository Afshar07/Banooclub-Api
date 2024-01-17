using BanooClub.Models;
using BanooClub.Services.FormalServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormalsController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IFormalService FormalService;

        public FormalsController(BanooClubDBContext context, IFormalService formalService)
        {
            this.context = context;
            this.FormalService = formalService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Formal inputDto)
        {
            await this.FormalService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Formal> Update([FromBody] Formal inputDto)
        {
            var result = await this.FormalService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<Formal>> GetAll()
        {
            return FormalService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<List<User>> GetAllUsers(long formalId)
        {
            return FormalService.GetAllUsers(formalId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await FormalService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Formal> Get(long id)
        {
            return FormalService.Get(id);
        }
    }
}
