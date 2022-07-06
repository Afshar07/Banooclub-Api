using Infrastructure;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.RatingServices;
using BanooClub.Services.SocialMediaServices;
using BanooClub.Services.WishListServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ServicePackServices
{
    public class ServicePackService : IServicePackService
    {
        private readonly IBanooClubEFRepository<ServicePack> servicePackRepository;
        private readonly IBanooClubEFRepository<View> viewRepository;
        private readonly IBanooClubEFRepository<Rating> ratingRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly IBanooClubEFRepository<WishList> wishListRepository;
        private readonly IBanooClubEFRepository<Tag> tagRepository;
        private readonly IBanooClubEFRepository<User> userRrepository;
        private readonly IBanooClubEFRepository<ServiceProperty> propertyRepository;
        private readonly IBanooClubEFRepository<ServiceComment> commentRrepository;
        private readonly IRatingService ratingService;
        private readonly ISocialMediaService mediaService;
        private readonly IWishListService wishListService;
        private readonly IHttpContextAccessor _accessor;
        public ServicePackService(IBanooClubEFRepository<ServicePack> servicePackRepository, IBanooClubEFRepository<ServiceComment> commentRrepository, IRatingService ratingService, 
            IBanooClubEFRepository<SocialMedia> mediaRepository,
            IBanooClubEFRepository<WishList> wishListRepository,
            IBanooClubEFRepository<Rating> ratingRepository
            , IHttpContextAccessor accessor, IBanooClubEFRepository<ServiceProperty> propertyRepository, IBanooClubEFRepository<View> viewRepository, IBanooClubEFRepository<User> userRrepository, 
            ISocialMediaService mediaService,
            IWishListService wishListService,
            IBanooClubEFRepository<Tag> tagRepository)
        {
            this.servicePackRepository = servicePackRepository;
            this.viewRepository = viewRepository;
            this.ratingRepository = ratingRepository;
            this.mediaRepository = mediaRepository;
            this.wishListRepository = wishListRepository;
            this.ratingService = ratingService;
            this.mediaService = mediaService;
            this.wishListService = wishListService;
            this.tagRepository = tagRepository;
            this.commentRrepository = commentRrepository;
            this.userRrepository = userRrepository;
            this.servicePackRepository = servicePackRepository;
            this.propertyRepository = propertyRepository;
            _accessor = accessor;
        }
        public async Task<long> Create(ServicePack inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = servicePackRepository.Insert(inputDto);

            foreach(var tag in inputDto.Tags)
            {
                tagRepository.Insert(new Tag()
                {
                    CreateDate = DateTime.Now,
                    IsDeleted = false,
                    ObjectId = creation.ServicePackId,
                    TagId = 0,
                    Title = tag.Title,
                    Type = TagType.Service
                });
            }

            foreach(var prperty in inputDto.Properties)
            {
                propertyRepository.Insert(new ServiceProperty()
                {
                    CreateDate = DateTime.Now,
                    Description = prperty.Description,
                    IsDeleted=false,
                    Name = prperty.Name,
                    Price = prperty.Price,
                    ServiceCategoryId = inputDto.ServiceCategoryId,
                    ServiceId = creation.ServicePackId,
                    ServicePropertyId = 0
                });
            }

            foreach (var item in inputDto.Medias)
            {
                if (!string.IsNullOrEmpty(item.Base64))
                {
                    var outPut = mediaService.SaveImageNew(item.Base64, EntityUrls.Service, item.Priority);
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = creation.ServicePackId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Service,
                        Priority = item.Priority,
                        MediaId = 0,
                        UpdateDate = DateTime.Now,
                    };
                    await mediaRepository.InsertAsync(dbMedia);
                }
            }

            return creation.ServicePackId;
        }

        public async Task<ServicePack> Update(ServicePack item)
        {
            await servicePackRepository.Update(item);
            foreach (var element in item.Medias)
            {
                if (element.Priority ==0)
                {
                    var photo = mediaRepository.GetQuery().FirstOrDefault(z => z.PictureUrl == element.Base64);
                    await mediaRepository.Delete(photo);
                }
                else
                {
                    if (!string.IsNullOrEmpty(element.Base64))
                    {
                        if (element.Priority == 1)
                        {
                            var LastMainMedia = mediaRepository.GetQuery().
                                FirstOrDefault(z => z.ObjectId == item.ServicePackId && z.Type == MediaTypes.Service && z.Priority == 1);
                            if (LastMainMedia != null)
                            {
                                var outPutMain = mediaService.SaveImageNew(element.Base64, EntityUrls.Service, element.Priority);
                                LastMainMedia.PictureUrl = outPutMain.ImageName;
                                LastMainMedia.UpdateDate = DateTime.Now;
                                await mediaRepository.Update(LastMainMedia);
                            }
                            else
                            {
                                var outPut = mediaService.SaveImageNew(element.Base64, EntityUrls.Service, element.Priority);
                                SocialMedia dbMedia = new SocialMedia()
                                {
                                    IsDeleted = false,
                                    ObjectId = item.ServicePackId,
                                    PictureUrl = outPut.ImageName,
                                    Type = MediaTypes.Service,
                                    Priority = element.Priority,
                                    MediaId = 0,
                                    UpdateDate =DateTime.Now
                                };
                                await mediaRepository.InsertAsync(dbMedia);
                            }
                        }
                        else
                        {
                            var outPut = mediaService.SaveImageNew(element.Base64, EntityUrls.Service, element.Priority);
                            SocialMedia dbMedia = new SocialMedia()
                            {
                                IsDeleted = false,
                                ObjectId = item.ServicePackId,
                                PictureUrl = outPut.ImageName,
                                Type = MediaTypes.Service,
                                Priority = element.Priority,
                                MediaId = 0,
                                UpdateDate =DateTime.Now
                            };
                            await mediaRepository.InsertAsync(dbMedia);
                        }

                    }
                }
            }
            return item;
        }

        public async Task<object> GetAll(int pageNumber, int count,string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<ServicePack> servicePacks = new List<ServicePack>();
            servicePacks = servicePackRepository.GetQuery().Where(z => z.Title.Contains(searchCommand)).OrderByDescending(z=>z.CreateDate).ToList();
            var servicesCount = servicePacks.Count();
            
            if(pageNumber != 0 && count != 0)
            {
                servicePacks = servicePacks.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            foreach (var servicePack in servicePacks)
            {
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.Service && z.ObjectId == servicePack.ServicePackId && z.Priority == 1);
                if(dbMedia != null)
                {
                    servicePack.Medias = new List<FileData>();
                    servicePack.Medias.Add(new FileData() { Priority = 1 , Base64 = dbMedia.PictureUrl});
                }
                var dbRate = await ratingService.GetByObjectIdAndType(servicePack.ServicePackId,RatingType.Service);
                servicePack.Rate = dbRate.Data.Average;

                var dbViews = viewRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.ServicePackId && z.Type == ViewType.Service);
                if(dbViews != null)
                {
                    servicePack.ViewsCount = dbViews.Count;
                }
                else
                {
                    servicePack.ViewsCount = 0;
                }
                servicePack.UserInfo = userRrepository.GetQuery().FirstOrDefault(z => z.UserId == servicePack.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z=>z.ObjectId == servicePack.UserId && z.Type == MediaTypes.Profile);
                servicePack.UserInfo.Password = null;
                if(dbUserMedia != null)
                {
                    servicePack.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
                }
            }
            var obj = new
            {
                Services = servicePacks,
                ServicesCount = servicesCount,
            };

            return obj;
        }

        public async Task<object> GetMyServices(int pageNumber, int count, string searchCommand)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<ServicePack> servicePacks = new List<ServicePack>();
            servicePacks = servicePackRepository.GetQuery().Where(z =>z.UserId == userId && z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var servicesCount = servicePacks.Count();

            if (pageNumber != 0 && count != 0)
            {
                servicePacks = servicePacks.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            foreach (var servicePack in servicePacks)
            {
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.Service && z.ObjectId == servicePack.ServicePackId && z.Priority == 1);
                if (dbMedia != null)
                {
                    servicePack.Medias = new List<FileData>();
                    servicePack.Medias.Add(new FileData() { Priority = 1, Base64 = dbMedia.PictureUrl });
                }
                var dbRate = await ratingService.GetByObjectIdAndType(servicePack.ServicePackId, RatingType.Service);
                servicePack.Rate = dbRate.Data.Average;

                var dbViews = viewRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.ServicePackId && z.Type == ViewType.Service);
                if (dbViews != null)
                {
                    servicePack.ViewsCount = dbViews.Count;
                }
                else
                {
                    servicePack.ViewsCount = 0;
                }
                servicePack.UserInfo = userRrepository.GetQuery().FirstOrDefault(z => z.UserId == servicePack.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.UserId && z.Type == MediaTypes.Profile);
                servicePack.UserInfo.Password = null;
                if (dbUserMedia != null)
                {
                    servicePack.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
                }
            }
            var obj = new
            {
                Services = servicePacks,
                ServicesCount = servicesCount,
            };

            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var cmnd = $" Update Service.ServiceComments set IsDeleted = 1 where ServiceId = {id} " +
                    $"Update Common.tags set IsDeleted = 1  where Type = 1 and ObjectId = {id} " +
                    $"Update Service.ServiceProperties set IsDeleted = 1  where ServiceId = {id} " +
                    $"Update Service.ServicePacks set IsDeleted = 1  where ServicePackId ={id} ";
                await servicePackRepository.DapperSqlQuery(cmnd);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ServicePack> Get(long id)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var service = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == id);
            var dbView = viewRepository.GetQuery().FirstOrDefault(z => z.Type == ViewType.Service && z.ObjectId ==id);
            if (dbView == null)
            {
                var dbViewCreation = new View()
                {
                    IsDeleted=false,
                    Count = 1,
                    ObjectId = id,
                    Type = ViewType.Service,
                    ViewId = 0,
                    CreateDate = DateTime.Now
                };
                dbView = viewRepository.Insert(dbViewCreation);
            }
            else
            {
                dbView.Count++;
                await viewRepository.Update(dbView);
            }
            service.ViewsCount = dbView.Count;

            service.Rate = null;
            if (userId >0)
            {
                var dbProductRate = ratingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId==id && z.Type == RatingType.Service);
                service.Rate = dbProductRate == null ? null : (int?)dbProductRate.Rate;
            }

            var dbMedia = mediaRepository.GetQuery().Where(z => z.Type == MediaTypes.Service && z.ObjectId == id).ToList();
            service.Medias = new List<FileData>();
            foreach (var media in dbMedia)
            {
                FileData mediaObj = new FileData()
                {
                    Base64 = media.PictureUrl,
                    Priority = (int)media.Priority
                };
                service.Medias.Add(mediaObj);
            }
            service.Tags = tagRepository.GetQuery().Where(z => z.Type == TagType.Service && z.ObjectId == id).ToList();
            service.UserInfo = userRrepository.GetQuery().FirstOrDefault(z => z.UserId == service.UserId);
            var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z=>z.ObjectId == service.UserId && z.Type == MediaTypes.Profile);
            service.UserInfo.Password = null;
            if(dbUserMedia != null)
            {
                service.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
            }

            service.Comments = commentRrepository.GetQuery().Where(z => z.ServiceId == id).ToList();
            foreach(var cmnt in service.Comments)
            {
                var dbUserInfo = userRrepository.GetQuery().FirstOrDefault(z => z.UserId == cmnt.UserId);
                var dbcmntUserMedia = mediaRepository.GetQuery().FirstOrDefault(z=>z.ObjectId == cmnt.UserId && z.Type == MediaTypes.Profile);
                if(dbcmntUserMedia != null)
                {
                    dbUserInfo.SelfieFileData = dbcmntUserMedia.PictureUrl;
                }
                cmnt.UserInfo = dbUserInfo;
            }
            service.Properties = propertyRepository.GetQuery().Where(z=>z.ServiceId == id).ToList();


            var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ServiceId == service.ServicePackId)
                .FirstOrDefault();
            if(dbWishList!=null)
            {
                service.IsFavourite = true;
            }
            else
            {
                service.IsFavourite = false;
            }

            return service;
        }

        public async Task<object> GetUserServices(int pageNumber, int count, string searchCommand,long userId)
        {
           
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<ServicePack> servicePacks = new List<ServicePack>();
            servicePacks = servicePackRepository.GetQuery().Where(z => z.UserId == userId && z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var servicesCount = servicePacks.Count();

            if (pageNumber != 0 && count != 0)
            {
                servicePacks = servicePacks.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            foreach (var servicePack in servicePacks)
            {
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.Service && z.ObjectId == servicePack.ServicePackId && z.Priority == 1);
                if (dbMedia != null)
                {
                    servicePack.Medias = new List<FileData>();
                    servicePack.Medias.Add(new FileData() { Priority = 1, Base64 = dbMedia.PictureUrl });
                }
                var dbRate = await ratingService.GetByObjectIdAndType(servicePack.ServicePackId, RatingType.Service);
                servicePack.Rate = dbRate.Data.Average;

                var dbViews = viewRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.ServicePackId && z.Type == ViewType.Service);
                if (dbViews != null)
                {
                    servicePack.ViewsCount = dbViews.Count;
                }
                else
                {
                    servicePack.ViewsCount = 0;
                }
                servicePack.UserInfo = userRrepository.GetQuery().FirstOrDefault(z => z.UserId == servicePack.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.UserId && z.Type == MediaTypes.Profile);
                servicePack.UserInfo.Password = null;
                if (dbUserMedia != null)
                {
                    servicePack.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
                }
            }
            var obj = new
            {
                Services = servicePacks,
                ServicesCount = servicesCount,
            };

            return obj;
        }

    }
}
