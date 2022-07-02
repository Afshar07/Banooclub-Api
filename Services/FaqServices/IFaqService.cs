using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.FaqServices
{
    public interface IFaqService
    {
        Task Create(Faq inputDto);
        Task<Faq> Update(Faq item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count);
        Task<Faq> Get(long id);
    }
}
