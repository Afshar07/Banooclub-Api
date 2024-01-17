using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ActivityServices
{
    public class ActivityService : IActivityService
    {
        private readonly IBanooClubEFRepository<Activity> activityRepository;
        private readonly IBanooClubEFRepository<UserSetting> userSettingRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IHttpContextAccessor _accessor;

        public ActivityService(IBanooClubEFRepository<Activity> activityRepository
            ,IHttpContextAccessor accessor
            ,IBanooClubEFRepository<UserSetting> userSettingRepository
            ,IBanooClubEFRepository<Following> followingRepository
            )
        {
            this.activityRepository = activityRepository;
            this.userSettingRepository = userSettingRepository;
            this.followingRepository = followingRepository;
            _accessor = accessor;
        }
        public async Task Create(Activity inputDto)
        {
            activityRepository.Insert(inputDto);
        }

        public async Task<Activity> Update(Activity item)
        {
            await activityRepository.Update(item);
            return item;
        }

        public async Task<List<Activity>> GetAll()
        {
            List<Activity> activities = new List<Activity>();
            activities = activityRepository.GetQuery().ToList();
            return activities;
        }

        public async Task Delete(long id)
        {
            var activity = activityRepository.GetQuery().FirstOrDefault(z => z.ActivityId == id);
            await activityRepository.Delete(activity);
        }

        public async Task<Activity> Get(long id)
        {
            var activity = activityRepository.GetQuery().FirstOrDefault(z => z.ActivityId == id);
            return activity;
        }
        public object GetByUserId(long userId,int pageNumber,int count)
        {
            var Activities= activityRepository.GetQuery().Where(z => z.UserId == userId).
                OrderByDescending(x=>x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var actsCount= activityRepository.GetQuery().Where(z => z.UserId == userId).Count();


            var MYselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var IsPrivate = userSettingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId).IsPrivateActivity;
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
                    Acts = Activities,
                    ActsCount = actsCount
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
                            Acts = Activities,
                            ActsCount= actsCount
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
        public object GetMyActivities(int pageNumber, int count)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbActivities= activityRepository.GetQuery().Where(z => z.UserId == userId).
                OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            var activityCount = activityRepository.GetQuery().Where(z => z.UserId==userId).Count();
            var obj = new
            {
                ActivitiesCount = activityCount,
                Activities = dbActivities
            };
            return obj;
        }
    }
}
