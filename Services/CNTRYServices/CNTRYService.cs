using BanooClub.Models;
using Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanooClub.Services.CNTRYServices
{
    public class CNTRYService : ICNTRYService
    {
        private readonly IBanooClubEFRepository<CNTRY> CNTRYRepository;
        private readonly IDistributedCache distributedCache;
        public CNTRYService(IBanooClubEFRepository<CNTRY> CNTRYRepository, IDistributedCache distributedCache)
        {
            this.CNTRYRepository = CNTRYRepository;
            this.distributedCache = distributedCache;
        }
        public async Task<List<CNTRY>> GetCNTRIESWithRedis()
        {
            var cacheKey = "CNTRY";
            string serializedCountryList;
            var Cntry = new List<CNTRY>();
            var CntryList = await distributedCache.GetAsync(cacheKey);
            if (CntryList != null)
            {
                serializedCountryList = Encoding.UTF8.GetString(CntryList);
                Cntry = JsonConvert.DeserializeObject<List<CNTRY>>(serializedCountryList);
            }
            else
            {
                Cntry = await CNTRYRepository.GetAll();
                serializedCountryList = JsonConvert.SerializeObject(Cntry);
                CntryList = Encoding.UTF8.GetBytes(serializedCountryList);
                await distributedCache.SetAsync(cacheKey, CntryList);
            }
            return Cntry;
        }
        public object GetCNTRIES(int pageNumber, int count)
        {
            var CNTRIES = CNTRYRepository.GetQuery().Skip((pageNumber-1)*count).Take(count);
            var CONTRIESCount = CNTRYRepository.GetQuery().Count();
            var obj = new
            {
                CNTRIES = CNTRIES,
                CNTRIESCount = CONTRIESCount
            };
            return obj;
        }
    }
}
