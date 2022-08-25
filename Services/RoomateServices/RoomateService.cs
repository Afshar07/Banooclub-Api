using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.SocialMediaServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BanooClub.Services.RoomateServices
{
    public class RoomateService : IRoomateService
    {
        private readonly IBanooClubEFRepository<Roomate> roomateRepository;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        private readonly IBanooClubEFRepository<UserSetting> userSettingRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<RoomateDoc> roomateDocRepository;
        private readonly ISocialMediaService _mediaService;
        private readonly IHttpContextAccessor _accessor;

        public RoomateService(IBanooClubEFRepository<Roomate> roomateRepository
            , IBanooClubEFRepository<SocialMedia> mediaRepository
            , IBanooClubEFRepository<UserSetting> userSettingRepository
            , IBanooClubEFRepository<Following> followingRepository
            , IBanooClubEFRepository<RoomateDoc> roomateDocRepository
            , ISocialMediaService mediaService
            , IHttpContextAccessor accessor)
        {
            this.roomateRepository = roomateRepository;
            _mediaRepository = mediaRepository;
            _mediaService = mediaService;
            _accessor = accessor;
            this.userSettingRepository = userSettingRepository;
            this.followingRepository = followingRepository;
            this.roomateDocRepository = roomateDocRepository;
        }
        public async Task Create(Roomate inputDto)
        {
            var dbLastRoomate = roomateRepository.GetQuery().FirstOrDefault(z => z.UserId == inputDto.UserId);
            if (dbLastRoomate == null)
            {
                var dbRoomate = roomateRepository.Insert(inputDto);
                foreach (var item in inputDto.Photos)
                {
                    if (!string.IsNullOrEmpty(item.Base64))
                    {
                        var outPut = _mediaService.SaveImage(item.Base64, EntityUrls.RoomatePhoto);
                        SocialMedia dbMedia = new SocialMedia()
                        {
                            IsDeleted = false,
                            ObjectId = dbRoomate.RoomateId,
                            PictureUrl = outPut.ImageName,
                            Type = MediaTypes.RoomatePhoto,
                            Priority = item.Priority,
                            MediaId = 0
                        };
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
            }
            else
            {
                await Update(inputDto);
            }
            await roomateRepository.Save();
        }

        public async Task<Roomate> Update(Roomate item)
        {
            var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == item.RoomateId && z.Type == MediaTypes.RoomatePhoto).ToList();
            foreach (var photo in dbPhotos)
            {
                await _mediaRepository.Delete(photo);
            }
            foreach (var element in item.Photos)
            {
                if (!string.IsNullOrEmpty(element.Base64))
                {
                    var outPut = _mediaService.SaveImage(element.Base64, EntityUrls.RoomatePhoto);
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.RoomateId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.RoomatePhoto,
                        Priority = element.Priority,
                        MediaId = 0
                    };
                    await _mediaRepository.InsertAsync(dbMedia);
                }
            }
            await roomateRepository.Update(item);
            return item;
        }

        public async Task<List<Roomate>> GetAll(long? mortgageFrom , long? mortgageTo ,long? dailyRentFrom,long? dailyRentTo)
        {
            try
            {
                var dbRoomates = roomateRepository.GetQuery();

                if (mortgageFrom != 0 && mortgageFrom != null)
                {
                    dbRoomates = dbRoomates.Where(z => z.Mortgage >= mortgageFrom);
                }
                if (mortgageTo != 0 && mortgageTo != null)
                {
                    dbRoomates = dbRoomates.Where(z => z.Mortgage <= mortgageTo);
                }

                if (dailyRentFrom != 0 && dailyRentFrom != null)
                {
                    dbRoomates = dbRoomates.Where(z => z.DailyRent >= dailyRentFrom);
                }
                if (dailyRentTo != 0 && dailyRentTo != null)
                {
                    dbRoomates = dbRoomates.Where(z => z.DailyRent <= dailyRentTo);
                }

                var finalRoomates = dbRoomates.ToList();
                var roommateUserIds = finalRoomates.Select(z => z.UserId).ToList();
                var dbUserSettings = userSettingRepository.GetQuery().Where(z=>roommateUserIds.Contains(z.UserId)).ToList();
                var ActiveRoomatesIds = new List<long>();
                foreach(var item in dbUserSettings)
                {
                    if(item.ActiveRoomate == true)
                    {
                        ActiveRoomatesIds.Add(item.UserId);
                    }
                }
                finalRoomates = finalRoomates.Where(z => ActiveRoomatesIds.Contains(z.UserId)).ToList();

                foreach (var roomate in finalRoomates)
                {
                    roomate.IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == roomate.UserId).IsPrivateRoomate;
                    roomate.Photos = new List<FileData>();
                    var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == roomate.RoomateId && z.Type == MediaTypes.RoomatePhoto).ToList();
                    foreach (var photo in dbPhotos)
                    {
                        if (photo != null)
                        {
                            roomate.Photos.Add(new FileData()
                            {
                                Base64 = "media/gallery/RoomatePhotos/" + photo.PictureUrl,
                                Priority = (int)photo.Priority,
                                UploadType = 1
                            });
                        }
                    }
                }
                return finalRoomates;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task Delete(long id)
        {
            var dbRoomate = roomateRepository.GetQuery().FirstOrDefault(z => z.RoomateId == id);
            await roomateRepository.Delete(dbRoomate);
        }

        public int CheckAvailablityForActivation()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var roomatDocStatus = roomateDocRepository.GetQuery().FirstOrDefault(z => z.UserId ==userId).Status;
            return roomatDocStatus;
        }

        public async Task<object> ChangeRoomateActivation(bool state)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var userSetting = userSettingRepository.GetQuery().FirstOrDefault(z=>z.UserId == userId);
            userSetting.ActiveRoomate = state;
            await userSettingRepository.Update(userSetting);
            return true;
        }

        public async Task<object> Get(long id)
        {
            var roomate = roomateRepository.GetQuery().FirstOrDefault(z => z.RoomateId == id);
            if (roomate != null)
            {
                roomate.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == roomate.RoomateId && z.Type == MediaTypes.RoomatePhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        roomate.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/RoomatePhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }

                var obj = new
                {
                    Status = 1,
                    Roomate = roomate
                };
                return obj;
            }
            else
            {
                var obj = new
                {
                    Status = 2,
                    Roomate = "Not Set Yet"
                };
                return obj;
            }
        }
        public async Task<object> GetByUserId(long userId)
        {
            var roomate = roomateRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            if (roomate != null)
            {
                roomate.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == roomate.RoomateId && z.Type == MediaTypes.RoomatePhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        roomate.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/RoomatePhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }
            }
            
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateRoomate;
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
                    Roomate = roomate
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
                            Roomate = roomate
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
        public async Task<object> GetMyRoomate()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var roomate = roomateRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            if (roomate != null)
            {
                roomate.Photos = new List<FileData>();
                var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == roomate.RoomateId && z.Type == MediaTypes.RoomatePhoto).ToList();
                foreach (var photo in dbPhotos)
                {
                    if (photo != null)
                    {
                        roomate.Photos.Add(new FileData()
                        {
                            Base64 = "media/gallery/RoomatePhotos/" + photo.PictureUrl,
                            Priority = (int)photo.Priority
                        });
                    }
                }

                var obj = new
                {
                    Status = 1,
                    Roomate = roomate
                };
                return obj;
            }
            else
            {
                var obj = new
                {
                    Status = 2,
                    Roomate = "Not Set Yet"
                };
                return obj;
            }
        }

        public async Task<List<Roomate>> GetAllForAdmin(long? mortgageFrom, long? mortgageTo, long? dailyRentFrom, long? dailyRentTo)
        {
            try
            {
                var dbRoomates = roomateRepository.GetQuery();

                if (mortgageFrom != 0 && mortgageFrom != null)
                {
                    dbRoomates = dbRoomates.Where(z => z.Mortgage >= mortgageFrom);
                }
                if (mortgageTo != 0 && mortgageTo != null)
                {
                    dbRoomates = dbRoomates.Where(z => z.Mortgage <= mortgageTo);
                }

                if (dailyRentFrom != 0 && dailyRentFrom != null)
                {
                    dbRoomates = dbRoomates.Where(z => z.DailyRent >= dailyRentFrom);
                }
                if (dailyRentTo != 0 && dailyRentTo != null)
                {
                    dbRoomates = dbRoomates.Where(z => z.DailyRent <= dailyRentTo);
                }

                var finalRoomates = dbRoomates.ToList();

                foreach (var roomate in finalRoomates)
                {
                    roomate.IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == roomate.UserId).IsPrivateRoomate;
                    roomate.Photos = new List<FileData>();
                    var dbPhotos = _mediaRepository.GetQuery().Where(z => z.ObjectId == roomate.RoomateId && z.Type == MediaTypes.RoomatePhoto).ToList();
                    foreach (var photo in dbPhotos)
                    {
                        if (photo != null)
                        {
                            roomate.Photos.Add(new FileData()
                            {
                                Base64 = "media/gallery/RoomatePhotos/" + photo.PictureUrl,
                                Priority = (int)photo.Priority,
                                UploadType = 1
                            });
                        }
                    }
                }
                return finalRoomates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
