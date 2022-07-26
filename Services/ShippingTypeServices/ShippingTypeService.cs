using BanooClub.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ShippingTypeServices
{
    public class ShippingTypeService : IShippingTypeService
    {
        private readonly IBanooClubEFRepository<ShippingType> shippingTypeRepository;
        public ShippingTypeService(IBanooClubEFRepository<ShippingType> shippingTypeRepository)
        {
            this.shippingTypeRepository = shippingTypeRepository;
        }
        public async Task<long> Create(ShippingType inputDto)
        {
            var creation = shippingTypeRepository.Insert(inputDto);
            return creation.ShippingTypeId;
        }

        public async Task<ShippingType> Update(ShippingType item)
        {
            await shippingTypeRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<ShippingType> shippingTypes = new List<ShippingType>();
            shippingTypes= shippingTypeRepository.GetQuery().OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var shippingTypesCount = shippingTypeRepository.GetQuery().Count();
            var obj = new
            {
                ShippingTypes = shippingTypes,
                ShippingTypesCount = shippingTypesCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var shippingType = shippingTypeRepository.GetQuery().FirstOrDefault(z => z.ShippingTypeId == id);
                await shippingTypeRepository.Delete(shippingType);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<ShippingType> Get(long id)
        {
            var shippingType = shippingTypeRepository.GetQuery().FirstOrDefault(z => z.ShippingTypeId == id);
            return shippingType;
        }
    }
}
