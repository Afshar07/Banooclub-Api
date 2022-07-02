using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IBanooClubEFRepository<Payment> paymentRepository;

        public PaymentService(IBanooClubEFRepository<Payment> paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }
        public async Task Create(Payment inputDto)
        {
            paymentRepository.Insert(inputDto);
        }

        public async Task<Payment> Update(Payment item)
        {
            await paymentRepository.Update(item);
            return item;
        }

        public async Task<List<Payment>> GetAll()
        {
            List<Payment> payments = new List<Payment>();
            payments = paymentRepository.GetQuery().ToList();
            return payments;
        }

        public async Task Delete(long id)
        {
            var payment = paymentRepository.GetQuery().FirstOrDefault(z => z.PaymentId == id);
            await paymentRepository.Delete(payment);
        }

        public async Task<Payment> Get(long id)
        {
            var payment = paymentRepository.GetQuery().FirstOrDefault(z => z.PaymentId == id);
            return payment;
        }

    }
}
