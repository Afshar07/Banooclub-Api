using BanooClub.Models.Consulting;
using BanooClub.Services.Common;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public interface IConsultantService
    {
        Task<ServiceResult> ChageStatus(long? id);
        Task<ServiceResult<long>> CreateOrder(long? id, ConsultTypeEnum? type, System.TimeSpan? targetTime, System.DateTime? targetDate);
        Task<ServiceResult<string>> CreateVideoConfranceRoom(long? id);
        Task<ServiceResult<object>> GetById(long? id);
        Task<object> GetNotEditedList();
        Task<object> GetSchedule(long? id);
    }
}
