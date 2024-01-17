using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ServiceTypeServices
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IBanooClubEFRepository<ServiceType> serviceTypeRepository;

        public ServiceTypeService(IBanooClubEFRepository<ServiceType> serviceTypeRepository)
        {
            this.serviceTypeRepository = serviceTypeRepository;
        }
        public async Task Create(ServiceType inputDto)
        {
            serviceTypeRepository.Insert(inputDto);
        }

        public async Task<ServiceType> Update(ServiceType item)
        {
            await serviceTypeRepository.Update(item);
            return item;
        }

        public async Task<List<ServiceType>> GetAll()
        {
            List<ServiceType> dbServiceTypes = new List<ServiceType>();
            dbServiceTypes = serviceTypeRepository.GetQuery().ToList();
            return dbServiceTypes;
        }

        public async Task Delete(long id)
        {
            var dbServiceType = serviceTypeRepository.GetQuery().FirstOrDefault(z => z.TypeId == id);
            await serviceTypeRepository.Delete(dbServiceType);
        }

        public async Task<ServiceType> Get(long id)
        {
            var dbServiceType = serviceTypeRepository.GetQuery().FirstOrDefault(z => z.TypeId == id);
            return dbServiceType;
        }

    }
}
