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
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<ServiceProperty> propertyRepository;
        private readonly IBanooClubEFRepository<ServiceComment> commentRepository;
        private readonly IBanooClubEFRepository<ServiceCategory> categoryRepository;
        private readonly IBanooClubEFRepository<ServicePlan> servicePlanRepository;
        private readonly IBanooClubEFRepository<CommentLike> commentLikeRepository;
        private readonly IBanooClubEFRepository<Plan> planRepository;
        private readonly IBanooClubEFRepository<Order> orderRepository;
        private readonly IBanooClubEFRepository<OrderItem> orderItemRepository;
        private readonly IBanooClubEFRepository<Discount> _discountRepository;
        private readonly IBanooClubEFRepository<City> _cityRepository;
        private readonly IBanooClubEFRepository<State> _stateRepository;

        private readonly IRatingService ratingService;
        private readonly ISocialMediaService mediaService;
        private readonly IWishListService wishListService;
        private readonly IHttpContextAccessor _accessor;
        public ServicePackService(IBanooClubEFRepository<ServicePack> servicePackRepository, IBanooClubEFRepository<ServiceComment> commentRepository, IRatingService ratingService,
            IBanooClubEFRepository<SocialMedia> mediaRepository,
            IBanooClubEFRepository<WishList> wishListRepository,
            IBanooClubEFRepository<ServiceCategory> categoryRepository,
            IBanooClubEFRepository<ServicePlan> servicePlanRepository,
            IBanooClubEFRepository<CommentLike> commentLikeRepository,
            IBanooClubEFRepository<Order> orderRepository,
            IBanooClubEFRepository<OrderItem> orderItemRepository,
            IBanooClubEFRepository<Plan> planRepository,
            IBanooClubEFRepository<Rating> ratingRepository
            , IHttpContextAccessor accessor, IBanooClubEFRepository<ServiceProperty> propertyRepository,
            IBanooClubEFRepository<View> viewRepository, IBanooClubEFRepository<User> userRepository,
            ISocialMediaService mediaService,
            IWishListService wishListService,
            IBanooClubEFRepository<Tag> tagRepository
            , IBanooClubEFRepository<Discount> discountRepository, IBanooClubEFRepository<City> cityRepository, IBanooClubEFRepository<State> stateRepository)
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
            this.commentRepository = commentRepository;
            this.categoryRepository = categoryRepository;
            this.servicePlanRepository = servicePlanRepository;
            this.userRepository = userRepository;
            this.servicePackRepository = servicePackRepository;
            this.propertyRepository = propertyRepository;
            this.commentLikeRepository = commentLikeRepository;
            this.planRepository = planRepository;
            this.orderItemRepository = orderItemRepository;
            this.orderRepository = orderRepository;
            _discountRepository = discountRepository;
            _accessor = accessor;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
        }

        public async Task<long> Create(ServicePack inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            inputDto.UserId = userId;
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            inputDto.Status = ServicePackStatus.Pending;
            inputDto.Maintain = inputDto.Quantity;
            var creation = servicePackRepository.Insert(inputDto);

            foreach (var tag in inputDto.Tags)
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

            foreach (var prperty in inputDto.Properties)
            {
                propertyRepository.Insert(new ServiceProperty()
                {
                    CreateDate = DateTime.Now,
                    Description = prperty.Description,
                    IsDeleted = false,
                    Name = prperty.Name,
                    Price = (bool)inputDto.IsFree ? 0 : prperty.Price,
                    IsFree = (bool)inputDto.IsFree ? true : prperty.IsFree,
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
            item.Status = ServicePackStatus.Pending;
            await servicePackRepository.Update(item);
            foreach (var element in item.Medias)
            {
                if (element.Priority == 0)
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
                                    UpdateDate = DateTime.Now
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
                                UpdateDate = DateTime.Now
                            };
                            await mediaRepository.InsertAsync(dbMedia);
                        }

                    }
                }
            }
            return item;
        }

        public async Task<object> GetAll(ServicePackFilterDto searchFilter)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                       ? _accessor.HttpContext.User.Identity.GetUserId()
                       : 0;

            if (searchFilter.SearchCommand == null)
            {
                searchFilter.SearchCommand = "";
            }
            List<ServicePack> servicePacks = new List<ServicePack>();

            string cmd = "";

            if (searchFilter.CategoryId > 0)
                cmd = "select s.* from [Service].[ServicePacks] s " +
                                "inner join [Service].[ServiceCategories] c on s.ServiceCategoryId = c.ServiceCategoryId " +
                                $"where s.IsDeleted='false' And s.ServiceCategoryId = {searchFilter.CategoryId} order by CreateDate desc";
            else
                cmd = "select * from Service.ServicePacks where IsDeleted='false' order by CreateDate desc";

            var DeSerializeObj = await servicePackRepository.DapperSqlQuery(cmd);

            var objSer = JsonSerializer.Serialize<object>(DeSerializeObj);
            servicePacks = JsonSerializer.Deserialize<List<ServicePack>>(objSer);

            if (searchFilter.Status != null)
            {
                servicePacks = servicePacks.Where(z => z.Status == searchFilter.Status).ToList();
            }

            if (searchFilter.FromPrice > 0)
                servicePacks = servicePacks.Where(x => x.TotalPrice >= searchFilter.FromPrice).ToList();

            if (searchFilter.ToPrice > 0)
                servicePacks = servicePacks.Where(x => x.TotalPrice <= searchFilter.ToPrice).ToList();

            if (searchFilter.CityId > 0)
                servicePacks = servicePacks.Where(x => x.CityId == searchFilter.CityId).ToList();

            if (searchFilter.StateId > 0)
                servicePacks = servicePacks.Where(x => x.StateId == searchFilter.StateId).ToList();

            if (searchFilter.HasDiscount)
            {
                var servicePackIds = servicePacks.Select(x => x.ServicePackId).ToList();

                var discountsForServices = _discountRepository.GetQuery()
                    .AsNoTracking()
                    .Where(x => servicePackIds.Contains(x.ServicePackId));

                var finalservicePackIds = new List<long>();

                foreach (var item in discountsForServices)
                {
                    if (item.ExpireDate != null && item.ExpireDate > DateTime.Now)
                        finalservicePackIds.Add(item.ServicePackId);
                    else if (item.StartDate != null && item.StartDate <= DateTime.Now)
                        finalservicePackIds.Add(item.ServicePackId);
                    else if (item.ExpireDate == null && item.StartDate == null)
                        finalservicePackIds.Add(item.ServicePackId);
                }

                servicePacks = servicePacks.Where(x => finalservicePackIds.Contains(x.ServicePackId)).ToList();
            }

            if (searchFilter.IsFunTime)
            {
                var categoryIds = servicePacks.Select(x => x.ServiceCategoryId).ToList();

                var categories = categoryRepository.GetQuery()
                    .AsNoTracking()
                    .Where(x => categoryIds.Contains(x.ServiceCategoryId) && x.IsFunTime)
                    .Select(x => x.ServiceCategoryId);

                servicePacks = servicePacks.Where(x => categories.Contains(x.ServiceCategoryId)).ToList();
            }

            var servicesCount = servicePacks.Count;
            if (searchFilter.PageNumber != 0 && searchFilter.Count != 0)
            {
                servicePacks = servicePacks.Where(z => z.Title.Contains(searchFilter.SearchCommand)).ToList();
                servicePacks = servicePacks.Skip((searchFilter.PageNumber - 1) * searchFilter.Count).Take(searchFilter.Count).ToList();
            }
            else
            {
                servicePacks = servicePacks.Where(z => z.Title.Contains(searchFilter.SearchCommand)).ToList();
            }

            foreach (var servicePack in servicePacks)
            {
                var planIds = servicePlanRepository.GetQuery().Where(z => z.ObjectId == servicePack.ServicePackId && z.Type == ServicePlanType.Service).Select(x => x.PlanId).ToList();
                servicePack.PlanTypes = planRepository.GetQuery().Where(z => planIds.Contains(z.PlanId)).Select(x => x.Type).ToList();
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
                    servicePack.ViewsCount = viewRepository.GetQuery().Where(z => z.Type == ViewType.Service && z.ObjectId == servicePack.ServicePackId).Sum(x => x.Count);
                }
                else
                {
                    servicePack.ViewsCount = 0;
                }

                #region WishList

                var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == servicePack.ServicePackId && x.Type == WishListType.Service)
                .FirstOrDefault();

                if (dbWishList != null)
                {
                    servicePack.IsFavourite = true;
                }
                else
                {
                    servicePack.IsFavourite = false;
                }

                #endregion

                servicePack.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == servicePack.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.UserId && z.Type == MediaTypes.Profile);
                servicePack.UserInfo.Password = null;
                if (dbUserMedia != null)
                {
                    servicePack.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
                }

                servicePack.Discount = _discountRepository.GetQuery().FirstOrDefault(x => x.ServicePackId == servicePack.ServicePackId);
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
            servicePacks = servicePackRepository.GetQuery().Where(z => z.UserId == userId && z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var servicesCount = servicePacks.Count();

            if (pageNumber != 0 && count != 0)
            {
                servicePacks = servicePacks.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            foreach (var servicePack in servicePacks)
            {
                var planIds = servicePlanRepository.GetQuery().Where(z => z.ObjectId == servicePack.ServicePackId && z.Type == ServicePlanType.Service).Select(x => x.PlanId).ToList();
                servicePack.PlanTypes = planRepository.GetQuery().Where(z => planIds.Contains(z.PlanId)).Select(x => x.Type).ToList();
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.Service && z.ObjectId == servicePack.ServicePackId && z.Priority == 1);
                if (dbMedia != null)
                {
                    servicePack.Medias = new List<FileData>();
                    servicePack.Medias.Add(new FileData() { Priority = 1, Base64 = dbMedia.PictureUrl, UploadType = 1 });
                }
                var dbRate = await ratingService.GetByObjectIdAndType(servicePack.ServicePackId, RatingType.Service);
                servicePack.Rate = dbRate.Data.Average;

                var dbViews = viewRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.ServicePackId && z.Type == ViewType.Service);
                if (dbViews != null)
                {
                    servicePack.ViewsCount = viewRepository.GetQuery().Where(z => z.Type == ViewType.Service && z.ObjectId == servicePack.ServicePackId).Sum(x => x.Count);
                }
                else
                {
                    servicePack.ViewsCount = 0;
                }

                servicePack.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == servicePack.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.UserId && z.Type == MediaTypes.Profile);
                servicePack.UserInfo.Password = null;
                if (dbUserMedia != null)
                {
                    servicePack.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
                }

                #region WishList

                var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == servicePack.ServicePackId && x.Type == WishListType.Service)
                .FirstOrDefault();

                if (dbWishList != null)
                {
                    servicePack.IsFavourite = true;
                }
                else
                {
                    servicePack.IsFavourite = false;
                }

                #endregion

                servicePack.Discount = _discountRepository.GetQuery().FirstOrDefault(x => x.ServicePackId == servicePack.ServicePackId);
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
                    $"Update Comment set IsDeleted = 1  where ServiceId = {id} " +
                    $"Update Service.ServicePacks set IsDeleted = 1  where ServicePackId ={id} " +
                    $"Update Common.WishLists set IsDeleted = 1  where Type = 1 and  ObjectId = {id} ";
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
            if (service != null)
                service.Discount = _discountRepository.GetQuery()
                    .FirstOrDefault(x => x.ServicePackId == service.ServicePackId);

            var dbViewCreation = new View()
            {
                IsDeleted = false,
                Count = 1,
                ObjectId = id,
                Type = ViewType.Service,
                ViewId = 0,
                UserId = userId,
                CreateDate = DateTime.Now
            };
            viewRepository.Insert(dbViewCreation);

            service.ViewsCount = viewRepository.GetQuery().Where(z => z.Type == ViewType.Service && z.ObjectId == id).Sum(x => x.Count); ;

            service.Rate = null;
            if (userId > 0)
            {
                var dbProductRate = ratingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId == id && z.Type == RatingType.Service);
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
            service.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == service.UserId);

            if (service.OwnerUserIds != null)
            {
                service.OwnerUserInfos = new List<User>();

                foreach (var ownerId in service.OwnerUserIds)
                {
                    var ownerUser = userRepository.GetQuery()
                        .AsQueryable()
                        .SingleOrDefault(x => x.UserId == ownerId);

                    if (ownerUser != null)
                    {
                        service.OwnerUserInfos.Add(ownerUser);
                    }
                }

                SetAvatarAndBannerForUser(service.OwnerUserInfos?.ToArray());
            }

            if (service.CityId != null && service.CityId != 0)
            {
                service.CityName = _cityRepository.GetQuery()
                    .AsNoTracking()
                    .SingleOrDefault(x => x.CityId == service.CityId)?.Name;
            }

            if (service.StateId != null && service.StateId != 0)
            {
                service.StateName = _stateRepository.GetQuery()
                    .AsNoTracking()
                    .SingleOrDefault(x => x.StateId == service.StateId)?.Name;
            }

            var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == service.UserId && z.Type == MediaTypes.Profile);
            service.UserInfo.Password = null;
            SetAvatarAndBannerForUser(service.UserInfo);

            var selfie = mediaRepository.GetQuery()
                        .FirstOrDefault(z => z.ObjectId == service.UserInfo.UserId && z.Type == MediaTypes.Profile);

            var banner = mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == service.UserInfo.UserId && z.Type == MediaTypes.Banner);

            service.UserInfo.SelfieFileData = selfie == null ? $"{Defaults.Avatars}/{service.UserInfo.Avatar}"
                : $"Media/Gallery/Profile/{selfie.PictureUrl}";

            service.UserInfo.BannerFileData = banner == null ? $"{Defaults.Banners}/{service.UserInfo.DefaultBanner}"
                : $"Media/Gallery/Profile/{banner.PictureUrl}";

            var dbComments = commentRepository.GetQuery().Where(z => z.ServiceId == id).ToList();
            //dbComments.ForEach(z => z.Children = dbComments.Where(x => x.ParentId == z.ServiceCommentId).ToList());
            foreach (var cmnt in dbComments)
            {
                var dbUserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == cmnt.UserId);
                var dbcmntUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == cmnt.UserId && z.Type == MediaTypes.Profile);
                if (dbcmntUserMedia != null)
                {
                    dbUserInfo.SelfieFileData = dbcmntUserMedia.PictureUrl;
                }
                cmnt.UserInfo = dbUserInfo;
                #region CommentLike
                var dbCommentLike = commentLikeRepository.GetQuery().Where(z => z.ObjectId == cmnt.ServiceCommentId && z.Type == CommentType.Service).ToList();
                cmnt.CommentsLikesCount = dbCommentLike.Count();
                #endregion

                var dbLikeCmnt = commentLikeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.Type == CommentType.Service && z.ObjectId == cmnt.ServiceCommentId);
                cmnt.IsLikedByMe = dbLikeCmnt == null ? false : true;

            }
            service.Comments = dbComments;
            service.Properties = propertyRepository.GetQuery().Where(z => z.ServiceId == id).ToList();

            #region WishList
            var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == service.ServicePackId && x.Type == WishListType.Service)
                .FirstOrDefault();
            if (dbWishList != null)
            {
                service.IsFavourite = true;
            }
            else
            {
                service.IsFavourite = false;
            }
            #endregion
            var dbServiceCat = categoryRepository.GetQuery().FirstOrDefault(Z => Z.ServiceCategoryId == service.ServiceCategoryId);
            service.CatName = dbServiceCat == null ? "" : dbServiceCat.Title;
            var planIds = servicePlanRepository.GetQuery().Where(z => z.ObjectId == id && z.Type == ServicePlanType.Service).Select(x => x.PlanId).ToList();
            service.PlanTypes = planRepository.GetQuery().Where(z => planIds.Contains(z.PlanId)).Select(x => x.Type).ToList();
            return service;
        }

        public async Task<object> GetUserServicesByUserName(long lastId, int count, string searchCommand, string userName)
        {
            var userId = userRepository.GetQuery().FirstOrDefault(z => z.UserName == userName).UserId;
            var currentUserId = _accessor.HttpContext.User?.GetUserId() ?? 0;

            if (searchCommand == null)
            {
                searchCommand = "";
            }

            List<ServicePack> servicePacks = new List<ServicePack>();
            servicePacks = servicePackRepository.GetQuery()
                .Where(z => z.UserId == userId
                && z.Title.Contains(searchCommand)
                && z.ExpireDate > DateTime.Now)
                .OrderByDescending(z => z.CreateDate).ToList();

            var servicesCount = servicePacks.Count();

            if (lastId != 0)
            {
                servicePacks = servicePacks.Where(z => z.UserId < lastId).Take(count).ToList();
            }
            foreach (var servicePack in servicePacks)
            {
                var planIds = servicePlanRepository.GetQuery().Where(z => z.ObjectId == servicePack.ServicePackId && z.Type == ServicePlanType.Service).Select(x => x.PlanId).ToList();
                servicePack.PlanTypes = planRepository.GetQuery().Where(z => planIds.Contains(z.PlanId)).Select(x => x.Type).ToList();
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.Service && z.ObjectId == servicePack.ServicePackId && z.Priority == 1);
                if (dbMedia != null)
                {
                    servicePack.Medias = new List<FileData>();
                    servicePack.Medias.Add(new FileData() { Priority = 1, Base64 = dbMedia.PictureUrl });
                }
                var dbRate = await ratingService.GetByObjectIdAndType(servicePack.ServicePackId, RatingType.Service);
                servicePack.Rate = dbRate.Data.Average;

                var dbViewsIsExist = viewRepository.GetQuery().Any(z => z.ObjectId == servicePack.ServicePackId && z.Type == ViewType.Service);
                if (dbViewsIsExist)
                {
                    servicePack.ViewsCount = viewRepository.GetQuery().Where(z => z.Type == ViewType.Service && z.ObjectId == servicePack.ServicePackId).Sum(x => x.Count);
                }
                else
                {
                    servicePack.ViewsCount = 0;
                }

                servicePack.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == servicePack.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.UserId && z.Type == MediaTypes.Profile);

                servicePack.UserInfo.Password = null;
                if (dbUserMedia != null)
                {
                    servicePack.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
                }

                #region WishList

                var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == currentUserId && x.ObjectId == servicePack.ServicePackId && x.Type == WishListType.Service)
                .FirstOrDefault();

                if (dbWishList != null)
                {
                    servicePack.IsFavourite = true;
                }
                else
                {
                    servicePack.IsFavourite = false;
                }

                #endregion

                servicePack.Discount = _discountRepository.GetQuery()
                    .FirstOrDefault(x => x.ServicePackId == servicePack.ServicePackId);
            }

            var obj = new
            {
                Services = servicePacks,
                ServicesCount = servicesCount,
            };

            return obj;
        }

        public async Task<object> GetUserServices(long lastId, int count, string searchCommand, long userId)
        {

            if (searchCommand == null)
            {
                searchCommand = "";
            }

            List<ServicePack> servicePacks = new List<ServicePack>();

            servicePacks = servicePackRepository.GetQuery()
                .Where(z => z.UserId == userId
                && z.Title.Contains(searchCommand)
                && z.ExpireDate > DateTime.Now)
                .OrderByDescending(z => z.CreateDate).ToList();

            var servicesCount = servicePacks.Count();

            if (lastId != 0)
            {
                servicePacks = servicePacks.Where(z => z.UserId < lastId).Take(count).ToList();
            }
            foreach (var servicePack in servicePacks)
            {
                var planIds = servicePlanRepository.GetQuery().Where(z => z.ObjectId == servicePack.ServicePackId && z.Type == ServicePlanType.Service).Select(x => x.PlanId).ToList();
                servicePack.PlanTypes = planRepository.GetQuery().Where(z => planIds.Contains(z.PlanId)).Select(x => x.Type).ToList();
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
                    servicePack.ViewsCount = viewRepository.GetQuery().Where(z => z.Type == ViewType.Service && z.ObjectId == servicePack.ServicePackId).Sum(x => x.Count);
                }
                else
                {
                    servicePack.ViewsCount = 0;
                }



                servicePack.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == servicePack.UserId);
                var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == servicePack.UserId && z.Type == MediaTypes.Profile);

                servicePack.UserInfo.Password = null;
                if (dbUserMedia != null)
                {
                    servicePack.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
                }

                #region WishList
                var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == servicePack.ServicePackId && x.Type == WishListType.Service)
                .FirstOrDefault();
                if (dbWishList != null)
                {
                    servicePack.IsFavourite = true;
                }
                else
                {
                    servicePack.IsFavourite = false;
                }
                #endregion

                servicePack.Discount = _discountRepository.GetQuery()
                    .FirstOrDefault(x => x.ServicePackId == servicePack.ServicePackId);
            }
            var obj = new
            {
                Services = servicePacks,
                ServicesCount = servicesCount,
            };

            return obj;
        }

        public async Task<object> GetDeactiveServices()
        {
            var servicePacks = servicePackRepository.GetQuery().Where(z => z.Status == ServicePackStatus.DeActive).ToList();

            foreach (var servicePack in servicePacks)
            {
                servicePack.Discount = _discountRepository.GetQuery()
                    .FirstOrDefault(x => x.ServicePackId == servicePack.ServicePackId);
            }

            return servicePacks;
        }

        public async Task<bool> ChangeServiceStatus(long servicePackId, ServicePackStatus status)
        {
            try
            {
                var servicePack = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == servicePackId);
                servicePack.Status = status;
                await servicePackRepository.Update(servicePack);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> GetMaintainedByServiceId(long serviceId)
        {
            var dbService = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == serviceId);

            if (dbService != null)
            {
                return dbService.Maintain;
            }

            return 0;
        }

        public async Task<List<ServicePack>> GetAllOrderedService()
        {
            var orderItems = orderItemRepository.GetQuery().Where(z => z.ServiceId != null && z.ServiceId != 0).Select(x => x.ServiceId).Distinct().ToList();
            var services = servicePackRepository.GetQuery().Where(z => orderItems.Contains(z.ServicePackId)).ToList();

            foreach (var servicePack in services)
            {
                servicePack.Discount = _discountRepository.GetQuery()
                    .FirstOrDefault(x => x.ServicePackId == servicePack.ServicePackId);
            }

            return services;
        }

        public async Task<List<ServicePack>> GetOrderedServiceForVendor()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var orderItems = orderItemRepository.GetQuery().Where(z => z.ServiceId != null && z.ServiceId != 0 && z.VendorUserId == userId).Select(x => x.ServiceId).Distinct().ToList();
            var services = servicePackRepository.GetQuery().Where(z => orderItems.Contains(z.ServicePackId)).ToList();

            foreach (var servicePack in services)
            {
                servicePack.Discount = _discountRepository.GetQuery()
                    .FirstOrDefault(x => x.ServicePackId == servicePack.ServicePackId);
            }

            return services;
        }

        public async Task<List<User>> GetServicePayedMember(long serviceId)
        {
            var dbOrderIds = orderItemRepository.GetQuery().Where(z => z.ServiceId == serviceId).Select(x => x.OrderId).ToList();
            var dbUserIds = orderRepository.GetQuery().Where(z => dbOrderIds.Contains(z.OrderId) && z.IsPayed == true).Select(x => x.UserId).ToList();
            var dbUsers = userRepository.GetQuery().Where(z => dbUserIds.Contains(z.UserId)).ToList();
            foreach (var user in dbUsers)
            {
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == user.UserId && z.Type == MediaTypes.Profile);
                user.SelfieFileData = dbMedia == null ? "" : dbMedia.PictureUrl;
            }
            return dbUsers;
        }

        public async Task<object> GetwithView(long id)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var service = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == id);
            var planIds = servicePlanRepository.GetQuery().Where(z => z.ObjectId == id && z.Type == ServicePlanType.Service).Select(x => x.PlanId).ToList();
            service.PlanTypes = planRepository.GetQuery().Where(z => planIds.Contains(z.PlanId)).Select(x => x.Type).ToList();

            var dbViewCreation = new View()
            {
                IsDeleted = false,
                Count = 1,
                ObjectId = id,
                Type = ViewType.Service,
                ViewId = 0,
                CreateDate = DateTime.Now
            };
            viewRepository.Insert(dbViewCreation);

            var viewsQuery = viewRepository.GetQuery()
                .Where(z => z.Type == ViewType.Service && z.ObjectId == id);

            var viewList = viewsQuery.OrderByDescending(x => x.CreateDate).Take(10).ToList();
            var userList = new List<User>();

            viewList.ForEach(view =>
            {
                var user = userRepository.GetQuery()
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == view.UserId);

                if (user != null)
                    userList.Add(user);
            });

            service.ViewsCount = viewsQuery.Sum(x => x.Count);

            #region WeekView

            List<View> WeekViews = viewsQuery.Where(z => z.CreateDate >= DateTime.Now.AddDays(-6)).ToList();
            var start = DateTime.Now.AddDays(-6);
            var today = DateTime.Now;

            for (var date = start; date <= today; date = date.AddDays(1))
                if (!WeekViews.Exists(d => d.CreateDate.Date == date.Date))
                    WeekViews.Add(new View { CreateDate = date, Count = 0, UserId = userId, ObjectId = id, Type = ViewType.Service });
            WeekViews = WeekViews.OrderBy(d => d.CreateDate).ToList();

            service.WeekViews = new
            {
                Data = WeekViews
            };

            #endregion

            service.Rate = null;
            if (userId > 0)
            {
                var dbProductRate = ratingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId == id && z.Type == RatingType.Service);
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
            service.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == service.UserId);

            if (service.OwnerUserIds != null && service.OwnerUserIds.Any())
                foreach (var ownerId in service.OwnerUserIds)
                {
                    var ownerUser = userRepository.GetQuery()
                        .AsQueryable()
                        .SingleOrDefault(x => x.UserId == ownerId);

                    if (ownerUser != null)
                        service.OwnerUserInfos.Add(ownerUser);
                }

            if (service.CityId != null && service.CityId != 0)
            {
                service.CityName = _cityRepository.GetQuery()
                    .AsNoTracking()
                    .SingleOrDefault(x => x.CityId == service.CityId)?.Name;
            }

            if (service.StateId != null && service.StateId != 0)
            {
                service.StateName = _stateRepository.GetQuery()
                    .AsNoTracking()
                    .SingleOrDefault(x => x.StateId == service.StateId)?.Name;
            }

            var dbUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == service.UserId && z.Type == MediaTypes.Profile);
            service.UserInfo.Password = null;
            if (dbUserMedia != null)
            {
                service.UserInfo.SelfieFileData = dbUserMedia.PictureUrl;
            }

            service.Comments = commentRepository.GetQuery().Where(z => z.ServiceId == id).ToList();
            foreach (var cmnt in service.Comments)
            {
                var dbUserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == cmnt.UserId);
                var dbcmntUserMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == cmnt.UserId && z.Type == MediaTypes.Profile);
                if (dbcmntUserMedia != null)
                {
                    dbUserInfo.SelfieFileData = dbcmntUserMedia.PictureUrl;
                }
                cmnt.UserInfo = dbUserInfo;
                #region CommentLike
                var dbCommentLike = commentLikeRepository.GetQuery().Where(z => z.UserId == userId && z.ObjectId == service.ServicePackId && z.Type == CommentType.Service).ToList();
                cmnt.UserCommentLikes = dbCommentLike;
                #endregion
            }
            service.Properties = propertyRepository.GetQuery().Where(z => z.ServiceId == id).ToList();

            #region WishList
            var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == service.ServicePackId && x.Type == WishListType.Service)
                .FirstOrDefault();
            if (dbWishList != null)
            {
                service.IsFavourite = true;
            }
            else
            {
                service.IsFavourite = false;
            }
            #endregion

            var dbServiceCat = categoryRepository.GetQuery().FirstOrDefault(Z => Z.ServiceCategoryId == service.ServiceCategoryId);
            service.CatName = dbServiceCat == null ? "" : dbServiceCat.Title;

            service.Discount = _discountRepository.GetQuery()
                .FirstOrDefault(x => x.ServicePackId == service.ServicePackId);

            var data = new
            {
                ServicePack = service,
                ViewerUsersInfo = userList
            };

            return data;
        }

        #region Utilites

        private void SetAvatarAndBannerForUser(params User[]? users)
        {
            if (users != null && users.Any())
            {
                var list = users.ToList();
                list.ForEach(user =>
                {
                    var selfie = mediaRepository.GetQuery()
                        .FirstOrDefault(z => z.ObjectId == user.UserId && z.Type == MediaTypes.Profile);

                    var banner = mediaRepository.GetQuery()
                        .FirstOrDefault(z => z.ObjectId == user.UserId && z.Type == MediaTypes.Banner);

                    user.SelfieFileData = selfie == null ? $"{Defaults.Avatars}/{user.Avatar}"
                        : $"Media/Gallery/Profile/{selfie.PictureUrl}";

                    user.BannerFileData = banner == null ? $"{Defaults.Banners}/{user.DefaultBanner}"
                        : $"Media/Gallery/Profile/{banner.PictureUrl}";
                });
            }
        }

        #endregion
    }
}