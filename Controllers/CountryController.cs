using BanooClub.Models;
using BanooClub.Services.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;
        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        [Route("[action]")]
        public Task<List<Country>> GetCountriesWithRedis()
        {
            return countryService.GetCountriesWithRedis();
        }

        [HttpGet]
        [Route("[action]")]
        public object GetCountries(int pageNumber, int count)
        {
            return countryService.GetCountries(pageNumber,count);
        }

        [HttpGet]
        [Route("[action]")]
        public Country GetCountryByIso(string iso)
        {
            return countryService.GetCountryByIso(iso);
        }
    }
}
