using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.Common
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountriesWithRedis();
        Country GetCountryByIso(string iso);
        object GetCountries(int pageNumber, int count);
    }
}
