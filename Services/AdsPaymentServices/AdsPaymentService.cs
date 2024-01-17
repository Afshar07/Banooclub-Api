using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsPaymentServices
{
    public class AdsPaymentService : IAdsPaymentService
    {
        private readonly IBanooClubEFRepository<AdsPayment> adsPaymentRepository;

        public AdsPaymentService(IBanooClubEFRepository<AdsPayment> adsPaymentRepository)
        {
            this.adsPaymentRepository = adsPaymentRepository;
        }
        public async Task Create(AdsPayment inputDto)
        {
            adsPaymentRepository.Insert(inputDto);
        }

        public async Task<AdsPayment> Update(AdsPayment item)
        {
            await adsPaymentRepository.Update(item);
            return item;
        }



        public object GetAll(int pageNumber, int count)
        {
            List<AdsPayment> dbAds = new List<AdsPayment>();
            dbAds = adsPaymentRepository.GetQuery().Skip((pageNumber-1)*count).Take(count).ToList();
            var AdsPayCount= adsPaymentRepository.GetQuery().Count();
            var obj = new
            {
                AdsPay=dbAds,
                AdsPayCount=AdsPayCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var dbAds = adsPaymentRepository.GetQuery().FirstOrDefault(z => z.AdsPaymentId == id);
            await adsPaymentRepository.Delete(dbAds);
        }

        public async Task<AdsPayment> Get(long id)
        {
            var dbAds = adsPaymentRepository.GetQuery().FirstOrDefault(z => z.AdsPaymentId == id);
            return dbAds;
        }

    }
}
