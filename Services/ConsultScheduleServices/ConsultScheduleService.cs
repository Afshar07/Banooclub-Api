using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultScheduleServices
{
    public class ConsultScheduleService : IConsultScheduleService
    {
        private readonly IBanooClubEFRepository<ConsultSchedule> consultScheduleRepository;

        public ConsultScheduleService(IBanooClubEFRepository<ConsultSchedule> consultScheduleRepository)
        {
            this.consultScheduleRepository = consultScheduleRepository;
        }
        public async Task Create(ConsultSchedule inputDto)
        {
            consultScheduleRepository.Insert(inputDto);
        }

        public async Task<ConsultSchedule> Update(ConsultSchedule item)
        {
            await consultScheduleRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<ConsultSchedule> dbSchedule = new List<ConsultSchedule>();
            dbSchedule = consultScheduleRepository.GetQuery().Skip((pageNumber-1)*count).Take(count).ToList();
            var dbSchedulesCount=consultScheduleRepository.GetQuery().Count();
            var obj = new
            {
                Schedules = dbSchedule,
                ScheduleCounts = dbSchedulesCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var dbSchedule = consultScheduleRepository.GetQuery().FirstOrDefault(z => z.ConsultScheduleId == id);
            await consultScheduleRepository.Delete(dbSchedule);
        }

        public async Task<ConsultSchedule> Get(long id)
        {
            var dbSchedule = consultScheduleRepository.GetQuery().FirstOrDefault(z => z.ConsultScheduleId == id);
            return dbSchedule;
        }

    }
}
