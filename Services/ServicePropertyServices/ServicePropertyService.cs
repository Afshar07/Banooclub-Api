using Infrastructure;
using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BanooClub.Services.ServicePropertyServices
{
    public class ServicePropertyService : IServicePropertyService
    {
        private readonly IBanooClubEFRepository<ServiceProperty> servicePropertyRepository;
        private readonly IBanooClubEFRepository<ServicePack> _servicePackRepostitory;

        public ServicePropertyService(IBanooClubEFRepository<ServiceProperty> servicePropertyRepository,
            IBanooClubEFRepository<ServicePack> servicePackRepostitory)
        {
            this.servicePropertyRepository = servicePropertyRepository;
            _servicePackRepostitory = servicePackRepostitory;
        }
        public async Task<long> Create(ServiceProperty inputDto)
        {
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = servicePropertyRepository.Insert(inputDto);

            var servicePack = _servicePackRepostitory.GetQuery()
                .FirstOrDefault(x => x.ServicePackId == creation.ServiceId);
            if (servicePack != null)
            {
                servicePack.TotalPrice += creation.Price;
                await _servicePackRepostitory.Update(servicePack);
            }

            return creation.ServicePropertyId;
        }

        public async Task<ServiceProperty> Update(ServiceProperty item)
        {
            item.IsDeleted = false;
            await servicePropertyRepository.Update(item);
            var dbServicePack = _servicePackRepostitory.GetQuery().FirstOrDefault(z => z.ServicePackId == item.ServiceId);
            if (dbServicePack != null)
            {
                dbServicePack.TotalPrice = servicePropertyRepository.GetQuery().Where(z => z.ServiceId == dbServicePack.ServicePackId).Sum(x => x.Price);
                await _servicePackRepostitory.Update(dbServicePack);
            }
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
