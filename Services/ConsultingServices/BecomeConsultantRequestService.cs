using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public class BecomeConsultantRequestService : IBecomeConsultantRequestService
    {
        public Task<IServiceResult> CreateBecomeConsultantRequest(CreateBecomeConsultantRequestDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IBecomeConsultantRequestService
    {
        Task<IServiceResult> CreateBecomeConsultantRequest(CreateBecomeConsultantRequestDTO dto);
    }
}
