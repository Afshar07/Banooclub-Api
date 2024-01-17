using BanooClub.Services.CityServices;
using Microsoft.AspNetCore.Mvc;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;
        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        [Route("[action]")]
        public object GetCities(int pageNumber, int count)
        {
            return cityService.GetCities(pageNumber, count);
        }

        [HttpGet]
        [Route("[action]")]
        public object GetCitiesByStateId(int pageNumber, int count, long stateId)
        {
            return cityService.GetCitiesByStateId(pageNumber, count, stateId);
        }
    }
}
