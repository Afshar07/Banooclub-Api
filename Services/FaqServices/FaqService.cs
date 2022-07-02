using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FaqServices
{
    public class FaqService : IFaqService
    {
        private readonly IBanooClubEFRepository<Faq> faqRepository;

        public FaqService(IBanooClubEFRepository<Faq> faqRepository)
        {
            this.faqRepository = faqRepository;
        }
        public async Task Create(Faq inputDto)
        {
            faqRepository.Insert(inputDto);
        }

        public async Task<Faq> Update(Faq item)
        {
            await faqRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<Faq> dbFaqs = new List<Faq>();
            dbFaqs = faqRepository.GetQuery().Skip((pageNumber-1)*count).Take(count).ToList();
            var dbFaqsCount = faqRepository.GetQuery().Count();
            var obj = new
            {
                Faqs = dbFaqs,
                FaqsCount = dbFaqsCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var dbFaq = faqRepository.GetQuery().FirstOrDefault(z => z.FaqId == id);
            await faqRepository.Delete(dbFaq);
        }

        public async Task<Faq> Get(long id)
        {
            var dbFaq = faqRepository.GetQuery().FirstOrDefault(z => z.FaqId == id);
            return dbFaq;
        }

    }
}
