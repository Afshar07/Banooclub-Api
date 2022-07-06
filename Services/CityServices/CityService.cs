using BanooClub.Models;
using Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanooClub.Services.CityServices
{
    public class CityService : ICityService
    {
        private readonly IBanooClubEFRepository<City> cityRepository;
        public CityService(IBanooClubEFRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public object GetCities(int pageNumber, int count)
        {
            List<City> Cities = new List<City>();
            Cities = cityRepository.GetQuery().ToList();
            var CitiesCount = Cities.Count();
            if (pageNumber != 0 && count != 0)
            {
                Cities = cityRepository.GetQuery().Skip((pageNumber-1)*count).Take(count).ToList();
            }
            var obj = new
            {
                Cities = Cities,
                CitiesCount = CitiesCount
            };
            return obj;
        }

        public object GetCitiesByStateId(int pageNumber, int count, long stateId)
        {
            List<City> Cities = new List<City>();
            if (count ==0 && pageNumber == 0)
            {
                Cities = cityRepository.GetQuery().Where(z => z.StateId == stateId).ToList();
            }
            else
            {
                Cities = cityRepository.GetQuery().Where(z => z.StateId == stateId).Skip((pageNumber-1)*count).Take(count).ToList();
            }

            var CitiesCount = cityRepository.GetQuery().Where(z => z.StateId == stateId).Count();
            var obj = new
            {
                Cities = Cities,
                CitiesCount = CitiesCount
            };
            return obj;
        }
    }
}
