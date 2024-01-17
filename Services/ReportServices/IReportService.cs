using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ReportServices
{
    public interface IReportService
    {
        Task Create(Report inputDto);
        Task<Report> Update(Report item);
        Task Delete(long id);
        Task<List<Report>> GetAll();
        Task<Report> Get(long id);
    }
}
