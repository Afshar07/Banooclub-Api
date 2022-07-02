using Infrastructure;
using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ServicePropertyServices
{
    public class ServicePropertyService : IServicePropertyService
    {
        private readonly IBanooClubEFRepository<ServiceProperty> servicePropertyRepository;
        public ServicePropertyService(IBanooClubEFRepository<ServiceProperty> servicePropertyRepository)
        {
            this.servicePropertyRepository = servicePropertyRepository;
        }
        public async Task<long> Create(ServiceProperty inputDto)
        {
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = servicePropertyRepository.Insert(inputDto);
            return creation.ServicePropertyId;
        }

        public async Task<ServiceProperty> Update(ServiceProperty item)
        {
            await servicePropertyRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<ServiceProperty> serviceProperties = new List<ServiceProperty>();
            serviceProperties = servicePropertyRepository.GetQuery().Where(z => z.Name.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var servicePropertiesCount = serviceProperties.Count();
            if (pageNumber != 0 && count != 0)
            {
                serviceProperties = serviceProperties.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            var obj = new
            {
                ServiceProperties = serviceProperties,
                ServicePropertiesCount = servicePropertiesCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var serviceProperty = servicePropertyRepository.GetQuery().FirstOrDefault(z => z.ServicePropertyId == id);
                await servicePropertyRepository.Delete(serviceProperty);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ServiceProperty> Get(long id)
        {
            var serviceProperty = servicePropertyRepository.GetQuery().FirstOrDefault(z => z.ServicePropertyId == id);
            return serviceProperty;
        }
    }
}
