using BanooClub.Models;
using Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanooClub.Services.PostNKServices
{
    public class PostNKService : IPostNKService
    {
        private readonly IBanooClubEFRepository<PostNK> postNKRepository;
        private readonly IDistributedCache distributedCache;
        public PostNKService(IBanooClubEFRepository<PostNK> postNKRepository, IDistributedCache distributedCache)
        {
            this.postNKRepository = postNKRepository;
            this.distributedCache = distributedCache;
        }
        public async Task Create(PostNK inputDto)
        {
            postNKRepository.Insert(inputDto);
            var Nks = await postNKRepository.GetAll();
            var cacheKey = "PostNK";
            var serializedCustomerList = JsonConvert.SerializeObject(Nks);
            var PostNksList = Encoding.UTF8.GetBytes(serializedCustomerList);
            //var options = new DistributedCacheEntryOptions()
            //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await distributedCache.SetAsync(cacheKey, PostNksList);
        }

        public async Task<PostNK> Update(PostNK item)
        {
            await postNKRepository.Update(item);
            var Nks = await postNKRepository.GetAll();
            var cacheKey = "PostNK";
            var serializedCustomerList = JsonConvert.SerializeObject(Nks);
            var PostNksList = Encoding.UTF8.GetBytes(serializedCustomerList);
            //var options = new DistributedCacheEntryOptions()
            //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await distributedCache.SetAsync(cacheKey, PostNksList);
            return item;
        }

        public async Task<List<PostNK>> GetAllPostNk()
        {
            return postNKRepository.GetQuery().ToList();
        }

        public async Task<object> GetAll(int pageNumber, int count, string search)
        {
            var cacheKey = "PostNK";
            string serializedCountryList;
            var Cntry = new List<PostNK>();
            var CntryList = await distributedCache.GetAsync(cacheKey);
            if (CntryList != null)
            {
                serializedCountryList = Encoding.UTF8.GetString(CntryList);
                Cntry = JsonConvert.DeserializeObject<List<PostNK>>(serializedCountryList);
            }
            else
            {
                Cntry = await postNKRepository.GetAll();
                serializedCountryList = JsonConvert.SerializeObject(Cntry);
                CntryList = Encoding.UTF8.GetBytes(serializedCountryList);
                await distributedCache.SetAsync(cacheKey, CntryList);
            }
            var dbPostNKS = Cntry.OrderByDescending(z=>z.PostNkId).Where(z=>z.Name.Contains(search)).Skip((pageNumber-1)*count).Take(count).ToList();
            var postNksCount= Cntry.Where(z => z.Name.Contains(search)).Count();
            var obj = new
            {
                PostNKs = dbPostNKS,
                PostNKsCount = postNksCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var review = postNKRepository.GetQuery().FirstOrDefault(z => z.PostNkId == id);
            await postNKRepository.Delete(review);
            var Nks = await postNKRepository.GetAll();
            var cacheKey = "PostNK";
            var serializedCustomerList = JsonConvert.SerializeObject(Nks);
            var PostNksList = Encoding.UTF8.GetBytes(serializedCustomerList);
            //var options = new DistributedCacheEntryOptions()
            //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await distributedCache.SetAsync(cacheKey, PostNksList);
        }

        public async Task<PostNK> Get(long id)
        {
            var review = postNKRepository.GetQuery().FirstOrDefault(z => z.PostNkId == id);
            return review;
        }
    }
}
