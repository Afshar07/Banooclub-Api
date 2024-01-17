using BanooClub.Models.Consulting;
using BanooClub.Models.DTO;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultantUserSchedulesService
{
    public class ConsultantUserSchedulesService : IConsultantUserSchedulesService
    {
        readonly IBanooClubEFRepository<ConsultantUserSchedule> _consultantUserSchedule;
        public ConsultantUserSchedulesService
            (
                IBanooClubEFRepository<ConsultantUserSchedule> consultantUserSchedule
            )
        {
            _consultantUserSchedule = consultantUserSchedule;
        }

        public async Task<object> GetById(long? id)
        {
            return await _consultantUserSchedule
                .GetQuery()
                .Where(t => t.ConsultantUserScheduleId == id)
                .Select(t => new 
                { 
                    description = t.Description 
                })
                .FirstOrDefaultAsync();
        }

        public async Task<object> GetList(ConsultantUserSchedulesGridFilterDTO input)
        {
            var quiryResult = _consultantUserSchedule.GetQuery();

            input = input ?? new ConsultantUserSchedulesGridFilterDTO();
            if (input.pageNumber == null || input.pageNumber <= 0)
                input.pageNumber = 1;
            if (input.take == null || input.take <= 0)
                input.take = 10;

            if (!string.IsNullOrEmpty(input.userFullname))
                quiryResult = quiryResult.Where(t => (t.User.Name + " " + t.User.FamilyName).Contains(input.userFullname));
            if (!string.IsNullOrEmpty(input.consultFullname))
                quiryResult = quiryResult.Where(t => (t.Consultant.User.Name + " " + t.Consultant.User.FamilyName).Contains(input.consultFullname));
            if (input.createDate != null)
                quiryResult = quiryResult.Where(t => t.CreateDate.Date == input.createDate.Value.Date);
            if (input.status != null)
                quiryResult = quiryResult.Where(t => t.Status == input.status.Value);
            if (input.reserveHour != null)
                quiryResult = quiryResult.Where(t => t.ReserveTime == input.reserveHour);
            if (input.isPayed != null)
                quiryResult = quiryResult.Where(t => t.IsPayed == input.isPayed);


            return new 
            {
                total = await quiryResult.CountAsync(),
                data = await quiryResult 
                .OrderByDescending(t => t.ConsultantUserScheduleId)
                .Skip((input.pageNumber.Value - 1) * input.take.Value)
                .Take(input.take.Value)
                .Select(t => new 
                {
                    id = t.ConsultantUserScheduleId,
                    userFullname = (t.User.Name + " " + t.User.FamilyName),
                    consultFullname = (t.Consultant.User.Name + " " + t.Consultant.User.FamilyName),
                    createDate = t.CreateDate,
                    status =  t.Status,
                    reserveHour = t.ReserveTime,
                    isPayed = t.IsPayed
                })
                .ToListAsync()
            };
        }
    }
}
