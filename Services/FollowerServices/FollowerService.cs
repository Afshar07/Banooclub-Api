using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FollowerServices
{
    public class FollowerService : IFollowerService
    {
        private readonly IBanooClubEFRepository<Follower> followerRepository;
        private readonly IBanooClubEFRepository<Following> followingRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IUserService userService;

        private readonly IHttpContextAccessor _accessor;

        public FollowerService(IBanooClubEFRepository<Follower> followerRepository,
            IBanooClubEFRepository<Following> followingRepository,
            IHttpContextAccessor accessor,
            IUserService userService,
            IBanooClubEFRepository<User> userRepository)
        {
            this.followerRepository = followerRepository;
            _accessor = accessor;
            this.userRepository = userRepository;
            this.userService = userService;
            this.followingRepository = followingRepository;
        }
        public async Task Create(Follower inputDto)
        {
            followerRepository.Insert(inputDto);
        }

        public async Task<Follower> Update(Follower item)
        {
            await followerRepository.Update(item);
            return item;
        }

        public async Task<List<Follower>> GetAll()
        {
            List<Follower> followers = new List<Follower>();
            followers = followerRepository.GetQuery().ToList();
            return followers;
        }

        public async Task Delete(long id)
        {
            var follower = followerRepository.GetQuery().FirstOrDefault(z => z.FollowerId == id);
            await followerRepository.Delete(follower);
        }

        public void DeleteByFollowerUserId(long followerUserId)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var follower = followerRepository.GetQuery().FirstOrDefault(z => z.FollowerUserId == followerUserId && z.UserId==userId);
            followerRepository.Erase(follower);
            var following = followingRepository.GetQuery().FirstOrDefault(z => z.FollowingUserId ==userId && z.UserId == followerUserId);
            followingRepository.Erase(following);

        }

        public async Task<Follower> Get(long id)
        {
            var follower = followerRepository.GetQuery().FirstOrDefault(z => z.FollowerId == id);
            follower.UserInfo = userService.Get(follower.FollowerUserId);
            return follower;
        }

        public List<Follower> GetMyFollowers()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbFollowers = followerRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            foreach (var item in dbFollowers)
            {
                var UserInfo = userService.Get(item.FollowerUserId);
                var dbFollower = followerRepository.GetQuery().FirstOrDefault(z => z.FollowerUserId==userId && z.UserId == item.UserId);

                item.UserInfo = UserInfo;
                if(dbFollower != null)
                {
                    item.UserInfo.IsFollowing =true;
                }
                else
                {
                    item.UserInfo.IsFollowing = false;
                }
                
            }
            return dbFollowers;
        }

        public List<Follower> GetByUserId(long userId)
        {
            var dbFollowers = followerRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            foreach (var item in dbFollowers)
            {
                var UserInfo = userService.Get(item.FollowerUserId);
                item.UserInfo = UserInfo;
            }
            return dbFollowers;
        }
    }
}
