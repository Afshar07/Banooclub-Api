using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.LogServices
{
    public class LogService : ILogService
    {
        private readonly IBanooClubEFRepository<Log> logRepository;
        public LogService(IBanooClubEFRepository<Log> logRepository)
        {
            this.logRepository = logRepository;
        }
        public async Task Create(Log inputDto)
        {
            logRepository.Insert(inputDto);
        }

        public async Task<Log> Update(Log item)
        {
            await logRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<Log> logs = new List<Log>();
            logs = logRepository.GetQuery().OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var LogsCount = logRepository.GetQuery().Count();
            var obj = new
            {
                Logs = logs,
                LogsCount = LogsCount,
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var log = logRepository.GetQuery().FirstOrDefault(z => z.LogId == id);
            await logRepository.Delete(log);
        }

        public async Task<Log> Get(long id)
        {
            var log = logRepository.GetQuery().FirstOrDefault(z => z.LogId == id);
            return log;
        }
        public List<Log> GetByUserId(long userId)
        {
            return logRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }
    }
}
