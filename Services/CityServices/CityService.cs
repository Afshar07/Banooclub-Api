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
        private readonly IDistributedCache distributedCache;
        public CityService(IBanooClubEFRepository<City> cityRepository , IDistributedCache distributedCache)
        {
            this.cityRepository = cityRepository;
            this.distributedCache = distributedCache;
        }
        public async Task<List<City>> GetCitiesWithRedis()
        {
            var cacheKey = "City";
            string serializedCountryList;
            var Cntry = new List<City>();
            var CntryList = await distributedCache.GetAsync(cacheKey);
            if (CntryList != null)
            {
                serializedCountryList = Encoding.UTF8.GetString(CntryList);
                Cntry = JsonConvert.DeserializeObject<List<City>>(serializedCountryList);
            }
            else
            {
                Cntry = await cityRepository.GetAll();
                serializedCountryList = JsonConvert.SerializeObject(Cntry);
                CntryList = Encoding.UTF8.GetBytes(serializedCountryList);
                await distributedCache.SetAsync(cacheKey, CntryList);
            }
            return Cntry;
        }
        public object GetCities(int pageNumber, int count)
        {
            var Cities = cityRepository.GetQuery().Skip((pageNumber-1)*count).Take(count);
            var CitiesCount = cityRepository.GetQuery().Count();
            var obj = new
            {
                Cities = Cities,
                CitiesCount = CitiesCount
            };
            return obj;
        }

        public object GetCitiesByCountryId(int pageNumber, int count,long countryId)
        {
            List<City> Cities = new List<City>();
            if(count ==0 && pageNumber == 0)
            {
                Cities = cityRepository.GetQuery().Where(z => z.CountryId == countryId).ToList();
            }
            else
            {
                Cities = cityRepository.GetQuery().Where(z => z.CountryId == countryId).Skip((pageNumber-1)*count).Take(count).ToList();
            }
            
            var CitiesCount = cityRepository.GetQuery().Where(z => z.CountryId == countryId).Count();
            var obj = new
            {
                Cities = Cities,
                CitiesCount = CitiesCount
            };
            return obj;
        }
    }
}
