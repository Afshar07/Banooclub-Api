using System.Threading.Tasks;

namespace BanooClub.Services.DashboardServices
{
    public interface IDashboardService
    {
        Task<object> GetDashboardInfo();
    }
}
