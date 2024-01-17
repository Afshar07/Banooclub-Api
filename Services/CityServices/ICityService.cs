using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.CityServices
{
    public interface ICityService
    {
        object GetCities(int pageNumber, int count);
        object GetCitiesByStateId(int pageNumber, int count, long stateId);
    }
}
