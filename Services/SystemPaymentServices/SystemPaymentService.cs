using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.SystemPaymentServices
{
    public class SystemPaymentService : ISystemPaymentService
    {
        private readonly IBanooClubEFRepository<SystemPayment> systemPaymentRepository;

        public SystemPaymentService(IBanooClubEFRepository<SystemPayment> systemPaymentRepository)
        {
            this.systemPaymentRepository = systemPaymentRepository;
        }
        public async Task Create(SystemPayment inputDto)
        {
            systemPaymentRepository.Insert(inputDto);
        }

        public async Task<SystemPayment> Update(SystemPayment item)
        {
            await systemPaymentRepository.Update(item);
            return item;
        }

        public async Task<List<SystemPayment>> GetAll()
        {
            List<SystemPayment> payments = new List<SystemPayment>();
            payments = systemPaymentRepository.GetQuery().ToList();
            return payments;
        }

        public async Task Delete(long id)
        {
            var payment = systemPaymentRepository.GetQuery().FirstOrDefault(z => z.SystemPaymentId == id);
            await systemPaymentRepository.Delete(payment);
        }

        public async Task<SystemPayment> Get(long id)
        {
            var payment = systemPaymentRepository.GetQuery().FirstOrDefault(z => z.SystemPaymentId == id);
            return payment;
        }

    }
}
