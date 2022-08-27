using BanooClub.Models;
using BanooClub.Models.Enums;
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
        Task<byte> ChangePaymentStatus(long paymentId, PaymentStatus status);
        Task<int> PayByWallet(long orderId);
        Task<object> GetMyPayments(int pageNumber, int count);
        Task<object> GetByUserId(long userId, byte count, short pageNumber);
    }
}