using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FollowingServices
{
    public class FollowingService : IFollowingService
    {
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<Follower> followerRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService userService;

        public FollowingService(IBanooClubEFRepository<Following> followingRepository, IHttpContextAccessor accessor,
            IBanooClubEFRepository<User> userRepository
            , IBanooClubEFRepository<Follower> followerRepository
            , IUserService userService)
        {
            this.followingRepository = followingRepository;
            _accessor = accessor;
            this.userRepository = userRepository;
            this.userService = userService;
            this.followerRepository = followerRepository;
        }
        public async Task Create(long followingUserId)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbFollowing = new Following()
            {
                UserId = userId,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                FollowingId = 0,
                FollowingUserId = followingUserId
            };
            followingRepository.Insert(dbFollowing);
            await followingRepository.Save();
        }

        public async Task<Following> Update(Following item)
        {
            await followingRepository.Update(item);
            return item;
        }

        public async Task<List<Following>> GetAll()
        {
            List<Following> followings = new List<Following>();
            followings = followingRepository.GetQuery().ToList();
            return followings;
        }

        public async Task Delete(long id)
        {
            var following = followingRepository.GetQuery().FirstOrDefault(z => z.FollowingId == id);
            await followingRepository.Delete(following);
        }

        public void DeleteByFollowingUserId(long followingUserId)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var following = followingRepository.GetQuery().FirstOrDefault(z => z.FollowingUserId == followingUserId && z.UserId==userId);
            followingRepository.Erase(following);
            var follower = followerRepository.GetQuery().FirstOrDefault(z => z.FollowerUserId==userId && z.UserId==followingUserId);
            followerRepository.Erase(follower);
        }

        public async Task<Following> Get(long id)
        {
            var following = followingRepository.GetQuery().FirstOrDefault(z => z.FollowingId == id);
            following.UserInfo = userService.Get(following.FollowingUserId);
            return following;
        }
        public List<Following> GetByUserId(long userId)
        {
            
            var dbFollowings = followingRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            foreach (var item in dbFollowings)
            {
                item.UserInfo = userService.Get(item.FollowingUserId);
            }
            return dbFollowings;
        }

        public List<Following> GetMyFollowings()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbFollowings = followingRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            foreach (var item in dbFollowings)
            {
                var UserInfo = userService.Get(item.FollowingUserId);
                item.UserInfo = UserInfo;
            }
            return dbFollowings;
        }

        public bool IsFollowing(long userId, long followingUserId)
        {
            var dbFollowing = followingRepository.GetQuery().FirstOrDefault(z => z.FollowingUserId == followingUserId && z.UserId == userId);
            var result = dbFollowing != null ? false : true;
            return result;
        }
    }
}
