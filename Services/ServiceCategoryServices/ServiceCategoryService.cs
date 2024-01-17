using Infrastructure;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.SocialMediaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ServiceCategoryServices
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly IBanooClubEFRepository<ServiceCategory> serviceCategoryRepository;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        private readonly ISocialMediaService _mediaService;
        public ServiceCategoryService(IBanooClubEFRepository<ServiceCategory> serviceCategoryRepository , IBanooClubEFRepository<SocialMedia> mediaRepository , ISocialMediaService mediaService)
        {
            this.serviceCategoryRepository = serviceCategoryRepository;
            _mediaRepository = mediaRepository;
            _mediaService = mediaService;
        }
        public async Task<long> Create(ServiceCategory inputDto)
        {
            inputDto.IsDeleted = false;
            var creation = serviceCategoryRepository.Insert(inputDto);
            if (!string.IsNullOrEmpty(inputDto.FileData))
            {
                var outPut = _mediaService.SaveImage(inputDto.FileData, EntityUrls.ServiceCategory);
                SocialMedia dbMedia = new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = creation.ServiceCategoryId,
                    PictureUrl = outPut.ImageName,
                    Type = MediaTypes.ServiceCategory,
                    MediaId = 0,
                    Priority = 1
                };
                await _mediaRepository.InsertAsync(dbMedia);
            }
            return creation.ServiceCategoryId;
        }

        public async Task<ServiceCategory> Update(ServiceCategory item)
        {
            var dbLastCatMedia = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == item.ServiceCategoryId && z.Type == MediaTypes.ServiceCategory);

            if (item.FileData == "Delete")
            {
                if (dbLastCatMedia != null)
                {
                    dbLastCatMedia.PictureUrl = "";
                    await _mediaRepository.Update(dbLastCatMedia);
                    await _mediaRepository.Delete(dbLastCatMedia);
                }

            }
            else if (!string.IsNullOrEmpty(item.FileData))
            {
                var outPut = _mediaService.SaveImage(item.FileData, EntityUrls.ServiceCategory);
                if (outPut.IsSuccess)
                {
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.ServiceCategoryId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.ServiceCategory,
                        MediaId = 0,
                        UpdateDate =DateTime.Now,
                        Priority = 1
                    };

                    if (dbLastCatMedia != null)
                    {
                        dbLastCatMedia.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLastCatMedia);
                        item.FileData = outPut.ImageName;
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                        item.FileData = outPut.ImageName;
                    }
                }
            }
            await serviceCategoryRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<ServiceCategory> serviceCategories = new List<ServiceCategory>();
            serviceCategories = serviceCategoryRepository.GetQuery().Where(z => z.Title.Contains(searchCommand)).OrderByDescending(z => z.ServiceCategoryId).ToList();
            var serviceCategoriesCount = serviceCategories.Count();
            if (pageNumber != 0 && count != 0)
            {
                serviceCategories = serviceCategories.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            foreach(var serviceCategory in serviceCategories)
            {
                var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.ServiceCategory && z.ObjectId == serviceCategory.ServiceCategoryId);
                if(dbMedia != null)
                {
                    serviceCategory.FileData = dbMedia.PictureUrl;
                }
            }
            var obj = new
            {
                ServiceCategories = serviceCategories,
                ServiceCategoriesCount = serviceCategoriesCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var serviceCategory = serviceCategoryRepository.GetQuery().FirstOrDefault(z => z.ServiceCategoryId == id);
                await serviceCategoryRepository.Delete(serviceCategory);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ServiceCategory> Get(long id)
        {
            var serviceCategory = serviceCategoryRepository.GetQuery().FirstOrDefault(z => z.ServiceCategoryId == id);
            var dbMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.ServiceCategory && z.ObjectId == serviceCategory.ServiceCategoryId);
            if (dbMedia != null)
            {
                serviceCategory.FileData = dbMedia.PictureUrl;
            }
            return serviceCategory;
        }
    }
}
