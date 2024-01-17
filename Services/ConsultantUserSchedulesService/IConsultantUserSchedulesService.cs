using BanooClub.Models.DTO;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultantUserSchedulesService
{
    public interface IConsultantUserSchedulesService
    {
        Task<object> GetById(long? id);
        Task<object> GetList(ConsultantUserSchedulesGridFilterDTO input);
    }
}
