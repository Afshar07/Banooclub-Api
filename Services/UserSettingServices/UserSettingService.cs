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
using System.Threading.Tasks;
using System.Data.Common;

namespace BanooClub.Services.UserSettingServices
{
    public class UserSettingService : IUserSettingService
    {
        private readonly IBanooClubEFRepository<UserSetting> userSettingRepository;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        private readonly IBanooClubEFRepository<RoomateDoc> _roomateDocRepository;
        private readonly IBanooClubEFRepository<Activity> _activityRepository;
        private readonly IBanooClubEFRepository<User> _userRepository;
        private readonly ISocialMediaService _mediaService;
        private readonly IHttpContextAccessor _accessor;
        public UserSettingService(IBanooClubEFRepository<UserSetting> userSettingRepository, IBanooClubEFRepository<SocialMedia> mediaRepository
            , IBanooClubEFRepository<RoomateDoc> roomateDocRepository
            , IBanooClubEFRepository<User> userRepository
            , ISocialMediaService mediaService, IHttpContextAccessor accessor,IBanooClubEFRepository<Activity> activityRepository)
        {
            this.userSettingRepository = userSettingRepository;
            _mediaRepository = mediaRepository;
            _mediaService = mediaService;
            _roomateDocRepository = roomateDocRepository;
            _accessor = accessor;
            _activityRepository = activityRepository;
            _userRepository = userRepository;
        }
        public async Task Create(UserSetting inputDto)
        {
            var dbLast = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == inputDto.UserId);
            if (dbLast == null)
            {
                userSettingRepository.Insert(inputDto);
            }
        }

