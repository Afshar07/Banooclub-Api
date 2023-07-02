using BanooClub.Extentions;
using BanooClub.Models.Consulting;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public class BecomeConsultantRequestScheduleService : IBecomeConsultantRequestScheduleService
    {
        readonly IBanooClubEFRepository<BecomeConsultantRequestSchedule> _becomeConsultantRequestScheduleRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        public BecomeConsultantRequestScheduleService
            (
                IBanooClubEFRepository<BecomeConsultantRequestSchedule> becomeConsultantRequestScheduleRepository,
                IHttpContextAccessor httpContextAccessor
            )
        {
            _becomeConsultantRequestScheduleRepository = becomeConsultantRequestScheduleRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<BecomeConsultantRequestSchedule>> CreateCleanIfExist(int? durationMinut, long becomeConsultantRequest)
        {
            List<BecomeConsultantRequestSchedule> result = new List<BecomeConsultantRequestSchedule>();
            if (durationMinut != null && durationMinut.Value > 0 && becomeConsultantRequest > 0)
            {
                var allPrevItems = await _becomeConsultantRequestScheduleRepository.GetQuery().Where(t => t.BecomeConsultantRequestId == becomeConsultantRequest).ToListAsync();
                while (allPrevItems.Count > 0)
                    _becomeConsultantRequestScheduleRepository.Erase(allPrevItems[0]);

                var startSpan = new TimeSpan();
                while ((startSpan + TimeSpan.FromMinutes(durationMinut.Value)).TotalHours < 24)
                {
                    var endSpan = new TimeSpan(startSpan.Hours, startSpan.Minutes, startSpan.Seconds);
                    endSpan += TimeSpan.FromMinutes(durationMinut.Value);
                    var newItem = new BecomeConsultantRequestSchedule()
                    {
                        BecomeConsultantRequestId = becomeConsultantRequest,
                        CreateDate = DateTime.Now,
                        StartTime = startSpan,
                        EntTime = endSpan
                    };
                    await _becomeConsultantRequestScheduleRepository.InsertAsync(newItem);
                    result.Add(newItem);
                    startSpan += TimeSpan.FromMinutes(durationMinut.Value);
                }
            }

            return result;
        }

        public async Task<List<GetLastBecomeConsultantRequestSchedulesDTO>> GetLastBecomeConsultantRequestSchedules()
        {
            List<GetLastBecomeConsultantRequestSchedulesDTO> result = new List<GetLastBecomeConsultantRequestSchedulesDTO>();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (userId > 0)
            {
                result = await _becomeConsultantRequestScheduleRepository
                    .GetQuery()
                    .Where(t => t.BecomeConsultantRequest.UserId == userId)
                    .Select(t => t.BecomeConsultantRequest)
                    .OrderByDescending(t => t.CreateDate)
                    .Take(1)
                    .SelectMany(t => t.BecomeConsultantRequestSchedules)
                    .Select(t => new GetLastBecomeConsultantRequestSchedulesDTO
                    {
                        id = t.BecomeConsultantRequestScheduleId,
                        isSelected = t.IsAvailable,
                        startHour = t.StartTime,
                        endHour = t.EntTime
                    })
                    .ToListAsync();
            }
            return result;
        }

        public async Task<ServiceResult> UpdateIsSelected(UpdateBecomeConsultantRequestSchedulesDTO input)
        {
            if (input == null)
                return new ServiceResult() { IsSuccess = false, ErrorMessage = "لطفا پارامتر های مورد نیاز را ارسال کنید" };
            input.ids = input.ids ?? new List<long>();

            var allAvaliableItems = await GetLastBecomeConsultantRequestSchedules();
            var allIds = allAvaliableItems.Select(t => t.id).ToList();
            var allItems = await _becomeConsultantRequestScheduleRepository.GetQuery().Where(t => allIds.Contains(t.BecomeConsultantRequestScheduleId)).ToListAsync();

            foreach (var item in allItems)
            {
                item.IsAvailable = false;
                if (input.ids.Any(t => t == item.BecomeConsultantRequestScheduleId))
                    item.IsAvailable = true;

                await _becomeConsultantRequestScheduleRepository.Update(item);
            }

            return new ServiceResult() { IsSuccess = true };
        }
    }
}
