using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.CityServices
{
    public interface ICityService
    {
        Task<List<City>> GetCitiesWithRedis();
        object GetCities(int pageNumber, int count);
        object GetCitiesByCountryId(int pageNumber, int count, long countryId);
    }
}
