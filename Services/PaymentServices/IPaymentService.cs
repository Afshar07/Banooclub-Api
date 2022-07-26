using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<object> Create(Payment inputDto);
        Task<Payment> Update(Payment item);
        Task<object> GetAll(int pageNumber, int count, string searchCommand);
        Task Delete(long id);
        Task<Payment> Get(long id);
        Task<int> ChangePaymentStatus(string orderId, string transId);
        Task<int> PayByWallet(long orderId);
        Task<object> GetMyPayments(int pageNumber, int count);
    }
}
