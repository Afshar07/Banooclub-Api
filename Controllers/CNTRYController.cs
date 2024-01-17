using BanooClub.Models;
using BanooClub.Services.CNTRYServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CNTRYController : ControllerBase
    {
        private readonly ICNTRYService cntryService;
        public CNTRYController(ICNTRYService cntryService)
        {
            this.cntryService = cntryService;
        }

        [HttpGet]
        [Route("[action]")]
        public Task<List<CNTRY>> GetCNTRIESWithRedis()
        {
            return cntryService.GetCNTRIESWithRedis();
        }

        [HttpGet]
        [Route("[action]")]
        public object GETCNTRIES(int pageNumber, int count)
        {
            return cntryService.GetCNTRIES(pageNumber, count);
        }
    }
}
