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

namespace BanooClub.Services.PostReportServices
{
    public class PostReportService : IPostReportService
    {
        private readonly IBanooClubEFRepository<PostReport> postReportRepository;
        private readonly IBanooClubEFRepository<Post> postRepository;
        private readonly IBanooClubEFRepository<Activity> activityRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService userService;
        public PostReportService(IBanooClubEFRepository<PostReport> postReportRepository
            , IBanooClubEFRepository<Post> postRepository
            , IBanooClubEFRepository<Activity> activityRepository
            , IBanooClubEFRepository<User> userRepository
            , IUserService userService
            , IHttpContextAccessor accessor)
        {
            this.postReportRepository = postReportRepository;
            this.postRepository = postRepository;
            _accessor = accessor;
            this.activityRepository = activityRepository;
            this.userRepository = userRepository;
            this.userService=userService;
        }
        public async Task Create(PostReport inputDto)
        {
            inputDto.ReporterUserId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.IsConfirmed = false;
            inputDto.IsDeleted = false;
            var report=postReportRepository.Insert(inputDto);

            activityRepository.Insert(new Activity()
            {
                CreateDate=DateTime.Now,
                IsDeleted=false,
                ActivityId=0,
                ObjectId=report.PostReportId,
                Type=ActivityTypes.PostReport,
                UserId=inputDto.ReporterUserId
            });
        }

        public async Task<PostReport> Update(PostReport item)
        {
            item.IsDeleted = false;
            await postReportRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }
            //string cmd = "select PR.*,(U.Name + ' ' +U.FamilyName) as UserName" +
            //    ",(Reporter.Name+' '+Reporter.FamilyName) as ReporterName from Common.PostReports PR " +
            //    "left join Social.Posts P on PR.PostId = P.PostId " +
            //    "join [User].Users Reporter on Reporter.UserId =PR.ReporterUserId " +
            //    $"join [user].Users U on U.UserId = PR.UserId where PR.IsDeleted =0 and PR.Reason like N'%{search}%' " +
            //    $"order by PR.PostReportId DESC OFFSET {(pageNumber-1)*count} Rows FETCH NEXT {count} ROWS ONLY ";
            //var reports= postReportRepository.DapperSqlQuery(cmd).Result;

            var reportsCount = postReportRepository.GetQuery().Where(z=>z.Reason.Contains(search)).Count();
            List<PostReport> reports = new List<PostReport>();
            reports = postReportRepository.GetQuery().Where(z=>z.Reason.Contains(search)).OrderByDescending(x => x.CreateDate).Skip((pageNumber-1)*count).Take(count).ToList();
            foreach (var report in reports)
            {
                var dbPost = postRepository.GetQuery().FirstOrDefault(z => z.PostId == report.PostId);
                report.Content = dbPost == null ? "" : dbPost.Description ;
                report.ReportedUserInfo=userRepository.GetQuery().FirstOrDefault(z=>z.UserId == report.UserId);

                report.ReporterUserInfo=userRepository.GetQuery().FirstOrDefault(z => z.UserId == report.ReporterUserId);
            }
            var obj = new
            {
                Reports = reports,
                ReportsCount = reportsCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var review = postReportRepository.GetQuery().FirstOrDefault(z => z.PostReportId == id);
            await postReportRepository.Delete(review);
        }

        public async Task<object> Get(long id)
        {
            var postReport = postReportRepository.GetQuery().FirstOrDefault(z => z.PostReportId == id);
            var postContent = postRepository.GetQuery().FirstOrDefault(z => z.PostId==postReport.PostId);
            var obj = new
            {
                PostReport = postReport,
                PostContent = postContent
            };
            return obj;
        }
        public List<PostReport> GetByUserId(long userId)
        {
            return postReportRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }
    }
}
