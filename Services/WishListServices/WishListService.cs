using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using System;
using BanooClub.Models.Enums;

namespace BanooClub.Services.WishListServices
{
    public class WishListService : IWishListService
    {
        private readonly IBanooClubEFRepository<WishList> wishRepository;
        private readonly IBanooClubEFRepository<ServicePack> serviceRepository;
        private readonly IBanooClubEFRepository<Ads> adsRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly IHttpContextAccessor _accessor;

        public WishListService(IBanooClubEFRepository<WishList> wishRepository, IBanooClubEFRepository<SocialMedia> mediaRepository, IBanooClubEFRepository<Ads> adsRepository, IBanooClubEFRepository<ServicePack> serviceRepository, IHttpContextAccessor accessor)
        {
            this.wishRepository = wishRepository;
            this.serviceRepository = serviceRepository;
            this.adsRepository = adsRepository;
            this.mediaRepository = mediaRepository;
            _accessor = accessor;
        }
        public async Task Create(WishList inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            inputDto.CreateDate = DateTime.Now;
            wishRepository.Insert(inputDto);
        }

        public async Task<WishList> Update(WishList item)
        {
            await wishRepository.Update(item);
            return item;
        }

        public async Task<List<WishList>> GetAll()
        {
            List<WishList> wishes = new List<WishList>();
            wishes = wishRepository.GetQuery().ToList();
            return wishes;
        }

        public async Task Delete(long objectId, WishListType type)
        {
            var wish = wishRepository.GetQuery().FirstOrDefault(z => z.ObjectId == objectId && z.Type == type);
            await wishRepository.Delete(wish);
        }

        public async Task<WishList> Get(long id)
        {
            var wish = wishRepository.GetQuery().FirstOrDefault(z => z.WishListId == id);
            return wish;
        }
        public List<WishList> GetByWishList()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbWishLists = wishRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            foreach (var wish in dbWishLists)
            {
                if(wish.Type == WishListType.Service)
                {
                    wish.ServiceInfo = serviceRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == wish.ObjectId);
                    var dbMedias = mediaRepository.GetQuery().Where(Z => Z.ObjectId == wish.ObjectId && Z.Type == MediaTypes.Service).ToList();
                    wish.ServiceInfo.Medias = new List<FileData>();
                    foreach (var dbMedia in dbMedias)
                    {
                        wish.ServiceInfo.Medias.Add(new FileData() { Base64 = dbMedia.PictureUrl, Priority = dbMedia.Priority, UploadType = 1 });
                    }
                }
                if(wish.Type == WishListType.Ads)
                {
                    wish.AdsInfo = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == wish.ObjectId);
                    var dbMedias = mediaRepository.GetQuery().Where(Z => Z.ObjectId == wish.ObjectId && Z.Type == MediaTypes.AdsPhoto).ToList();
                    wish.AdsInfo.Photos = new List<FileData>();
                    foreach (var dbMedia in dbMedias)
                    {
                        wish.AdsInfo.Photos.Add(new FileData() { Base64 = dbMedia.PictureUrl, Priority = dbMedia.Priority, UploadType = 1 });
                    }
                }
                
            }
            return dbWishLists;
        }
        public async Task Toggle(long objectId,WishListType type)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var wish = wishRepository.GetQuery().Where(z => z.UserId == userId && z.ObjectId==objectId && z.Type == type).FirstOrDefault();
            if (wish != null)
            {
                wishRepository.Erase(wish);
            }
            else
            {
                WishList inputDto = new WishList();
                inputDto.UserId = userId;
                inputDto.ObjectId = objectId;
                inputDto.Type = type;
                inputDto.CreateDate = DateTime.Now;
                wishRepository.Insert(inputDto);
            }
        }
        public object GetByUserId(long userId)
        {
            var dbAdses = wishRepository.GetQuery().Where(z=>z.Type  == WishListType.Ads && z.UserId == userId).ToList();
            var dbServices = wishRepository.GetQuery().Where(z=>z.Type == WishListType.Service && z.UserId == userId).ToList();
            foreach(var ads in dbAdses)
            {
                ads.AdsInfo = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == ads.ObjectId);
                var dbMediaAds = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == ads.ObjectId && z.Type == MediaTypes.AdsPhoto && z.Priority == 1);
                if(dbMediaAds != null)
                {
                    ads.AdsInfo.Photos = new List<FileData>();
                    ads.AdsInfo.Photos.Add(new FileData() { Base64 = dbMediaAds.PictureUrl , Priority = 1 });
                }
            }
            foreach(var service in dbServices)
            {
                service.ServiceInfo = serviceRepository.GetQuery().FirstOrDefault(z=>z.ServicePackId == service.ObjectId);
                var dbMediaService = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == service.ObjectId && z.Type == MediaTypes.Service && z.Priority == 1);
                if (dbMediaService != null)
                {
                    service.ServiceInfo.Medias = new List<FileData>();
                    service.ServiceInfo.Medias.Add(new FileData() { Base64 = dbMediaService.PictureUrl, Priority = 1 });
                }
            }
            var obj = new
            {
                ServiceWishes = dbServices,
                AdsWishes = dbAdses
            };
            return obj;
        }
    }
}
