using BanooClub.Models.Consulting;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public interface IBecomeConsultantRequestScheduleService
    {
        Task<List<BecomeConsultantRequestSchedule>> CreateCleanIfExist(int? durationMinut, long becomeConsultantRequest);
        Task<List<GetLastBecomeConsultantRequestSchedulesDTO>> GetLastBecomeConsultantRequestSchedules();
        Task<ServiceResult> UpdateIsSelected(UpdateBecomeConsultantRequestSchedulesDTO input);
    }
}
