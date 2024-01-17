using BanooClub.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ShippingServices
{
    public class ShippingService : IShippingService
    {
        private readonly IBanooClubEFRepository<Shipping> shippingRepository;
        public ShippingService(IBanooClubEFRepository<Shipping> shippingRepository)
        {
            this.shippingRepository = shippingRepository;
        }
        public async Task<long> Create(Shipping inputDto)
        {
            var creation = shippingRepository.Insert(inputDto);
            return creation.ShippingId;
        }

        public async Task<Shipping> Update(Shipping item)
        {
            await shippingRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<Shipping> shippings= new List<Shipping>();
            shippings= shippingRepository.GetQuery().OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var shippingsCount = shippingRepository.GetQuery().Count();
            var obj = new
            {
                Shippings = shippings,
                ShippingsCount = shippingsCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var shipping = shippingRepository.GetQuery().FirstOrDefault(z => z.ShippingId == id);
                await shippingRepository.Delete(shipping);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
            
        }

        public async Task<Shipping> Get(long id)
        {
            var shipping = shippingRepository.GetQuery().FirstOrDefault(z => z.ShippingId == id);
            return shipping ;
        }
    }
}
