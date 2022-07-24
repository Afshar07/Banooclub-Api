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
        public object GetByUserId(long userId)
        {
            var dbAdses = wishRepository.GetQuery().Where(z=>z.Type  == WishListType.Ads).ToList();
            var dbServices = wishRepository.GetQuery().Where(z=>z.Type == WishListType.Service).ToList();
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
