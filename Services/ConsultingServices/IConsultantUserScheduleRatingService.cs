using BanooClub.Models.DTO;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public interface IConsultantUserScheduleRatingService
    {
        Task<object> Confirm(long? id);
        Task<object> Delete(long? id);
        Task<object> GetById(long? id);
        Task<object> GetList(ConsultantUserScheduleRatingGridFilterDTO input);
    }
}
