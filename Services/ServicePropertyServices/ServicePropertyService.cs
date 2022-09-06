﻿using Infrastructure;
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
                .AsNoTracking()
                .SingleOrDefault(x => x.ServicePackId == creation.ServiceId);

            if (servicePack != null)
            {
                servicePack.TotalPrice += creation.Price;
                await _servicePackRepostitory.Save();
            }

            return creation.ServicePropertyId;
        }

        public async Task<ServiceProperty> Update(ServiceProperty item)
        {
            var previousPrice = servicePropertyRepository.GetQuery()
                .AsNoTracking()
                .SingleOrDefault(x => x.ServicePropertyId == item.ServicePropertyId)?.Price ?? 0;

            await servicePropertyRepository.Update(item);

            if (item.Price != previousPrice)
            {
                var servicePack = _servicePackRepostitory.GetQuery()
                    .AsNoTracking()
                    .SingleOrDefault(x => x.ServicePackId == item.ServiceId);

                if (servicePack != null)
                {
                    if (previousPrice == 0)
                        servicePack.TotalPrice += item.Price;
                    else if (previousPrice > item.Price)
                        servicePack.TotalPrice -= (previousPrice - item.Price);
                    else
                        servicePack.TotalPrice += (item.Price - previousPrice);

                    await _servicePackRepostitory.Save();
                }
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