        public async Task<UserSetting> Update(UserSetting item)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var dbVideoIdentity = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.VideoIdentity);

            if (item.VideoIdentity == "Delete")
            {
                dbVideoIdentity.PictureUrl = "";
                await _mediaRepository.Update(dbVideoIdentity);
                await _mediaRepository.Delete(dbVideoIdentity);
                item.VideoIdentityStatus=VideoIdentity.Empty;
            }
            else if (!string.IsNullOrEmpty(item.VideoIdentity))
            {
                var outPut = _mediaService.SaveImage(item.VideoIdentity, EntityUrls.VideoIdentity);
                if (outPut.IsSuccess)
                {
                    item.VideoIdentityStatus=VideoIdentity.Uploaded;
                    item.VideoIdentityUpdateDate=DateTime.Now;
                    item.VideoIdentity = outPut.ImageName;
                    
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.UserId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.VideoIdentity,
                        MediaId = 0
                    };

                    if (dbVideoIdentity != null)
                    {
                        dbVideoIdentity.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbVideoIdentity);
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
            }

            var dbNewspaper = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.Newspaper);

            if (item.NewspaperDoc == "Delete")
            {
                dbNewspaper.PictureUrl = "";
                await _mediaRepository.Update(dbNewspaper);
                await _mediaRepository.Delete(dbNewspaper);
                item.NewspaperStatus=DocStatus.Empty;
            }
            else if (!string.IsNullOrEmpty(item.NewspaperDoc))
            {
                item.NewspaperStatus=DocStatus.NotConfirmed;
                var outPut = _mediaService.SaveImage(item.NewspaperDoc, EntityUrls.Newspaper);
                if (outPut.IsSuccess)
                {
                    item.NewspaperStatus=DocStatus.NotConfirmed;
                    item.NewspaperDoc = outPut.ImageName;

                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.UserId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Newspaper,
                        MediaId = 0,
                        UpdateDate = DateTime.Now
                    };

                    if (dbNewspaper != null)
                    {
                        dbNewspaper.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbNewspaper);
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
            }

            var dbLawyerCertificate = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.LawyerCertificate);

            if (item.LawyerCertificateDoc == "Delete")
            {
                dbLawyerCertificate.PictureUrl = "";
                await _mediaRepository.Update(dbLawyerCertificate);
                await _mediaRepository.Delete(dbLawyerCertificate);
                item.LawyerCertificateStatus=DocStatus.Empty;
            }
            else if (!string.IsNullOrEmpty(item.LawyerCertificateDoc))
            {
                item.LawyerCertificateStatus=DocStatus.NotConfirmed;
                var outPut = _mediaService.SaveImage(item.LawyerCertificateDoc, EntityUrls.LawyerCertificate);
                if (outPut.IsSuccess)
                {
                    item.LawyerCertificateStatus=DocStatus.NotConfirmed;
                    item.LawyerCertificateDoc = outPut.ImageName;

                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.UserId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.LawyerCertificate,
                        MediaId = 0,
                        UpdateDate = DateTime.Now
                    };

                    if (dbLawyerCertificate != null)
                    {
                        dbLawyerCertificate.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLawyerCertificate);
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
            }

            var dbLastKartMelli = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.KartMelliDoc);

            if (item.KartMelliDoc == "Delete")
            {
                dbLastKartMelli.PictureUrl = "";
                await _mediaRepository.Update(dbLastKartMelli);
                await _mediaRepository.Delete(dbLastKartMelli);
            }
            else if (!string.IsNullOrEmpty(item.KartMelliDoc))
            {
                var outPut = _mediaService.SaveImage(item.KartMelliDoc, EntityUrls.KartMelliDoc);
                if (outPut.IsSuccess)
                {
                    item.KartMelliDoc = outPut.ImageName;
                    var dbLastRoomateDoc = _roomateDocRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
                    if (dbLastRoomateDoc == null)
                    {
                        _roomateDocRepository.Insert(new RoomateDoc()
                        {
                            IsDeleted = false,
                            RoomateDocId = 0,
                            StateDesc = null,
                            UpdatedDate = DateTime.Now,
                            Status = (int)DocStatus.NotConfirmed,
                            KartMelliImage = outPut.ImageName,
                            PassportImage = null,
                            UserId = userId
                        });
                    }
                    else
                    {
                        dbLastRoomateDoc.Status = (int)DocStatus.NotConfirmed;
                        dbLastRoomateDoc.KartMelliImage = outPut.ImageName;
                        await _roomateDocRepository.Update(dbLastRoomateDoc);
                    }
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.UserId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.KartMelliDoc,
                        MediaId = 0
                    };

                    if (dbLastKartMelli != null)
                    {
                        dbLastKartMelli.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLastKartMelli);
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
            }

            var dbLastPassport = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.PassportDoc);

            if (item.PassportDoc == "Delete")
            {
                dbLastPassport.PictureUrl = "";
                await _mediaRepository.Update(dbLastPassport);
                await _mediaRepository.Delete(dbLastPassport);
            }
            else if (!string.IsNullOrEmpty(item.PassportDoc))
            {
                var outPut = _mediaService.SaveImage(item.PassportDoc, EntityUrls.PassportDoc);
                if (outPut.IsSuccess)
                {
                    item.PassportDoc = outPut.ImageName;
                    var dbLastRoomateDoc = _roomateDocRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
                    if (dbLastRoomateDoc == null)
                    {
                        _roomateDocRepository.Insert(new RoomateDoc()
                        {
                            IsDeleted = false,
                            RoomateDocId = 0,
                            StateDesc = null,
                            UpdatedDate = DateTime.Now,
                            Status = (int)DocStatus.NotConfirmed,
                            KartMelliImage = null,
                            PassportImage = outPut.ImageName,
                            UserId = userId
                        });
                    }
                    else
                    {
                        dbLastRoomateDoc.Status = (int)DocStatus.NotConfirmed;
                        dbLastRoomateDoc.PassportImage = outPut.ImageName;
                        await _roomateDocRepository.Update(dbLastRoomateDoc);
                    }

                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.UserId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.PassportDoc,
                        MediaId = 0,
                        UpdateDate = DateTime.Now
                    };

                    if (dbLastPassport != null)
                    {
                        dbLastPassport.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLastPassport);
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
            }

            var dbUserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId==userId);

            dbUserSetting.Flag = string.IsNullOrEmpty(item.Flag) ? dbUserSetting.Flag : item.Flag;
            dbUserSetting.Bio = string.IsNullOrEmpty(item.Bio) ? dbUserSetting.Bio : item.Bio;
            dbUserSetting.UserTag =string.IsNullOrEmpty(item.UserTag) ? dbUserSetting.UserTag : item.UserTag;
            dbUserSetting.BirthDate = item.BirthDate ==null ? dbUserSetting.BirthDate : item.BirthDate;
            dbUserSetting.Gender = item.Gender == null ?  dbUserSetting.Gender:item.Gender;
            dbUserSetting.IsPrivateActivity = item.IsPrivateActivity ==null ?dbUserSetting.IsPrivateActivity: item.IsPrivateActivity;
            dbUserSetting.IsPrivateAds = item.IsPrivateAds ==null ?dbUserSetting.IsPrivateAds : item.IsPrivateAds;
            dbUserSetting.IsPrivateSocial = item.IsPrivateSocial ==null ?dbUserSetting.IsPrivateSocial : item.IsPrivateSocial;
            dbUserSetting.IsPrivateRoomate = item.IsPrivateRoomate ==null ?dbUserSetting.IsPrivateRoomate : item.IsPrivateRoomate;
            dbUserSetting.LawyerCertificateStatus = string.IsNullOrEmpty(item.LawyerCertificateDoc) ? dbUserSetting.LawyerCertificateStatus : item.LawyerCertificateStatus;
            dbUserSetting.NewspaperStatus =string.IsNullOrEmpty(item.NewspaperDoc) ? dbUserSetting.NewspaperStatus : item.NewspaperStatus;
            dbUserSetting.VideoIdentityStatus = string.IsNullOrEmpty(item.VideoIdentity) ? dbUserSetting.VideoIdentityStatus : item.VideoIdentityStatus;
            dbUserSetting.IsPrivatePost = item.IsPrivatePost == null ? dbUserSetting.IsPrivatePost : item.IsPrivatePost;
            dbUserSetting.IsPrivateGalleryFriend = item.IsPrivateGalleryFriend == null ? dbUserSetting.IsPrivateGalleryFriend : item.IsPrivateGalleryFriend;

            await userSettingRepository.Update(dbUserSetting);

            var dbActivity = new Activity()
            {
                CreateDate = DateTime.Now,
                ActivityId=0,
                IsDeleted = false,
                ObjectId=item.UserSettingId,
                Type= ActivityTypes.UpdateProfile,
                UserId= userId
            };
            await _activityRepository.InsertAsync(dbActivity);
            return item;
        }

        public async Task<List<UserSetting>> GetAll()
        {
            List<UserSetting> reviews = new List<UserSetting>();
            reviews = userSettingRepository.GetQuery().ToList();
            return reviews;
        }

        public async Task Delete(long id)
        {
            var review = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserSettingId == id);
            await userSettingRepository.Delete(review);
        }

        public async Task<UserSetting> Get(long id)
        {
            var dbUserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserSettingId == id);
            var dbKartMelli = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == dbUserSetting.UserId && z.Type == MediaTypes.KartMelliDoc);
            if (dbKartMelli != null)
            {
                dbUserSetting.KartMelliDoc = "media/gallery/KartMelliDocs/" + dbKartMelli.PictureUrl;
            }

            var dbPassportDoc = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == dbUserSetting.UserId && z.Type == MediaTypes.PassportDoc);
            if (dbPassportDoc != null)
            {
                dbUserSetting.PassportDoc = "media/gallery/PassportDocs/" + dbPassportDoc.PictureUrl;
            }

            var dbVideoIdentity = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == dbUserSetting.UserId && z.Type == MediaTypes.VideoIdentity);
            if (dbVideoIdentity != null)
            {
                dbUserSetting.VideoIdentity = "media/gallery/VideoIdentity/" + dbVideoIdentity.PictureUrl;
            }

            var dbLastNewspaperMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.Newspaper);
            dbUserSetting.NewspaperDoc= dbLastNewspaperMedia == null ? null : "Media/gallery/Newspaper/" +dbLastNewspaperMedia.PictureUrl;

            var dbLastCertificateMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.LawyerCertificate);
            dbUserSetting.LawyerCertificateDoc= dbLastCertificateMedia == null ? null : "Media/gallery/LawyerCertificate/" +dbLastCertificateMedia.PictureUrl;

            return dbUserSetting;
        }
        public List<UserSetting> GetByUserId()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                var dbKartMelli = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.KartMelliDoc);
                if (dbKartMelli != null)
                {
                    dbUserSetting.KartMelliDoc = "media/gallery/KartMelliDocs/" + dbKartMelli.PictureUrl;
                }

                var dbPassportDoc = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.PassportDoc);
                if (dbPassportDoc != null)
                {
                    dbUserSetting.PassportDoc = "media/gallery/PassportDocs/" + dbPassportDoc.PictureUrl;
                }

                var dbVideoIdentity = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.VideoIdentity);
                if (dbVideoIdentity != null)
                {
                    dbUserSetting.VideoIdentity = "media/gallery/VideoIdentity/" + dbVideoIdentity.PictureUrl;
                }
                var dbLastCertificateMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.LawyerCertificate);
                dbUserSetting.LawyerCertificateDoc= dbLastCertificateMedia == null ? null : "Media/gallery/LawyerCertificate/" +dbLastCertificateMedia.PictureUrl;
                var dbLastNewspaperMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.Newspaper);
                dbUserSetting.NewspaperDoc= dbLastNewspaperMedia == null ? null : "Media/gallery/Newspaper/" +dbLastNewspaperMedia.PictureUrl;
            }

            return dbUserSettings;
        }
        public int ChangeVideoStatusByAdmin(long userSettingId,VideoIdentity Status)
        {
            var dbUserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserSettingId==userSettingId);
            dbUserSetting.VideoIdentityStatus = Status;
            userSettingRepository.Update(dbUserSetting);
            return (int)Status;
        }

        public int ChangeLawyerCertificateStatusByAdmin(long userSettingId, DocStatus Status)
        {
            var dbUserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserSettingId==userSettingId);
            dbUserSetting.LawyerCertificateStatus = Status;
            userSettingRepository.Update(dbUserSetting);
            return (int)Status;
        }
        public int ChangeNewspaperDocStatusByAdmin(long userSettingId, DocStatus Status)
        {
            var dbUserSetting = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserSettingId==userSettingId);
            dbUserSetting.NewspaperStatus = Status;
            userSettingRepository.Update(dbUserSetting);
            return (int)Status;
        }

        public object GetRejectedVideoIdentity(int pageNumber, int count, string search)
        {
            var dbUserSettings=userSettingRepository.GetQuery().Where(z=>z.VideoIdentityStatus == VideoIdentity.Rejected && z.UserTag.Contains(search)).
                OrderBy(x=>x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach(var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z=>z.UserId==dbUserSetting.UserId);

                var dbLastVideoIdentityMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.VideoIdentity);
                dbUserSetting.VideoIdentity= dbLastVideoIdentityMedia == null ? null : "Media/gallery/VideoIdentity/" +dbLastVideoIdentityMedia.PictureUrl;
            }
            var dbUserSettingsCount= userSettingRepository.GetQuery().Where(z => z.VideoIdentityStatus == VideoIdentity.Rejected && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }

        public object GetConfirmedVideoIdentity(int pageNumber, int count, string search)
        {
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.VideoIdentityStatus == VideoIdentity.Confirmed && z.UserTag.Contains(search)).
                OrderBy(x => x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z => z.UserId==dbUserSetting.UserId);

                var dbLastVideoIdentityMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.VideoIdentity);
                dbUserSetting.VideoIdentity= dbLastVideoIdentityMedia == null ? null : "Media/gallery/VideoIdentity/" +dbLastVideoIdentityMedia.PictureUrl;
            }
            var dbUserSettingsCount = userSettingRepository.GetQuery().Where(z => z.VideoIdentityStatus == VideoIdentity.Confirmed && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }
        public object GetUploadedVideoIdentity(int pageNumber, int count, string search)
        {
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.VideoIdentityStatus == VideoIdentity.Uploaded && z.UserTag.Contains(search)).
                OrderBy(x => x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z => z.UserId==dbUserSetting.UserId);

                var dbLastVideoIdentityMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.VideoIdentity);
                dbUserSetting.VideoIdentity= dbLastVideoIdentityMedia == null ? null : "Media/gallery/VideoIdentity/" +dbLastVideoIdentityMedia.PictureUrl;
            }
            var dbUserSettingsCount = userSettingRepository.GetQuery().Where(z => z.VideoIdentityStatus == VideoIdentity.Uploaded && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }

        public object GetUploadedNewspaperDocs(int pageNumber, int count, string search)
        {
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.NewspaperStatus == DocStatus.NotConfirmed && z.UserTag.Contains(search)).
                OrderBy(x => x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z => z.UserId==dbUserSetting.UserId);

                var dbLastNewspaperMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.Newspaper);
                dbUserSetting.NewspaperDoc= dbLastNewspaperMedia == null ? null : "Media/gallery/Newspaper/" +dbLastNewspaperMedia.PictureUrl;
            }
            var dbUserSettingsCount = userSettingRepository.GetQuery().Where(z => z.NewspaperStatus == DocStatus.NotConfirmed && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }

        public object GetUploadedLawyerCertificates(int pageNumber, int count, string search)
        {
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.LawyerCertificateStatus == DocStatus.NotConfirmed && z.UserTag.Contains(search)).
                OrderBy(x => x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z => z.UserId==dbUserSetting.UserId);
                var dbLastCertificateMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.LawyerCertificate);
                dbUserSetting.LawyerCertificateDoc= dbLastCertificateMedia == null ? null : "Media/gallery/LawyerCertificate/" +dbLastCertificateMedia.PictureUrl;
            }
            var dbUserSettingsCount = userSettingRepository.GetQuery().Where(z => z.LawyerCertificateStatus == DocStatus.NotConfirmed && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }

        public object GetConfirmedNewspaperDocs(int pageNumber, int count, string search)
        {
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.NewspaperStatus == DocStatus.Confirmed && z.UserTag.Contains(search)).
                OrderBy(x => x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z => z.UserId==dbUserSetting.UserId);
                var dbLastNewspaperMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.Newspaper);
                dbUserSetting.NewspaperDoc= dbLastNewspaperMedia == null ? null : "Media/gallery/Newspaper/" +dbLastNewspaperMedia.PictureUrl;
            }
            var dbUserSettingsCount = userSettingRepository.GetQuery().Where(z => z.NewspaperStatus == DocStatus.Confirmed  && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }

        public object GetConfirmedLawyerCertificates(int pageNumber, int count, string search)
        {
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.LawyerCertificateStatus == DocStatus.Confirmed && z.UserTag.Contains(search)).
                OrderBy(x => x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z => z.UserId==dbUserSetting.UserId);
                var dbLastCertificateMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.LawyerCertificate);
                dbUserSetting.LawyerCertificateDoc= dbLastCertificateMedia == null ? null : "Media/gallery/LawyerCertificate/" +dbLastCertificateMedia.PictureUrl;
            }
            var dbUserSettingsCount = userSettingRepository.GetQuery().Where(z => z.LawyerCertificateStatus == DocStatus.Confirmed  && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }

        public object GetRejectedLawyerCertificates(int pageNumber, int count, string search)
        {
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.LawyerCertificateStatus == DocStatus.Rejected && z.UserTag.Contains(search)).
                OrderBy(x => x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z => z.UserId==dbUserSetting.UserId);
                var dbLastCertificateMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.LawyerCertificate);
                dbUserSetting.LawyerCertificateDoc= dbLastCertificateMedia == null ? null : "Media/gallery/LawyerCertificate/" +dbLastCertificateMedia.PictureUrl;
            }
            var dbUserSettingsCount = userSettingRepository.GetQuery().Where(z => z.LawyerCertificateStatus == DocStatus.Rejected  && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }

        public object GetRejectedNewspaperDocs(int pageNumber, int count, string search)
        {
            var dbUserSettings = userSettingRepository.GetQuery().Where(z => z.NewspaperStatus == DocStatus.Rejected && z.UserTag.Contains(search)).
                OrderBy(x => x.VideoIdentityUpdateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var dbUserSetting in dbUserSettings)
            {
                dbUserSetting.UserInfo=_userRepository.GetQuery().FirstOrDefault(z => z.UserId==dbUserSetting.UserId);
                var dbLastNewspaperMedia = _mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId==dbUserSetting.UserId && z.Type == MediaTypes.Newspaper);
                dbUserSetting.NewspaperDoc= dbLastNewspaperMedia == null ? null : "Media/gallery/Newspaper/" +dbLastNewspaperMedia.PictureUrl;
            }
            var dbUserSettingsCount = userSettingRepository.GetQuery().Where(z => z.NewspaperStatus == DocStatus.Rejected  && z.UserTag.Contains(search)).Count();
            var obj = new
            {
                UserSettings = dbUserSettings,
                UserSettingsCount = dbUserSettingsCount
            };
            return obj;
        }
    }
}
