using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public interface IBecomeConsultantRequestService
    {
        List<IdTitleKeyValueDTO> GetDuration();
        Task<IServiceResult> AcceptRequestAndCreateConsultant(long requestId);
        Task<IServiceResult> CreateBecomeConsultantRequest(CreateBecomeConsultantRequestDTO dto);
        Task<IServiceResult<PageModel<BecomeConsultantRequestDTO>>> GetRequests(int page, int size, BecomeConsultantRequestStatus? status);
        Task<IServiceResult> RejectRequest(long requestId);
        Task<object> GetLast();
        Task<object> GetById(long id);
    }
}
