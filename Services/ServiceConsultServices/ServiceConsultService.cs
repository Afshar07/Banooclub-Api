using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ServiceConsultServices
{
    public class ServiceConsultService : IServiceConsultService
    {
        private readonly IBanooClubEFRepository<ServiceConsult> serviceConsultRepository;

        public ServiceConsultService(IBanooClubEFRepository<ServiceConsult> serviceConsultRepository)
        {
            this.serviceConsultRepository = serviceConsultRepository;
        }
        public async Task Create(ServiceConsult inputDto)
        {
            serviceConsultRepository.Insert(inputDto);
        }

        public async Task<ServiceConsult> Update(ServiceConsult item)
        {
            await serviceConsultRepository.Update(item);
            return item;
        }

        public async Task<List<ServiceConsult>> GetAll()
        {
            List<ServiceConsult> dbServiceConsults = new List<ServiceConsult>();
            dbServiceConsults = serviceConsultRepository.GetQuery().ToList();
            return dbServiceConsults;
        }

        public async Task Delete(long id)
        {
            var dbServiceConsult = serviceConsultRepository.GetQuery().FirstOrDefault(z => z.ConsultId == id);
            await serviceConsultRepository.Delete(dbServiceConsult);
        }

        public async Task<ServiceConsult> Get(long id)
        {
            var dbServiceConsult = serviceConsultRepository.GetQuery().FirstOrDefault(z => z.ConsultId == id);
            return dbServiceConsult;
        }

    }
}
