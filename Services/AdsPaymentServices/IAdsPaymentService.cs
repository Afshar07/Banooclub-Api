using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsPaymentServices
{
    public interface IAdsPaymentService
    {
        Task Create(AdsPayment inputDto);
        Task<AdsPayment> Update(AdsPayment item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count);
        Task<AdsPayment> Get(long id);
    }
}
