using BanooClub.Models;
using BanooClub.Models.Enums;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsCommentServices
{
    public class AdsCommentService : IAdsCommentService
    {
        private readonly IBanooClubEFRepository<AdsComment> adsCommentRepository;
        private readonly IBanooClubEFRepository<Activity> _activityRepository;

        public AdsCommentService(IBanooClubEFRepository<AdsComment> adsCommentRepository
            ,IBanooClubEFRepository<Activity> activityRepository)
        {
            this.adsCommentRepository = adsCommentRepository;
            _activityRepository = activityRepository;
        }
        public async Task Create(AdsComment inputDto)
        {
            adsCommentRepository.Insert(inputDto);
            var dbActivity = new Activity()
            {
                CreateDate = DateTime.Now,
                ActivityId=0,
                IsDeleted = false,
                ObjectId=inputDto.AdsCommentId,
                Type= ActivityTypes.CommentAd,
                UserId= inputDto.UserId
            };
            await _activityRepository.InsertAsync(dbActivity);
        }

        public async Task<AdsComment> Update(AdsComment item)
        {
            await adsCommentRepository.Update(item);
            return item;
        }



        public object GetAll(int pageNumber, int count)
        {
            List<AdsComment> dbAds = new List<AdsComment>();
            dbAds = adsCommentRepository.GetQuery().Skip((pageNumber-1)*count).Take(count).ToList();
            var dbAdsCommentsCount=adsCommentRepository.GetQuery().Count();
            var obj = new
            {
                AdsComments = dbAds,
                AdsCommentsCount = dbAdsCommentsCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var dbAds = adsCommentRepository.GetQuery().FirstOrDefault(z => z.AdsCommentId == id);
            await adsCommentRepository.Delete(dbAds);
        }

        public async Task<AdsComment> Get(long id)
        {
            var dbAds = adsCommentRepository.GetQuery().FirstOrDefault(z => z.AdsCommentId == id);
            return dbAds;
        }

    }
}
