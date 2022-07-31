using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.CityServices;
using BanooClub.Services.CNTRYServices;
using BanooClub.Services.Common;
using BanooClub.Services.SocialMediaServices;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsServices
{
    public class AdsService : IAdsService
    {
        private readonly IBanooClubEFRepository<Ads> adsRepository;
        private readonly IBanooClubEFRepository<AdsCategory> adsCategoryRepository;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        private readonly IBanooClubEFRepository<UserSetting> userSettingRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<Activity> _activityRepository;
        private readonly IBanooClubEFRepository<State> _stateRepository;
        private readonly IBanooClubEFRepository<City> _cityRepository;
        private readonly IBanooClubEFRepository<WishList> wishListRepository;
        private readonly IBanooClubEFRepository<ServicePlan> servicePlanRepository;
        private readonly IBanooClubEFRepository<Plan> PlanRepository;
        private readonly ICityService cityService;
        private readonly ISocialMediaService _mediaService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService userService;
        private readonly ICountryService countryService;

        public AdsService(IBanooClubEFRepository<Ads> adsRepository
            , IBanooClubEFRepository<Activity> activityRepoository
            , ISocialMediaService mediaService
            , IBanooClubEFRepository<WishList> wishListRepository
            , IBanooClubEFRepository<SocialMedia> mediaRepository
            , IBanooClubEFRepository<City> cityRepository
            , IBanooClubEFRepository<UserSetting> userSettingRepository
            , IBanooClubEFRepository<Following> followingRepository
            , ICountryService countryService
            , IBanooClubEFRepository<State> stateRepository
            , IBanooClubEFRepository<ServicePlan> servicePlanRepository
            , IBanooClubEFRepository<AdsCategory> adsCategoryRepository
            , IBanooClubEFRepository<Plan> PlanRepository
            , IHttpContextAccessor accessor
            , ICityService cityService
            , ICNTRYService CNTRYService
            , IUserService userService)
        {
            this.adsRepository = adsRepository;
            _mediaService = mediaService;
            _mediaRepository = mediaRepository;
            _accessor = accessor;
            _activityRepository = activityRepoository;
            this.userService = userService;
            this.userSettingRepository = userSettingRepository;
            this.followingRepository = followingRepository;
            this.adsCategoryRepository=adsCategoryRepository;
            this.countryService=countryService;
            this.cityService=cityService;
            this.servicePlanRepository = servicePlanRepository;
            _stateRepository =stateRepository;
            _cityRepository = cityRepository;
            this.wishListRepository = wishListRepository;
            this.PlanRepository = PlanRepository;
        }
        public async Task Create(Ads inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            inputDto.IsDeleted = false;
            inputDto.Status = (int)AdsStatus.NotConfirmed;
            
            if (inputDto.AdsId > 0 && inputDto !=null)
            {
                await Update(inputDto);
            }
            else
            {
                inputDto.UpdateDate = DateTime.Now;
                inputDto.CreateDate = DateTime.Now;
                inputDto.FireDate = DateTime.Now;
                var dbAd = adsRepository.Insert(inputDto);

                foreach (var item in inputDto.Photos)
                {
                    if (!string.IsNullOrEmpty(item.Base64))
                    {
                        var outPut = _mediaService.SaveImage(item.Base64, EntityUrls.AdsPhoto);
                        SocialMedia dbMedia = new SocialMedia()
                        {
                            IsDeleted = false,
                            ObjectId = dbAd.AdsId,
                            PictureUrl = outPut.ImageName,
                            Type = MediaTypes.AdsPhoto,
                            Priority = item.Priority,
                            MediaId = 0
                        };
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
                _activityRepository.Insert(new Activity()
                    {
                        CreateDate=DateTime.Now,
                        IsDeleted=false,
                        ActivityId=0,
                        ObjectId=dbAd.AdsId,
                        UserId=inputDto.UserId,
                        Type=ActivityTypes.CreateAd
                    }
                );
            }
            
            
        }
        public async Task<bool> Ladder(long AdsId)
        {
            try
            {
                var dbAd = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId==AdsId);
                dbAd.UpdateDate = DateTime.Now;
                await adsRepository.Update(dbAd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Ads> Update(Ads item)
        {
            foreach (var element in item.Photos)
            {
                if (element.Priority ==0)
                {
                    var photo = _mediaRepository.GetQuery().FirstOrDefault(z => z.PictureUrl == element.Base64);
                    await _mediaRepository.Delete(photo);
                }
                else
                {
                    if (!string.IsNullOrEmpty(element.Base64))
                    {
                        if (element.Priority == 1)
                        {
                            var LastMainMedia = _mediaRepository.GetQuery().
                                FirstOrDefault(z => z.ObjectId == item.AdsId && z.Type == MediaTypes.AdsPhoto && z.Priority == 1);
                            if (LastMainMedia != null)
                            {
                                var outPutMain = _mediaService.SaveImageNew(element.Base64, EntityUrls.AdsPhoto, element.Priority);
                                LastMainMedia.PictureUrl = outPutMain.ImageName;
                                LastMainMedia.UpdateDate = DateTime.Now;
                                await _mediaRepository.Update(LastMainMedia);
                            }
                            else
                            {
                                var outPut = _mediaService.SaveImageNew(element.Base64, EntityUrls.AdsPhoto, element.Priority);
                                SocialMedia dbMedia = new SocialMedia()
                                {
                                    IsDeleted = false,
                                    ObjectId = item.AdsId,
                                    PictureUrl = outPut.ImageName,
                                    Type = MediaTypes.AdsPhoto,
                                    Priority = element.Priority,
                                    MediaId = 0,
                                    UpdateDate =DateTime.Now
                                };
                                await _mediaRepository.InsertAsync(dbMedia);
                            }
                        }
                        else
                        {
                            var outPut = _mediaService.SaveImageNew(element.Base64, EntityUrls.AdsPhoto, element.Priority);
                            SocialMedia dbMedia = new SocialMedia()
                            {
                                IsDeleted = false,
                                ObjectId = item.AdsId,
                                PictureUrl = outPut.ImageName,
                                Type = MediaTypes.AdsPhoto,
                                Priority = element.Priority,
                                MediaId = 0,
                                UpdateDate =DateTime.Now
                            };
                            await _mediaRepository.InsertAsync(dbMedia);
                        }

                    }
                }
            }


            //var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == item.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
            //foreach (var photo in dbPhotos)
            //{
            //    await _mediaRepository.Delete(photo);
            //}
            //foreach (var element in item.Photos)
            //{
            //    if (!string.IsNullOrEmpty(element.Base64))
            //    {
            //        var outPut = _mediaService.SaveImage(element.Base64, EntityUrls.AdsPhoto);
            //        SocialMedia dbMedia = new SocialMedia()
            //        {
            //            IsDeleted = false,
            //            ObjectId = item.AdsId,
            //            PictureUrl = outPut.ImageName,
            //            Type = MediaTypes.AdsPhoto,
            //            Priority = element.Priority,
            //            MediaId = 0
            //        };
            //        await _mediaRepository.InsertAsync(dbMedia);
            //    }
            //}



            item.IsDeleted = false;
            item.Status = (int)AdsStatus.NotConfirmed;
            //item.UpdateDate = DateTime.Now;
            await adsRepository.Update(item);

            _activityRepository.Insert(new Activity()
            {
                CreateDate=DateTime.Now,
                IsDeleted=false,
                ActivityId=0,
                ObjectId=item.AdsId,
                UserId=item.UserId,
                Type=ActivityTypes.UpdateAd
            }
                );
            return item;
        }
        public async Task<object> GetAll(int pageNumber, int count, string search)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            List<Ads> dbAds = new List<Ads>();
            dbAds = adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published && (z.Title.Contains(search) || z.Description.Contains(search))).
                OrderByDescending(x => x.FireDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var AdsCount=adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published && (z.Title.Contains(search) || z.Description.Contains(search))).Count();
            foreach (var ad in dbAds)
            {
                ad.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        ad.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/AdsPhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }
                ad.UserInfo=userService.Get(ad.UserId);
                //ad.AdsCategoryParents = GetAdsParents(ad.CategoryId,"");
                var cat = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == ad.CategoryId);
                ad.AdsCategoryParents = cat == null ? "" : cat.Name;
                var dbState = _stateRepository.GetQuery().FirstOrDefault(z => z.StateId == ad.StateId);
                ad.State = dbState == null ? "" : dbState.Name;
                var dbCity = _cityRepository.GetQuery().FirstOrDefault(z => z.CityId == ad.CityId);
                ad.City = dbCity == null ? "" : dbCity.Name;
                var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == ad.AdsId && x.Type == WishListType.Ads)
                .FirstOrDefault();
                if (dbWishList!=null)
                {
                    ad.IsFavourite = true;
                }
                else
                {
                    ad.IsFavourite = false;
                }
            }
            var obj = new
            {
                Ads = dbAds,
                AdsCount = AdsCount,
            };
            return obj;
        }
        public async Task<bool> Delete(long id)
        {
            try
            {
                var dbAds = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == id);
                var dbwishLists = wishListRepository.GetQuery().Where(z=>z.Type == WishListType.Ads && z.ObjectId == id).ToList();
                foreach(var wish in dbwishLists)
                {
                    await wishListRepository.Delete(wish);
                }
                await adsRepository.Delete(dbAds);

                _activityRepository.Insert(new Activity()
                {
                    CreateDate=DateTime.Now,
                    IsDeleted=false,
                    ActivityId=0,
                    ObjectId=id,
                    UserId=dbAds.UserId,
                    Type=ActivityTypes.DeleteAd
                }
                    );
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        public async Task<Ads> Get(long id)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var ad = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == id);
            ad.Photos = new List<FileData>();
            var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
            foreach (var photo in dbPhotos)
            {
                if (photo != null )
                {
                    if (!string.IsNullOrEmpty(photo.PictureUrl.Trim()))
                    {
                        ad.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/AdsPhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }
            }
            var dbState = _stateRepository.GetQuery().FirstOrDefault(z=>z.StateId == ad.StateId);
            ad.State = dbState == null ? "" : dbState.Name;
            var dbCity = _cityRepository.GetQuery().FirstOrDefault(x => x.CityId == ad.CityId);
            ad.City = dbCity == null ? "" : dbCity.Name;
            ad.UserInfo = userService.Get(ad.UserId);

            var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == ad.AdsId && x.Type == WishListType.Ads)
                .FirstOrDefault();
            if (dbWishList!=null)
            {
                ad.IsFavourite = true;
            }
            else
            {
                ad.IsFavourite = false;
            }
            var cat = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == ad.CategoryId);
            ad.AdsCategoryParents = cat == null ? "" : cat.Name;
            //ad.AdsCategoryParents = GetAdsParents(ad.CategoryId,"");
            return ad;
        }
        public async Task<object> GetMyAds(int pageNumber, int count)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            List<Ads> dbAds = new List<Ads>();
            dbAds = adsRepository.GetQuery().Where(z => z.UserId == userId).
                OrderByDescending(x => x.FireDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var AdsCount = adsRepository.GetQuery().Where(z => z.UserId == userId).Count();
            foreach (var ad in dbAds)
            {
                //ad.AdsCategoryParents = GetAdsParents(ad.CategoryId,"");
                var cat = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == ad.CategoryId);
                ad.AdsCategoryParents = cat == null ? "" : cat.Name;
                ad.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        ad.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/AdsPhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                        
                    }
                }

                var dbState = _stateRepository.GetQuery().FirstOrDefault(z => z.StateId == ad.StateId);
                ad.State = dbState == null ? "" : dbState.Name;
                var dbCity = _cityRepository.GetQuery().FirstOrDefault(x => x.CityId == ad.CityId);
                ad.City = dbCity == null ? "" : dbCity.Name;
                var plans = servicePlanRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == ServicePlanType.Ads).Select(x => x.PlanId).ToList();
                ad.PlanTypes = PlanRepository.GetQuery().Where(z=>plans.Contains(z.PlanId)).Select(x=>x.Type).ToList();
            }
            var obj = new
            {
                Ads = dbAds,
                AdsCount = AdsCount,
            };
            return obj;
        }
        public async Task<object> GetByUserId(long userId, int pageNumber, int count)
        {
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            List<Ads> dbAds = new List<Ads>();
            dbAds = adsRepository.GetQuery().Where(z => z.UserId == userId && z.Status == (int)AdsStatus.Published).
                OrderByDescending(x => x.FireDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var AdsCount = adsRepository.GetQuery().Where(z => z.UserId == userId && z.Status == (int)AdsStatus.Published).Count();
            foreach (var ad in dbAds)
            {
                ad.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        ad.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/AdsPhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                        
                    }
                }
                ad.UserInfo = userService.Get(userId);
                ad.AdsCategoryParents = GetAdsParents(ad.CategoryId,"");
                var dbState = _stateRepository.GetQuery().FirstOrDefault(z => z.StateId == ad.StateId);
                ad.State = dbState == null ? "" : dbState.Name;
                var dbCity = _cityRepository.GetQuery().FirstOrDefault(x => x.CityId == ad.CityId);
                ad.City = dbCity == null ? "" : dbCity.Name;

                var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == MYselfId && x.ObjectId == ad.AdsId && x.Type == WishListType.Ads)
                .FirstOrDefault();
                if (dbWishList!=null)
                {
                    ad.IsFavourite = true;
                }
                else
                {
                    ad.IsFavourite = false;
                }
            }

            
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateAds;
            var Followed = false;
            if (IsPrivate == false)
            {
                if (MYselfId>0)
                {
                    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                    if (dbFollowing != null)
                    {
                        Followed= true;
                    }
                }

                var obj = new
                {
                    Status = (int)PostVisibility.VisibleAndPublic,
                    FollowButton = Followed == false ? true : false,
                    Ads = dbAds,
                    AdsCount=AdsCount
                };
                return obj;
            }
            else
            {
                if (MYselfId > 0)
                {
                    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                    if (dbFollowing != null)
                    {

                        var obj = new
                        {
                            Status = (int)PostVisibility.Visible,
                            FollowButton = false,
                            Ads = dbAds,
                            AdsCount = AdsCount
                        };
                        return obj;
                    }
                    else
                    {
                        var obj2 = new
                        {
                            Status = (int)PostVisibility.PrivateAndNotFollowing,
                            FollowButton = true
                        };
                        return obj2;
                    }
                }

                var obj3 = new
                {
                    Status = (int)PostVisibility.PrivateAndNotLogged,
                    FollowButton = true,

                };
                return obj3;

            }
        }
        public async Task<object> GetNotConfirmed(int pageNumber, int count, string search)
        {
            List<Ads> dbAds = new List<Ads>();
            dbAds = adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.NotConfirmed && (z.Title.Contains(search) || z.Description.Contains(search))).
                OrderByDescending(x => x.FireDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var AdsCount = adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.NotConfirmed && (z.Title.Contains(search) || z.Description.Contains(search))).Count();
            foreach (var ad in dbAds)
            {
                //ad.AdsCategoryParents = GetAdsParents(ad.CategoryId,"");
                var cat = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == ad.CategoryId);
                ad.AdsCategoryParents = cat == null ? "" : cat.Name;
                ad.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        ad.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/AdsPhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }
                ad.UserInfo = userService.Get(ad.UserId);
                var dbState = _stateRepository.GetQuery().FirstOrDefault(z => z.StateId == ad.StateId);
                ad.State = dbState == null ? "" : dbState.Name;
                var dbCity = _cityRepository.GetQuery().FirstOrDefault(x => x.CityId == ad.CityId);
                ad.City = dbCity == null ? "" : dbCity.Name;

            }
            var obj = new
            {
                Ads = dbAds,
                AdsCount = AdsCount,
            };
            return obj;
        }
        public async Task<object> GetRejected(int pageNumber, int count, string search)
        {
            List<Ads> dbAds = new List<Ads>();
            dbAds = adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Rejected && (z.Title.Contains(search) || z.Description.Contains(search))).
                OrderByDescending(x => x.FireDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var AdsCount = adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Rejected && (z.Title.Contains(search) || z.Description.Contains(search))).Count();
            foreach (var ad in dbAds)
            {
                ad.AdsCategoryParents = GetAdsParents(ad.CategoryId,"");
                ad.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        ad.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/AdsPhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }
                ad.UserInfo = userService.Get(ad.UserId);
                ad.UserInfo = userService.Get(ad.UserId);
                var dbState = _stateRepository.GetQuery().FirstOrDefault(z => z.StateId == ad.StateId);
                ad.State = dbState == null ? "" : dbState.Name;
                var dbCity = _cityRepository.GetQuery().FirstOrDefault(x => x.CityId == ad.CityId);
                ad.City = dbCity == null ? "" : dbCity.Name;
            }
            var obj = new
            {
                Ads = dbAds,
                AdsCount = AdsCount,
            };
            return obj;
        }
        public string GetAdsParents(long? adsCategoryId,string CatName)
        {
            var dbAdsCat=adsCategoryRepository.GetQuery().First(z=>z.AdsCategoryId == adsCategoryId);
            CatName = CatName=="" ? dbAdsCat.Name :CatName+ " > " +dbAdsCat.Name;
            if(dbAdsCat.ParentId != null && dbAdsCat.ParentId != 0)
            {
                return GetAdsParents(dbAdsCat.ParentId, CatName);
            }
            return CatName;
        }
        public async Task<Ads> ChangeAdsStatus(Ads item)
        {
            var dbAds = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == item.AdsId);
            if(dbAds != null)
            {
                dbAds.UpdateDate = DateTime.Now;
                dbAds.Status = item.Status;
                await adsRepository.Update(dbAds);
                
            }
            return dbAds;

        }

        public List<long> GetAllSubCatsId(long categoryId,List<long> catIds)
        {
            var subCats=adsCategoryRepository.GetQuery().Where(z=>z.ParentId == categoryId).ToList();
            subCats.ForEach(z => catIds.Add(z.AdsCategoryId));
            if(subCats.Count() > 0 )
            {
                foreach (var sub in subCats)
                {
                    GetAllSubCatsId(sub.AdsCategoryId, catIds);
                }
            }
            return catIds;
        }
        public async Task<object> GetAdsByCategory(long categoryId, long firstSearchadsId, int count)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            List<Ads> dbAds =new List<Ads>();
            int AdsCount = 0;
            if(categoryId == 0)
            {
                if(firstSearchadsId !=0)
                {
                    dbAds=adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published && z.AdsId < firstSearchadsId).
                    OrderByDescending(x => x.FireDate).Take(count).ToList();
                }
                else
                {
                    dbAds=adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published).
                    OrderByDescending(x => x.FireDate).Take(count).ToList();
                }
                AdsCount=adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published).Count();
            }
            else
            {
                var catIds = GetAllSubCatsId(categoryId, new List<long>() { categoryId });
                if(firstSearchadsId !=0)
                {
                    dbAds = adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published && catIds.Contains(z.CategoryId) && z.AdsId < firstSearchadsId).
                    OrderByDescending(x => x.FireDate).Take(count).ToList();
                }
                else
                {
                    dbAds=adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published && catIds.Contains(z.CategoryId)).
                    OrderByDescending(x => x.FireDate).Take(count).ToList();
                }
                AdsCount = adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published && catIds.Contains(z.CategoryId)).Count();
            }
            
            foreach (var ad in dbAds)
            {
                ad.AdsCategoryParents = GetAdsParents(ad.CategoryId, "");
                ad.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        ad.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/AdsPhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }
                ad.UserInfo = userService.Get(ad.UserId);
                var dbState = _stateRepository.GetQuery().FirstOrDefault(z => z.StateId == ad.StateId);
                ad.State = dbState == null ? "" : dbState.Name;
                var dbCity = _cityRepository.GetQuery().FirstOrDefault(x => x.CityId == ad.CityId);
                ad.City = dbCity == null ? "" : dbCity.Name;
                var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == ad.AdsId && x.Type == WishListType.Ads)
                .FirstOrDefault();
                if (dbWishList!=null)
                {
                    ad.IsFavourite = true;
                }
                else
                {
                    ad.IsFavourite = false;
                }
            }
            var obj = new
            {
                Ads = dbAds,
                AdsCount = AdsCount,
            };
            return obj;
        }

        public async Task<object> GetAdsByFilter(long? priceFrom,long? priceTo,string title, string tag,long? city,long? state,long firstSearchadsId, int count,long? categoryId)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            tag = tag ==null ? string.Empty : tag;
            title = title ==null ? string.Empty : title;
            state = state ==null ? 0 : state;
            city = city ==null ? 0 : city;
            IOrderedQueryable<Ads> dbAds;
            int AdsCount = 0;
            
            if (firstSearchadsId !=0)
            {
            dbAds=adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published && z.AdsId < firstSearchadsId).
            OrderByDescending(x => x.FireDate);
            }
            else
            {
            dbAds=adsRepository.GetQuery().Where(z => z.Status == (int)AdsStatus.Published).
            OrderByDescending(x => x.FireDate);
            }
            
            var result = dbAds.Where(z => z.Tag.Contains(tag) && z.Title.Contains(title));
            if (state !=0)
            {
                result=result.Where(z=>z.StateId==state);
            }
            if(city !=0)
            {
                result=result.Where(x=>x.CityId==city);
            }
            if(categoryId !=0 && categoryId != null)
            {
                result = result.Where(z=>z.CategoryId==categoryId);
            }
            if (priceTo != 0 && priceTo !=null)
            {
                result =result.Where(c => c.Price <= priceTo);
            }
            if(priceFrom != 0 && priceFrom != null)
            {
                result= result.Where(c => c.Price >= priceFrom);
            }
            AdsCount=result.Count();
            if(count == 0)
            {
                count=AdsCount;
            }
            var finalResult=result.Take(count).ToList();
            foreach (var ad in finalResult)
            {
                //ad.AdsCategoryParents = GetAdsParents(ad.CategoryId, "");
                var cat = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == ad.CategoryId);
                ad.AdsCategoryParents = cat == null ? "" : cat.Name;
                ad.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == MediaTypes.AdsPhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        ad.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/AdsPhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }
                var plans = servicePlanRepository.GetQuery().Where(z => z.ObjectId == ad.AdsId && z.Type == ServicePlanType.Ads).Select(x => x.PlanId).ToList();
                ad.PlanTypes = PlanRepository.GetQuery().Where(z => plans.Contains(z.PlanId)).Select(x => x.Type).ToList();
                ad.UserInfo = userService.Get(ad.UserId);
                var dbState = _stateRepository.GetQuery().FirstOrDefault(z => z.StateId == ad.StateId);
                ad.State = dbState == null ? "" : dbState.Name;
                var dbCity = _cityRepository.GetQuery().FirstOrDefault(x => x.CityId == ad.CityId);
                ad.City = dbCity == null ? "" : dbCity.Name;

                var dbWishList = wishListRepository.GetQuery().Where(x => x.UserId == userId && x.ObjectId == ad.AdsId && x.Type == WishListType.Ads)
                .FirstOrDefault();
                if (dbWishList!=null)
                {
                    ad.IsFavourite = true;
                }
                else
                {
                    ad.IsFavourite = false;
                }
            }
            var obj = new
            {
                Ads = finalResult,
                AdsCount = AdsCount,
            };
            return obj;
        }
    }
}
