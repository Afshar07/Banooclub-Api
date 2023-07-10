using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Models.Enums;
using BanooClub.Services.LogServices;
using BanooClub.Services.TicketServices;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace BanooClub.Services.CommonServices
{
    public class CommonService : ICommonService
    {
        private readonly ILogService _logService;
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IBanooClubEFRepository<UserSetting> userSettingRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<Activity> activityRepository;
        private readonly IBanooClubEFRepository<SocialMedia> socialMediaRepository;
        private readonly IBanooClubEFRepository<Ads> adsRepository;
        private readonly IBanooClubEFRepository<Roomate> roomateRepository;
        private readonly IBanooClubEFRepository<PostLike> postLikeRepository;
        private readonly IBanooClubEFRepository<Post> postRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<State> stateRepository;
        private readonly IBanooClubEFRepository<City> cityRepository;
        private readonly IBanooClubEFRepository<Consultant> _consultantRepository;
        private readonly IBanooClubEFRepository<BecomeConsultantRequest> _becomeConsultantRequestRepository;
        private readonly IBanooClubEFRepository<FollowRequest> followRequestRepository;
        public CommonService(ILogService logService, IUserService userService
            , IBanooClubEFRepository<UserSetting> userSettingRepository
            , IBanooClubEFRepository<Following> followingRepository
            , IBanooClubEFRepository<SocialMedia> socialMediaRepository
            , IBanooClubEFRepository<Activity> activityRepository
            , IBanooClubEFRepository<Ads> adsRepository
            , IBanooClubEFRepository<Roomate> roomateRepository
            , IBanooClubEFRepository<PostLike> postLikeRepository
            , IBanooClubEFRepository<Post> postRepository
            , IBanooClubEFRepository<User> userRepository
            , IBanooClubEFRepository<State> stateRepository
            , IBanooClubEFRepository<City> cityRepository
            , IBanooClubEFRepository<FollowRequest> followRequestRepository
            , IBanooClubEFRepository<Consultant> consultantRepository
            , IBanooClubEFRepository<BecomeConsultantRequest> becomeConsultantRequestRepository
            , ITicketService ticketService, IHttpContextAccessor accessor)
        {
            _logService = logService;
            _userService = userService;
            _ticketService = ticketService;
            _accessor = accessor;
            this.userSettingRepository = userSettingRepository;
            this.followingRepository = followingRepository;
            this.activityRepository = activityRepository;
            this.socialMediaRepository = socialMediaRepository;
            this.adsRepository = adsRepository;
            this.roomateRepository = roomateRepository;
            this.postLikeRepository = postLikeRepository;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
            this.stateRepository = stateRepository;
            this.cityRepository = cityRepository;
            this.followRequestRepository = followRequestRepository;
            _consultantRepository = consultantRepository;
            _becomeConsultantRequestRepository = becomeConsultantRequestRepository;
        }


        public object GetIndexData()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var Last8Viewer = new List<UserBaseData>();
            var Last8Log = _logService.GetByUserId(userId).DistinctBy(x => x.ViewUserId).OrderByDescending(z => z.CreateDate).Take(8).ToList();
            var RecentActivity = activityRepository.GetQuery().Where(z => z.UserId == userId).OrderByDescending(x => x.CreateDate).Take(7).ToList();
            foreach (var item in Last8Log)
            {
                var userInfo = _userService.Get(item.ViewUserId);
                var obj1 = new UserBaseData()
                {
                    UserId = item.ViewUserId,
                    UserName = userInfo.UserName,
                    Banner = userInfo.BannerFileData,
                    Selfie = userInfo.SelfieFileData,
                    Name = userInfo.Name,
                    FamilyName = userInfo.FamilyName
                };
                Last8Viewer.Add(obj1);
            }
            var dbUserInfo = _userService.Get(userId);

            var dbLastLawyerCertificateMedia = socialMediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.LawyerCertificate);
            dbUserInfo.UserSetting.LawyerCertificateDoc = dbLastLawyerCertificateMedia == null ? null : "Media/gallery/LawyerCertificate/" + dbLastLawyerCertificateMedia.PictureUrl;

            var dbLastNewspaperMedia = socialMediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.Newspaper);
            dbUserInfo.UserSetting.NewspaperDoc = dbLastNewspaperMedia == null ? null : "Media/gallery/Newspaper/" + dbLastNewspaperMedia.PictureUrl;

            var dbKartMelliDoc = socialMediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.KartMelliDoc);
            dbUserInfo.UserSetting.KartMelliDoc = dbKartMelliDoc == null ? null : "Media/gallery/KartMelliDocs/" + dbKartMelliDoc.PictureUrl;

            var dbPassportDoc = socialMediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.PassportDoc);
            dbUserInfo.UserSetting.PassportDoc = dbPassportDoc == null ? null : "Media/gallery/PassportDocs/" + dbPassportDoc.PictureUrl;

            var dbVideoIdentity = socialMediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == userId && z.Type == MediaTypes.VideoIdentity);
            dbUserInfo.UserSetting.VideoIdentity = dbVideoIdentity == null ? null : "Media/gallery/VideoIdentity/" + dbVideoIdentity.PictureUrl;

            #region state & city
            var dbState = stateRepository.GetQuery().FirstOrDefault(z => z.StateId == dbUserInfo.StateId);
            var dbCity = cityRepository.GetQuery().FirstOrDefault(z => z.CityId == dbUserInfo.CityId);
            dbUserInfo.StateName = dbState == null ? "" : dbState.Name;
            dbUserInfo.CityName = dbCity == null ? "" : dbCity.Name;
            #endregion

            var dbtickets = _ticketService.UserUnreadTicketCounts();
            var logCounts = _logService.GetByUserId(userId).Count();
            var logCountsDistinct = _logService.GetByUserId(userId).DistinctBy(x => x.ViewUserId).Count();

            var userAds = adsRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            var userRoomate = dbUserInfo.UserSetting.ActiveRoomate;
            var dbPostIdsForLike = postRepository.GetQuery().Where(z => z.UserId == userId).Select(x => x.PostId).ToList();
            var dbLikes = postLikeRepository.GetQuery().Where(z => dbPostIdsForLike.Contains(z.PostId)).Count();
            BecomeConsultantRequestStatus? lastConsultantRequest = null;
            if (_becomeConsultantRequestRepository.GetQuery().Any(t => t.UserId == userId))
                lastConsultantRequest = _becomeConsultantRequestRepository.GetQuery().Where(t => t.UserId == userId).OrderByDescending(t => t.CreateDate).Select(t => t.Status).FirstOrDefault();
            var obj = new
            {
                ConsultantId = _consultantRepository.GetQuery().Where(t => t.UserId == userId).Select(t => t.Id).FirstOrDefault(),
                LastConsultantRequestStatus = lastConsultantRequest,
                Last8Viewer = Last8Viewer,
                UserInfo = dbUserInfo,
                UnreadTicketCount = dbtickets,
                LogCounts = logCounts,
                LogCountsDistinct = logCountsDistinct,
                RecentActivity = RecentActivity,
                BaseData = new
                {
                    Banner = dbUserInfo.BannerFileData,
                    Selfie = dbUserInfo.SelfieFileData,
                    Name = dbUserInfo.Name,
                    FamilyName = dbUserInfo.FamilyName,
                    UserName = dbUserInfo.UserName,
                    FollowersCount = dbUserInfo.FollowersCount,
                    FollowingsCount = dbUserInfo.FollowingsCount,
                    UserType = dbUserInfo.Type,
                    UserAds = userAds == null ? false : true,
                    ActiveRoomate = userRoomate,
                    LikesCount = dbLikes
                }
            };
            return obj;
        }
        public object GetUserIndex(long userId)
        {
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;


            var Last8Viewer = new List<UserBaseData>();
            var Last8Log = _logService.GetByUserId(userId).DistinctBy(x => x.ViewUserId).OrderByDescending(z => z.CreateDate).Take(8).ToList();
            var RecentActivity = activityRepository.GetQuery().Where(z => z.UserId == userId).OrderByDescending(x => x.CreateDate).Take(7).ToList();
            foreach (var item in Last8Log)
            {
                var userInfo = _userService.Get(item.ViewUserId);
                var obj = new UserBaseData()
                {
                    UserId = item.ViewUserId,
                    UserName = userInfo.UserName,
                    Banner = userInfo.BannerFileData,
                    Selfie = userInfo.SelfieFileData,
                    Name = userInfo.Name,
                    FamilyName = userInfo.FamilyName
                };
                Last8Viewer.Add(obj);
            }

            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateSocial;
            var dbUserInfo = _userService.Get(userId);

            #region state & city
            var dbState = stateRepository.GetQuery().FirstOrDefault(z => z.StateId == dbUserInfo.StateId);
            var dbCity = cityRepository.GetQuery().FirstOrDefault(z => z.CityId == dbUserInfo.CityId);
            dbUserInfo.StateName = dbState == null ? "" : dbState.Name;
            dbUserInfo.CityName = dbCity == null ? "" : dbCity.Name;
            #endregion

            var userAds = adsRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            var userRoomate = dbUserInfo.UserSetting.ActiveRoomate;

            var followStatus = FollowStatus.None;
            if (MYselfId > 0)
            {
                //var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                //if(dbFollowing != null)
                //{
                //     Followed= true;
                //}
                #region check follow Status


                //آیا کاربر وارد شده یوزر آی دی ذکر شده رو فالو دارد؟
                var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                //اگر فالو دارد
                if (dbFollowing != null)
                {
                    followStatus = FollowStatus.Follow;
                }
                //اگر فالو ندارد
                else
                {

                    var dbFollowRequest = followRequestRepository.GetQuery().FirstOrDefault(x => x.FollowerUserId == MYselfId && x.FollowingUserId == userId);
                    if (dbFollowRequest != null)
                    {
                        followStatus = FollowStatus.Pending;
                    }
                    else
                    {
                        followStatus = FollowStatus.None;
                    }

                }
                #endregion
            }


            //var Followed = false;
            if (IsPrivate == false)
            {
                //if (MYselfId>0)
                //{
                //    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                //    if (dbFollowing != null)
                //    {
                //        Followed = true;
                //    }

                //}

                var dbtickets = _ticketService.UserUnreadTicketCounts();
                var logCounts = _logService.GetByUserId(userId).Count();
                var logCountsDistinct = _logService.GetByUserId(userId).DistinctBy(x => x.ViewUserId).Count();

                var obj = new
                {
                    Last8Viewer = Last8Viewer,
                    UserInfo = dbUserInfo,
                    UnreadTicketCount = dbtickets,
                    LogCounts = logCounts,
                    LogCountsDistinct = logCountsDistinct,
                    RecentActivity = RecentActivity,
                    Status = (int)PostVisibility.VisibleAndPublic,
                    //FollowButton = Followed == false ? true : false,
                    BaseData = new
                    {
                        Banner = dbUserInfo.BannerFileData,
                        Selfie = dbUserInfo.SelfieFileData,
                        Name = dbUserInfo.Name,
                        FamilyName = dbUserInfo.FamilyName,
                        UserName = dbUserInfo.UserName,
                        FollowersCount = dbUserInfo.FollowersCount,
                        FollowingsCount = dbUserInfo.FollowingsCount,
                        UserType = dbUserInfo.Type,
                        UserAds = userAds == null ? false : true,
                        ActiveRoomate = userRoomate
                    },
                    FollowStatus = followStatus,
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
                        var dbtickets = _ticketService.UserUnreadTicketCounts();
                        var logCounts = _logService.GetByUserId(userId).Count();
                        var logCountsDistinct = _logService.GetByUserId(userId).DistinctBy(x => x.ViewUserId).Count();

                        var obj = new
                        {
                            UserInfo = dbUserInfo,
                            UnreadTicketCount = dbtickets,
                            LogCounts = logCounts,
                            LogCountsDistinct = logCountsDistinct,
                            Status = (int)PostVisibility.Visible,
                            RecentActivity = RecentActivity,
                            Last8Viewer = Last8Viewer,
                            //FollowButton = false,
                            BaseData = new
                            {
                                Banner = dbUserInfo.BannerFileData,
                                Selfie = dbUserInfo.SelfieFileData,
                                Name = dbUserInfo.Name,
                                FamilyName = dbUserInfo.FamilyName,
                                UserName = dbUserInfo.UserName,
                                FollowersCount = dbUserInfo.FollowersCount,
                                FollowingsCount = dbUserInfo.FollowingsCount,
                                UserType = dbUserInfo.Type,
                                UserAds = userAds == null ? false : true,
                                ActiveRoomate = userRoomate
                            }
                            ,
                            FollowStatus = followStatus,
                        };
                        return obj;
                    }
                    else
                    {
                        var obj2 = new
                        {
                            Status = (int)PostVisibility.PrivateAndNotFollowing,
                            BaseData = new
                            {
                                Banner = dbUserInfo.BannerFileData,
                                Selfie = dbUserInfo.SelfieFileData,
                                Name = dbUserInfo.Name,
                                FamilyName = dbUserInfo.FamilyName,
                                UserName = dbUserInfo.UserName,
                                FollowersCount = dbUserInfo.FollowersCount,
                                FollowingsCount = dbUserInfo.FollowingsCount,
                                UserType = dbUserInfo.Type,
                                UserAds = userAds == null ? false : true,
                                ActiveRoomate = userRoomate
                            },
                            //FollowButton = true

                            FollowStatus = followStatus,
                        };
                        return obj2;

                    }
                }

                var obj3 = new
                {
                    Status = (int)PostVisibility.PrivateAndNotLogged,
                    BaseData = new
                    {
                        Banner = dbUserInfo.BannerFileData,
                        Selfie = dbUserInfo.SelfieFileData,
                        Name = dbUserInfo.Name,
                        FamilyName = dbUserInfo.FamilyName,
                        UserName = dbUserInfo.UserName,
                        FollowersCount = dbUserInfo.FollowersCount,
                        FollowingsCount = dbUserInfo.FollowingsCount,
                        UserType = dbUserInfo.Type,
                        UserAds = userAds == null ? false : true,
                        ActiveRoomate = userRoomate
                    }
                    //FollowButton = true,
                    ,
                    FollowStatus = followStatus,

                };
                return obj3;

            }
        }

        public object GetUserIndexByUserName(string userName)
        {
            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;

            var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserName == userName);
            if (dbUser != null)
            {
                var userId = dbUser.UserId;
                var Last8Viewer = new List<UserBaseData>();
                var Last8Log = _logService.GetByUserId(userId).DistinctBy(x => x.ViewUserId).OrderByDescending(z => z.CreateDate).Take(8).ToList();
                var RecentActivity = activityRepository.GetQuery().Where(z => z.UserId == userId).OrderByDescending(x => x.CreateDate).Take(7).ToList();
                foreach (var item in Last8Log)
                {
                    var userInfo = _userService.Get(item.ViewUserId);
                    var obj = new UserBaseData()
                    {
                        UserId = item.ViewUserId,
                        UserName = userInfo.UserName,
                        Banner = userInfo.BannerFileData,
                        Selfie = userInfo.SelfieFileData,
                        Name = userInfo.Name,
                        FamilyName = userInfo.FamilyName
                    };
                    Last8Viewer.Add(obj);
                }

                var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateSocial;
                var dbUserInfo = _userService.Get(userId);

                #region state & city
                var dbState = stateRepository.GetQuery().FirstOrDefault(z => z.StateId == dbUserInfo.StateId);
                var dbCity = cityRepository.GetQuery().FirstOrDefault(z => z.CityId == dbUserInfo.CityId);
                dbUserInfo.StateName = dbState == null ? "" : dbState.Name;
                dbUserInfo.CityName = dbCity == null ? "" : dbCity.Name;
                #endregion

                var userAds = adsRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
                var userRoomate = dbUserInfo.UserSetting.ActiveRoomate;

                var followStatus = FollowStatus.None;
                if (MYselfId > 0)
                {
                    //var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                    //if(dbFollowing != null)
                    //{
                    //     Followed= true;
                    //}
                    #region check follow Status


                    //آیا کاربر وارد شده یوزر آی دی ذکر شده رو فالو دارد؟
                    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                    //اگر فالو دارد
                    if (dbFollowing != null)
                    {
                        followStatus = FollowStatus.Follow;
                    }
                    //اگر فالو ندارد
                    else
                    {

                        var dbFollowRequest = followRequestRepository.GetQuery().FirstOrDefault(x => x.FollowerUserId == MYselfId && x.FollowingUserId == userId);
                        if (dbFollowRequest != null)
                        {
                            followStatus = FollowStatus.Pending;
                        }
                        else
                        {
                            followStatus = FollowStatus.None;
                        }

                    }
                    #endregion
                }


                //var Followed = false;
                if (IsPrivate == false)
                {
                    //if (MYselfId>0)
                    //{
                    //    var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.UserId == MYselfId & z.FollowingUserId == userId);
                    //    if (dbFollowing != null)
                    //    {
                    //        Followed = true;
                    //    }

                    //}

                    var dbtickets = _ticketService.UserUnreadTicketCounts();
                    var logCounts = _logService.GetByUserId(userId).Count();
                    var logCountsDistinct = _logService.GetByUserId(userId).DistinctBy(x => x.ViewUserId).Count();

                    var obj = new
                    {
                        Last8Viewer = Last8Viewer,
                        UserInfo = dbUserInfo,
                        UnreadTicketCount = dbtickets,
                        LogCounts = logCounts,
                        LogCountsDistinct = logCountsDistinct,
                        RecentActivity = RecentActivity,
                        Status = (int)PostVisibility.VisibleAndPublic,
                        //FollowButton = Followed == false ? true : false,
                        BaseData = new
                        {
                            Banner = dbUserInfo.BannerFileData,
                            Selfie = dbUserInfo.SelfieFileData,
                            Name = dbUserInfo.Name,
                            FamilyName = dbUserInfo.FamilyName,
                            UserName = dbUserInfo.UserName,
                            FollowersCount = dbUserInfo.FollowersCount,
                            FollowingsCount = dbUserInfo.FollowingsCount,
                            UserType = dbUserInfo.Type,
                            UserAds = userAds == null ? false : true,
                            ActiveRoomate = userRoomate
                        },
                        FollowStatus = followStatus,
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
                            var dbtickets = _ticketService.UserUnreadTicketCounts();
                            var logCounts = _logService.GetByUserId(userId).Count();
                            var logCountsDistinct = _logService.GetByUserId(userId).DistinctBy(x => x.ViewUserId).Count();

                            var obj = new
                            {
                                UserInfo = dbUserInfo,
                                UnreadTicketCount = dbtickets,
                                LogCounts = logCounts,
                                LogCountsDistinct = logCountsDistinct,
                                Status = (int)PostVisibility.Visible,
                                RecentActivity = RecentActivity,
                                Last8Viewer = Last8Viewer,
                                //FollowButton = false,
                                BaseData = new
                                {
                                    Banner = dbUserInfo.BannerFileData,
                                    Selfie = dbUserInfo.SelfieFileData,
                                    Name = dbUserInfo.Name,
                                    FamilyName = dbUserInfo.FamilyName,
                                    UserName = dbUserInfo.UserName,
                                    FollowersCount = dbUserInfo.FollowersCount,
                                    FollowingsCount = dbUserInfo.FollowingsCount,
                                    UserType = dbUserInfo.Type,
                                    UserAds = userAds == null ? false : true,
                                    ActiveRoomate = userRoomate
                                }
                                ,
                                FollowStatus = followStatus,
                            };
                            return obj;
                        }
                        else
                        {
                            var obj2 = new
                            {
                                Status = (int)PostVisibility.PrivateAndNotFollowing,
                                BaseData = new
                                {
                                    Banner = dbUserInfo.BannerFileData,
                                    Selfie = dbUserInfo.SelfieFileData,
                                    Name = dbUserInfo.Name,
                                    FamilyName = dbUserInfo.FamilyName,
                                    UserName = dbUserInfo.UserName,
                                    FollowersCount = dbUserInfo.FollowersCount,
                                    FollowingsCount = dbUserInfo.FollowingsCount,
                                    UserType = dbUserInfo.Type,
                                    UserAds = userAds == null ? false : true,
                                    ActiveRoomate = userRoomate
                                },
                                //FollowButton = true

                                FollowStatus = followStatus,
                            };
                            return obj2;

                        }
                    }

                    var obj3 = new
                    {
                        Status = (int)PostVisibility.PrivateAndNotLogged,
                        BaseData = new
                        {
                            Banner = dbUserInfo.BannerFileData,
                            Selfie = dbUserInfo.SelfieFileData,
                            Name = dbUserInfo.Name,
                            FamilyName = dbUserInfo.FamilyName,
                            UserName = dbUserInfo.UserName,
                            FollowersCount = dbUserInfo.FollowersCount,
                            FollowingsCount = dbUserInfo.FollowingsCount,
                            UserType = dbUserInfo.Type,
                            UserAds = userAds == null ? false : true,
                            ActiveRoomate = userRoomate
                        }
                        //FollowButton = true,
                        ,
                        FollowStatus = followStatus,

                    };
                    return obj3;

                }
            }
            else
            {
                return null;
            }

        }
    }
}
