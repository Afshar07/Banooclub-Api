using BanooClub.Models.Consulting;
using BanooClub.Models.DTO;
using BanooClub.Services.Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public class ConsultantUserScheduleRatingService : IConsultantUserScheduleRatingService
    {
        readonly IBanooClubEFRepository<ConsultantUserScheduleRating> _consultantUserScheduleRatingRepository;
        public ConsultantUserScheduleRatingService
            (
                IBanooClubEFRepository<ConsultantUserScheduleRating> consultantUserScheduleRatingRepository
            )
        {
            _consultantUserScheduleRatingRepository = consultantUserScheduleRatingRepository;
        }

        public async Task<object> Confirm(long? id)
        {
            var foundItem = await _consultantUserScheduleRatingRepository
                .GetQuery()
                .Where(t => t.ConsultantUserScheduleRatingId == id)
                .FirstOrDefaultAsync();

            if (foundItem != null)
            {
                foundItem.IsConfirm = true;
                await _consultantUserScheduleRatingRepository.Update(foundItem);
                return new ServiceResult() { IsSuccess = true };
            }

            return new ServiceResult() { IsSuccess = false, ErrorMessage = "اطلاعاتی یافت نشد" };
        }

        public async Task<object> Delete(long? id)
        {
            var foundItem = await _consultantUserScheduleRatingRepository
               .GetQuery()
               .Where(t => t.ConsultantUserScheduleRatingId == id)
               .FirstOrDefaultAsync();

            if (foundItem != null)
            {
                await _consultantUserScheduleRatingRepository.Delete(foundItem);
                return new ServiceResult() { IsSuccess = true };
            }

            return new ServiceResult() { IsSuccess = false, ErrorMessage = "اطلاعاتی یافت نشد" };
        }

        public async Task<object> GetById(long? id)
        {
            return await _consultantUserScheduleRatingRepository
                .GetQuery()
                .Where(t => t.ConsultantUserScheduleRatingId == id)
                .Select(t => new { t.Description })
                .FirstOrDefaultAsync();
        }

        public async Task<object> GetList(ConsultantUserScheduleRatingGridFilterDTO input)
        {
            input = input ?? new ConsultantUserScheduleRatingGridFilterDTO();

            if (input.pageNumber == null || input.pageNumber <= 0)
                input.pageNumber = 1;
            if (input.take == null || input.take <= 0)
                input.take = 10;

            var quiryResult = _consultantUserScheduleRatingRepository.GetQuery();

            if (!string.IsNullOrEmpty(input.consultFullname))
                quiryResult = quiryResult.Where(t => (t.ConsultantUserSchedule.Consultant.User.Name + " " + t.ConsultantUserSchedule.Consultant.User.FamilyName).Contains(input.consultFullname));
            if (!string.IsNullOrEmpty(input.userFullname))
                quiryResult = quiryResult.Where(t => (t.ConsultantUserSchedule.User.Name + " " + t.ConsultantUserSchedule.User.FamilyName).Contains(input.userFullname));
            if (input.rate != null)
                quiryResult = quiryResult.Where(t => t.Rate == input.rate);
            if (input.minRate != null)
                quiryResult = quiryResult.Where(t => t.Rate < input.minRate);
            if (input.maxRate != null)
                quiryResult = quiryResult.Where(t => t.Rate > input.maxRate);
            if (input.createDate != null)
                quiryResult = quiryResult.Where(t => t.CreateDate.Year == input.createDate.Value.Year && t.CreateDate.Month == input.createDate.Value.Month && t.CreateDate.Day == input.createDate.Value.Day);
            if (input.isConfirm != null)
                quiryResult = quiryResult.Where(t => t.IsConfirm == input.isConfirm);


            return new
            {
                total = await quiryResult.CountAsync(),
                data = await quiryResult
                        .OrderByDescending(t => t.ConsultantUserScheduleRatingId)
                        .Skip((input.pageNumber.Value - 1) * input.take.Value)
                        .Take(input.take.Value)
                        .Select(t => new
                        {
                            id = t.ConsultantUserScheduleRatingId,
                            consultFullname = (t.ConsultantUserSchedule.Consultant.User.Name + " " + t.ConsultantUserSchedule.Consultant.User.FamilyName),
                            userFullname = (t.ConsultantUserSchedule.User.Name + " " + t.ConsultantUserSchedule.User.FamilyName),
                            rate = t.Rate,
                            createDate = t.CreateDate,
                            isConfirm = t.IsConfirm
                        })
                        .ToListAsync()
            };
        }
    }
}
