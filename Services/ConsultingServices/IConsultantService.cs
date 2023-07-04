using BanooClub.Models.Consulting;
using BanooClub.Models.DTO;
using BanooClub.Services.Common;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public interface IConsultantService
    {
        Task<object> CanComment(long? id);
        Task<ServiceResult> ChageStatus(long? id);
        Task<object> CreateComment(ConsultantNewCommentDTO id);
        Task<ServiceResult<long>> CreateOrder(long? id, ConsultTypeEnum? type, System.TimeSpan? targetTime, System.DateTime? targetDate);
        Task<ServiceResult<string>> CreateVideoConfranceRoom(long? id);
        Task<ServiceResult<object>> GetById(long? id);
        Task<object> GetComments(long? id, int? pageNumber, int? count);
        Task<object> GetNotEditedList(Models.DTO.ConsultGridFilterDTO input);
        Task<object> GetSchedule(long? id, System.Collections.Generic.List<Models.Enums.MyDayOfWeek> days);
    }
}
