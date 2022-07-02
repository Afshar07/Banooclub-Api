using Infrastructure;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.AccountServices;
using BanooClub.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.RatingServices
{
    public class RatingService : IRatingService
    {
        private readonly IBanooClubEFRepository<Rating> _ratingRepository;
        private readonly IBanooClubEFRepository<User> _userRepository;
        private readonly IHttpContextAccessor _accessor;
        public RatingService(IBanooClubEFRepository<Rating> rateRepository,IHttpContextAccessor accessor, IBanooClubEFRepository<User> userRepository)
        {
            _ratingRepository = rateRepository;
            _userRepository = userRepository;
            _accessor = accessor;
        }

        public async Task<IServiceResult<RateOveral>> GetByObjectIdAndType(long objectId , RatingType type)
        {
            var dbRates = _ratingRepository.GetQuery().Where(z => z.ObjectId==objectId && z.Type == type).ToList();
            //var users = (await _accountManagerService.GetByIds(dbRates.Select(p => p.UserId).ToList()));

            var Five = dbRates.Where(x => x.Rate == 5).Count();
            var Four = dbRates.Where(x => x.Rate == 4).Count();
            var Three = dbRates.Where(x => x.Rate == 3).Count();
            var Two = dbRates.Where(x => x.Rate == 2).Count();
            var One = dbRates.Where(x => x.Rate == 1).Count();
            var Sum = ((Five * 5) + (Four * 4) + (Three * 3) + (Two * 2) + (One * 1));
            var RATECOUNT = Five + Four + Three + Two + One;
            double AVG = 0.0;
            if (Sum > 0)
            {
                AVG = (double)Sum / (double)dbRates.Count();
            }
            RateOveral obj = new RateOveral()
            {
                FiveStar = RATECOUNT > 0 ? (Five * 100) / RATECOUNT : 0,
                FourStar = RATECOUNT > 0 ? (Four * 100) / RATECOUNT : 0,
                ThreeStar = RATECOUNT > 0 ? (Three * 100) / RATECOUNT : 0,
                TwoStar = RATECOUNT > 0 ? (Two * 100) / RATECOUNT : 0,
                OneStar = RATECOUNT > 0 ? (One * 100) / RATECOUNT : 0,
                RateCount = RATECOUNT,
                Average = AVG
            };
            //var OBJS = new List<object>()
            //{
            //    model,
            //    obj
            //};

            return new ServiceResult<RateOveral>().Ok(obj);
        }

        public IServiceResult<object> GetAll()
        {
            var dbCourseRates = _ratingRepository.GetQuery().ToList();

            var OBJS = new List<List<object>>();
            foreach (var rate in dbCourseRates)
            {
                //var dbuser = _accountManagerService.GetById(rate.UserId);
                var dbuser = _userRepository.GetQuery().FirstOrDefault(z=>z.UserId == rate.UserId);
                var obj = new List<object>()
                {
                    rate,
                    dbuser,
                };
                OBJS.Add(obj);
            }
            return new ServiceResult<object>().Ok(OBJS);
        }

        public async Task<IServiceResult<bool>> DeleteProductRate(long id)
        {
            try
            {
                var rate = _ratingRepository.GetQuery().FirstOrDefault(z => z.RatingId==id);
                await _ratingRepository.Delete(rate);
                return new ServiceResult<bool>().Ok(true);
            }
            catch(Exception ex)
            {
                return new ServiceResult<bool>().Ok(false);
            }
                
        }

        public async Task<IServiceResult<long>> CreateObjectRate(int rate, long objectId,RatingType type)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbRates = _ratingRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId == objectId && z.Type == type);
            if (dbRates != null)
                return new ServiceResult<long>().Failure("You have submit a rate recently");

            var model = new Rating { UserId = userId, ObjectId = objectId, Rate = rate, RatingId = 0 , Type = type , CreateDate = DateTime.Now, IsDeleted = false };
            await _ratingRepository.InsertAsync(model);

            return new ServiceResult<long>().Ok(model.RatingId);
        }

        public async Task<IServiceResult<Rating>> UpdateProductRate(Rating model)
        {
            await _ratingRepository.Update(model);
            return new ServiceResult<Rating>().Ok(model);
        }
    }
}
