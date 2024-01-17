using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.SocialMediaServices;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsCategoryServices
{
    public class AdsCategoryService : IAdsCategoryService
    {
        private readonly IBanooClubEFRepository<AdsCategory> adsCategoryRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly ISocialMediaService _mediaService;

        public AdsCategoryService(IBanooClubEFRepository<AdsCategory> adsCategoryRepository, ISocialMediaService mediaService, IBanooClubEFRepository<SocialMedia> mediaRepository)
        {
            this.adsCategoryRepository = adsCategoryRepository;
            this.mediaRepository = mediaRepository;
            _mediaService = mediaService;
        }
        public async Task Create(AdsCategory inputDto)
        {
            inputDto.IsDeleted = false;
            var creation = adsCategoryRepository.Insert(inputDto);
            if (!string.IsNullOrEmpty(inputDto.FileData))
            {
                var outPut = _mediaService.SaveImage(inputDto.FileData, EntityUrls.AdsCategory);
                SocialMedia dbMedia = new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = creation.AdsCategoryId,
                    PictureUrl = outPut.ImageName,
                    Type = MediaTypes.AdsCategory,
                    MediaId = 0,
                    Priority = 1,
                    UpdateDate = DateTime.Now
                };
                await mediaRepository.InsertAsync(dbMedia);
            }
        }

        public async Task<AdsCategory> Update(AdsCategory item)
        {
            var dbLastCatMedia = mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == item.AdsCategoryId && z.Type == MediaTypes.AdsCategory);

            if (item.FileData == "Delete")
            {
                if (dbLastCatMedia != null)
                {
                    dbLastCatMedia.PictureUrl = "";
                    await mediaRepository.Update(dbLastCatMedia);
                    await mediaRepository.Delete(dbLastCatMedia);
                }

            }
            else if (!string.IsNullOrEmpty(item.FileData))
            {
                var outPut = _mediaService.SaveImage(item.FileData, EntityUrls.AdsCategory);
                if (outPut.IsSuccess)
                {
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.AdsCategoryId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.AdsCategory,
                        MediaId = 0,
                        UpdateDate =DateTime.Now,
                        Priority = 1
                    };

                    if (dbLastCatMedia != null)
                    {
                        dbLastCatMedia.PictureUrl = outPut.ImageName;
                        await mediaRepository.Update(dbLastCatMedia);
                        item.FileData = outPut.ImageName;
                    }
                    else
                    {
                        await mediaRepository.InsertAsync(dbMedia);
                        item.FileData = outPut.ImageName;
                    }
                }
            }
            await adsCategoryRepository.Update(item);
            return item;
        }



        public object GetAll(int pageNumber,int count, string search)
        {
            var AdsCount = adsCategoryRepository.GetQuery().Where(z=>z.Name.Contains(search)).Count();
            var adsCategories = adsCategoryRepository.GetQuery().Where(z => z.Name.Contains(search)).OrderByDescending(x => x.AdsCategoryId).ToList();
            foreach(var adsCategory in adsCategories)
            {
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.AdsCategory && z.ObjectId == adsCategory.AdsCategoryId);
                if (dbMedia != null)
                {
                    adsCategory.FileData = dbMedia.PictureUrl;
                }
            }
            if(pageNumber != 0 && count != 0)
            {
                adsCategories = adsCategories.Skip((pageNumber - 1)* count).Take(count).ToList();
            }
            var obj = new
            {
                AdsCategories = adsCategories,
                AdsCategoryCount = AdsCount
            };
            return obj;
            //if (pageNumber ==0 && count ==0)
            //{
            //    string cmd= "select Main.AdsCategoryId as MainAdsCategoryId,Main.Name as MainName, " +
            //        " Parent.AdsCategoryId as ParentId,Parent.Name as ParentName from Service.AdsCategories as Main " +
            //        $" left join Service.AdsCategories as Parent on Main.ParentId = Parent.AdsCategoryId where Main.IsDeleted = 0 and Main.Name like N'%{search}%' order by Main.AdsCategoryId DESC";
            //    var AdsCats = adsCategoryRepository.DapperSqlQuery(cmd).Result;
            //    var obj = new
            //    {
            //        AdsCategories = AdsCats,
            //        AdsCategoryCount = AdsCount
            //    };
            //    return obj;
            //}
            //else
            //{
            //    string cmd = "select Main.AdsCategoryId as MainAdsCategoryId,Main.Name as MainName, " +
            //        " Parent.AdsCategoryId as ParentId,Parent.Name as ParentName from Service.AdsCategories as Main " +
            //        $" left join Service.AdsCategories as Parent on Main.ParentId = Parent.AdsCategoryId where Main.IsDeleted = 0 and Main.Name like N'%{search}%' " +
            //        $" order by Main.AdsCategoryId DESC OFFSET {(pageNumber-1)*count} Rows FETCH NEXT {count} ROWS ONLY";
            //    var AdsCats = adsCategoryRepository.DapperSqlQuery(cmd).Result;
            //    var obj = new
            //    {
            //        AdsCategories = AdsCats,
            //        AdsCategoryCount = AdsCount
            //    };
            //    return obj;
            //}
        }

        public async Task<bool> Delete(long id)
        {
            var dbAds = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == id);
            var child=adsCategoryRepository.GetQuery().FirstOrDefault(z=>z.ParentId==dbAds.AdsCategoryId);
            if(child !=null)
            {
                return false;
            }
            await adsCategoryRepository.Delete(dbAds);
            return true;
        }

        public async Task<AdsCategory> Get(long id)
        {
            var dbAds = adsCategoryRepository.GetQuery().FirstOrDefault(z => z.AdsCategoryId == id);
            var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.Type == MediaTypes.AdsCategory && z.ObjectId == id);
            if (dbMedia != null)
            {
                dbAds.FileData = dbMedia.PictureUrl;
            }
            return dbAds;
        }

    }
}
