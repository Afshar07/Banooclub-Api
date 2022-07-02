using BanooClub.Models;
using BanooClub.Services.CityServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public Task<List<City>> GetCitiesWithRedis()
        {
            return cityService.GetCitiesWithRedis();
        }

        [HttpGet]
        [Route("[action]")]
        public object GetCities(int pageNumber, int count)
        {
            return cityService.GetCities(pageNumber, count);
        }

        [HttpGet]
        [Route("[action]")]
        public object GetCitiesByCountryId(int pageNumber, int count,long countryId)
        {
            return cityService.GetCitiesByCountryId(pageNumber, count,countryId);
        }
    }
}
