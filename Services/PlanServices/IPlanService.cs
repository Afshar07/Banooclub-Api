using BanooClub.Models;
using BanooClub.Models.Enums;
using System.Threading.Tasks;

namespace BanooClub.Services.PlanServices
{
    public interface IPlanService
    {
        Task Create(Plan inputDto);
        Task<Plan> Update(Plan item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count);
        Task<Plan> Get(long id);
        Task<Plan> GetPanelByType(PlanTypes planType);
    }
}
