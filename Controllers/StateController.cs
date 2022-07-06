using BanooClub.Services.StateServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService stateService;
        public StateController(IStateService stateService)
        {
            this.stateService = stateService;
        }

        [HttpGet]
        [Route("[action]")]
        public object GetStates(int pageNumber, int count)
        {
            return stateService.GetStates(pageNumber, count);
        }
    }
}
