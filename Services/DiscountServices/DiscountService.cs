using BanooClub.Models;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using MoreLinq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly IBanooClubEFRepository<Discount> _discountRepository;
        private readonly IBanooClubEFRepository<ServicePack> _servicePackRepository;
        private readonly IHttpContextAccessor _accessor;

        public DiscountService(IBanooClubEFRepository<Discount> discountRepository,
            IHttpContextAccessor accessor,
            IBanooClubEFRepository<ServicePack> servicePackRepository)
        {
            _discountRepository = discountRepository;
            _accessor = accessor;
            _servicePackRepository = servicePackRepository;
        }

        public async Task<object> Create(DiscountDto inputDto)
        {
            int Response = 1;

            var LastDbDiscount = _discountRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == inputDto.ServicePackId);
            var dbDiscount = new Discount();
            if (LastDbDiscount == null)
            {
                dbDiscount = new Discount()
                {
                    DiscountId = 0,
                    CreateDate = DateTime.Now,
                    ExpireDate = inputDto.ExpireDate,
                    IsDeleted = false,
                    ServicePackId = inputDto.ServicePackId,
                    Type = inputDto.Type,
                    Value = inputDto.Value,
                    Refraction = inputDto.Refraction,
                    StartDate = inputDto.StartDate
                };
                _discountRepository.Insert(dbDiscount);
            }
            else
            {
                Response = 2;
            }

            var obj = new
            {
                DiscountId = LastDbDiscount is null ? dbDiscount.DiscountId : LastDbDiscount.DiscountId,
                Message = Response
            };
            return obj;
        }

        public async Task<Discount> Update(Discount item)
        {
            await _discountRepository.Update(item);
            return item;
        }

        public async Task<object> GetAll(int pageNumber, int count)
        {
            var discounts = _discountRepository.GetQuery().OrderByDescending(z => z.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            var DiscountCount = _discountRepository.GetQuery().Count();

            var obj = new
            {
                Discounts = discounts,
                Counts = DiscountCount
            };

            return obj;
        }

        public async Task<object> GetLatestDiscount(string searchCommand, byte count, short pageNumber)
        {
            try
            {
                var query = _servicePackRepository.GetQuery()
                    .Where(z => z.ExpireDate > DateTime.Now);

                if (!string.IsNullOrWhiteSpace(searchCommand))
                    query = query.Where(x => x.Title.Contains(searchCommand));

                var servicePacks = query.OrderByDescending(x => x.CreateDate)
                    .Skip((pageNumber - 1) * count)
                    .Take(count).ToList();

                servicePacks.ForEach(servicePack =>
                {
                    var discount = _discountRepository.GetQuery().FirstOrDefault(x => x.ServicePackId == servicePack.ServicePackId
                    && x.ExpireDate > DateTime.Now);

                    servicePack.Discount = discount is null ? null : discount;
                });

                var obj = new
                {
                    ServicePacks = servicePacks,
                    ServicePackCount = query.Count()
                };

                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> GetAllRefraction(int pageNumber, int count)
        {
            try
            {
                var query = _discountRepository.GetQuery()
                    .Where(z => z.Refraction == true && z.ExpireDate > DateTime.Now);

                var obj = new
                {
                    Discounts = query
                    .OrderByDescending(x => x.CreateDate)
                    .Skip((pageNumber - 1) * count)
                    .Take(count).ToList(),
                    DiscountCount = query.Count()
                };

                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task Delete(long id)
        {
            var discount = _discountRepository.GetQuery().FirstOrDefault(z => z.DiscountId == id);
            await _discountRepository.Delete(discount);
        }

        public async Task<Discount> Get(long id)
        {
            var discount = _discountRepository.GetQuery().FirstOrDefault(z => z.DiscountId == id);
            return discount;
        }
    }
}