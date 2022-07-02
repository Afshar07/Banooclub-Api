using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FollowRequestServices
{
    public class FollowRequestService : IFollowRequestService
    {
        private readonly IBanooClubEFRepository<FollowRequest> followRequestRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<Follower> followerRepository;
        private readonly IBanooClubEFRepository<Activity> activityRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService userService;

        public FollowRequestService(IBanooClubEFRepository<FollowRequest> followRequestRepository, IHttpContextAccessor accessor,
            IBanooClubEFRepository<Following> followingRepository,
            IBanooClubEFRepository<Activity> activityRepository,
            IBanooClubEFRepository<Follower> followerRepository,
            IBanooClubEFRepository<User> userRepository,
            IUserService userService)
        {
            this.followRequestRepository = followRequestRepository;
            _accessor = accessor;
            this.userService = userService;
            this.userRepository = userRepository;
            this.followerRepository = followerRepository;
            this.followingRepository = followingRepository;
            this.activityRepository= activityRepository;
        }
        public async Task<bool> Create(long followingUserId)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            if(userId == followingUserId)
            {
                return false;
            }
            else
            {
                try
                {
                    var CheckReq = followRequestRepository.GetQuery().FirstOrDefault(z => z.FollowingUserId == followingUserId && z.FollowerUserId == userId);
                    var IsFollowing = followerRepository.GetQuery().FirstOrDefault(z => z.UserId == followingUserId && z.FollowerUserId == userId);
                    if (CheckReq == null && IsFollowing == null)
                    {
                        var dbFollowReq = new FollowRequest()
                        {
                            FollowerUserId = userId,
                            FollowingUserId = followingUserId,
                            IsDeleted = false,
                            FollowRequestId = 0
                        };
                        var dbFoReq = followRequestRepository.Insert(dbFollowReq);

                        var dbActivity = new Activity()
                        {
                            CreateDate = DateTime.Now,
                            IsDeleted=false,
                            ActivityId = 0,
                            ObjectId=dbFoReq.FollowRequestId,
                            Type=ActivityTypes.FollowRequest,
                            UserId=userId
                        };
                        activityRepository.Insert(dbActivity);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<FollowRequest> Update(FollowRequest item)
        {
            await followRequestRepository.Update(item);
            return item;
        }

        public async Task<List<FollowRequest>> GetAll()
        {
            List<FollowRequest> followings = new List<FollowRequest>();
            followings = followRequestRepository.GetQuery().ToList();
            return followings;
        }

        public async Task Delete(long id)
        {
            var following = followRequestRepository.GetQuery().FirstOrDefault(z => z.FollowRequestId == id);
            await followRequestRepository.Delete(following);
        }

        public async Task<FollowRequest> Get(long id)
        {
            var following = followRequestRepository.GetQuery().FirstOrDefault(z => z.FollowRequestId == id);
            following.FollowerInfo = userService.Get(following.FollowerUserId);
            return following;
        }
        public List<FollowRequest> GetByUserId()
        {
            var followingUserId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbFollowers = followRequestRepository.GetQuery().Where(z => z.FollowingUserId == followingUserId).ToList();
            foreach (var item in dbFollowers)
            {
                var UserInfo = userService.Get(item.FollowerUserId);
                item.FollowerInfo = UserInfo;
            }
            return dbFollowers;
        }
        public async Task<bool> QuestRespond(long followRequestId, bool Respond)
        {
            try
            {
                var dbReq = followRequestRepository.GetQuery().FirstOrDefault(z => z.FollowRequestId == followRequestId);
                if (Respond == true)
                {
                    var dbFollowerInsert = followerRepository.Insert(new Follower()
                    {
                        CreateDate = DateTime.Now,
                        IsDeleted = false,
                        FollowerId = 0,
                        FollowerUserId = dbReq.FollowerUserId,
                        UserId = dbReq.FollowingUserId,
                    });
                    var dbActivityFollower = new Activity()
                    {
                        CreateDate= DateTime.Now,
                        IsDeleted= false,
                        ActivityId=0,
                        ObjectId=dbFollowerInsert.FollowerId,
                        Type=ActivityTypes.Follow,
                        UserId=dbReq.FollowerUserId
                    };

                    activityRepository.Insert(dbActivityFollower);

                    var dbFollowingInsert = followingRepository.Insert(new Following()
                    {
                        CreateDate = DateTime.Now,
                        IsDeleted = false,
                        FollowingId = 0,
                        FollowingUserId = dbReq.FollowingUserId,
                        UserId = dbReq.FollowerUserId,
                    });

                    var dbActivityFollowing = new Activity()
                    {
                        CreateDate= DateTime.Now,
                        IsDeleted= false,
                        ActivityId=0,
                        ObjectId=dbFollowingInsert.FollowingId,
                        Type=ActivityTypes.Followed,
                        UserId=dbReq.FollowingUserId
                    };

                    activityRepository.Insert(dbActivityFollowing);


                    followRequestRepository.Erase(dbReq);
                }
                else
                {
                    followRequestRepository.Erase(dbReq);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
