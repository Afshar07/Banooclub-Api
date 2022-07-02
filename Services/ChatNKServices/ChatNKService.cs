using BanooClub.Models;
using Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanooClub.Services.ChatNKServices
{
    public class ChatNKService : IChatNKService
    {
        private readonly IBanooClubEFRepository<ChatNK> chatNKRepository;
        private readonly IDistributedCache distributedCache;
        public ChatNKService(IBanooClubEFRepository<ChatNK> chatNKRepository,IDistributedCache distributedCache)
        {
            this.chatNKRepository = chatNKRepository;
            this.distributedCache = distributedCache;
        }
        public async Task Create(ChatNK inputDto)
        {
            chatNKRepository.Insert(inputDto);
            var Nks = await chatNKRepository.GetAll();
            var cacheKey = "ChatNkList";
            var serializedCustomerList = JsonConvert.SerializeObject(Nks);
            var ChatNksList = Encoding.UTF8.GetBytes(serializedCustomerList);
            //var options = new DistributedCacheEntryOptions()
            //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await distributedCache.SetAsync(cacheKey, ChatNksList);
        }

        public async Task<ChatNK> Update(ChatNK item)
        {
            await chatNKRepository.Update(item);
            var Nks = await chatNKRepository.GetAll();
            var cacheKey = "ChatNkList";
            var serializedCustomerList = JsonConvert.SerializeObject(Nks);
            var ChatNksList = Encoding.UTF8.GetBytes(serializedCustomerList);
            //var options = new DistributedCacheEntryOptions()
            //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await distributedCache.SetAsync(cacheKey, ChatNksList);
            return item;
        }
        public async Task<List<ChatNK>> GetAllChatNk()
        {
            return chatNKRepository.GetQuery().ToList();
        }

        public async Task<object> GetAll(int pageNumber, int count,string search)
        {
            var cacheKey = "ChatNkList";
            string serializedCountryList;
            var Cntry = new List<ChatNK>();
            var CntryList = await distributedCache.GetAsync(cacheKey);
            if (CntryList != null)
            {
                serializedCountryList = Encoding.UTF8.GetString(CntryList);
                Cntry = JsonConvert.DeserializeObject<List<ChatNK>>(serializedCountryList);
            }
            else
            {
                Cntry = await chatNKRepository.GetAll();
                serializedCountryList = JsonConvert.SerializeObject(Cntry);
                CntryList = Encoding.UTF8.GetBytes(serializedCountryList);
                await distributedCache.SetAsync(cacheKey, CntryList);
            }
            var dbChatNks = Cntry.OrderByDescending(z => z.ChatNKId).Where(z => z.Name.Contains(search)).Skip((pageNumber-1)*count).Take(count).ToList();
            var postNksCount = Cntry.Where(z => z.Name.Contains(search)).Count();
            var obj = new
            {
                ChatNKs = dbChatNks,
                ChatNKsCount = postNksCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var review = chatNKRepository.GetQuery().FirstOrDefault(z => z.ChatNKId == id);
            await chatNKRepository.Delete(review);
            var Nks = await chatNKRepository.GetAll();
            var cacheKey = "ChatNkList";
            var serializedCustomerList = JsonConvert.SerializeObject(Nks);
            var ChatNksList = Encoding.UTF8.GetBytes(serializedCustomerList);
            //var options = new DistributedCacheEntryOptions()
            //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await distributedCache.SetAsync(cacheKey, ChatNksList);
        }

        public async Task<ChatNK> Get(long id)
        {
            var review = chatNKRepository.GetQuery().FirstOrDefault(z => z.ChatNKId == id);
            return review;
        }
    }
}
