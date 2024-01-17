using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.SystemPaymentServices
{
    public interface ISystemPaymentService
    {
        Task Create(SystemPayment inputDto);
        Task<SystemPayment> Update(SystemPayment item);
        Task Delete(long id);
        Task<List<SystemPayment>> GetAll();
        Task<SystemPayment> Get(long id);
    }
}
