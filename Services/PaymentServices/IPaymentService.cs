using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task Create(Payment inputDto);
        Task<Payment> Update(Payment item);
        Task Delete(long id);
        Task<List<Payment>> GetAll();
        Task<Payment> Get(long id);
    }
}
