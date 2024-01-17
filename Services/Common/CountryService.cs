using BanooClub.Models;
using Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanooClub.Services.Common
{
    public class CountryService : ICountryService
    {
        private readonly IBanooClubEFRepository<Country> countryRepository;
        private readonly IDistributedCache distributedCache;
        public CountryService(IBanooClubEFRepository<Country> countryRepository, IDistributedCache distributedCache)
        {
            this.countryRepository = countryRepository;
            this.distributedCache = distributedCache;
        }

        public async Task<List<Country>> GetCountriesWithRedis()
        {
            var cacheKey = "Country";
            string serializedCountryList;
            var Cntry = new List<Country>();
            var CntryList = await distributedCache.GetAsync(cacheKey);
            if (CntryList != null)
            {
                serializedCountryList = Encoding.UTF8.GetString(CntryList);
                Cntry = JsonConvert.DeserializeObject<List<Country>>(serializedCountryList);
            }
            else
            {
                Cntry = await countryRepository.GetAll();
                serializedCountryList = JsonConvert.SerializeObject(Cntry);
                CntryList = Encoding.UTF8.GetBytes(serializedCountryList);
                await distributedCache.SetAsync(cacheKey, CntryList);
            }
            return Cntry;
        }
        public Country GetCountryByIso(string iso)
        {
            return countryRepository.GetQuery().FirstOrDefault(z => z.Iso == iso);
        }
        public object GetCountries(int pageNumber, int count)
        {
            var Countries= countryRepository.GetQuery().Skip((pageNumber-1)*count).Take(count);
            var CountriesCount = countryRepository.GetQuery().Count();
            var obj = new
            {
                Countries = Countries,
                CountriesCount = CountriesCount
            };
            return obj;
        }
    }
}
