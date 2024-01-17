using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly IBanooClubEFRepository<Report> reportRepository;

        public ReportService(IBanooClubEFRepository<Report> reportRepository)
        {
            this.reportRepository = reportRepository;
        }
        public async Task Create(Report inputDto)
        {
            reportRepository.Insert(inputDto);
        }

        public async Task<Report> Update(Report item)
        {
            await reportRepository.Update(item);
            return item;
        }

        public async Task<List<Report>> GetAll()
        {
            List<Report> reports = new List<Report>();
            reports = reportRepository.GetQuery().ToList();
            return reports;
        }

        public async Task Delete(long id)
        {
            var report = reportRepository.GetQuery().FirstOrDefault(z => z.ReportId == id);
            await reportRepository.Delete(report);
        }

        public async Task<Report> Get(long id)
        {
            var report = reportRepository.GetQuery().FirstOrDefault(z => z.ReportId == id);
            return report;
        }

    }
}
