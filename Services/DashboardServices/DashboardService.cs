using BanooClub.Models;
using BanooClub.Models.Enums;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.DashboardServices
{
    public class DashboardService : IDashboardService
    {
        #region Constructor

        private readonly IBanooClubEFRepository<Ads> _adsRepository;
        private readonly IBanooClubEFRepository<User> _userRepository;
        private readonly IBanooClubEFRepository<Post> _postRepository;
        private readonly IBanooClubEFRepository<Forum> _forumRepository;
        private readonly IBanooClubEFRepository<Ticket> _ticketRepository;
        private readonly IBanooClubEFRepository<Roomate> _roommateRepository;

        public DashboardService(IServiceProvider serviceProvider)
        {
            _adsRepository = serviceProvider.GetService<IBanooClubEFRepository<Ads>>();
            _userRepository = serviceProvider.GetService<IBanooClubEFRepository<User>>();
            _postRepository = serviceProvider.GetService<IBanooClubEFRepository<Post>>();
            _forumRepository = serviceProvider.GetService<IBanooClubEFRepository<Forum>>();
            _ticketRepository = serviceProvider.GetService<IBanooClubEFRepository<Ticket>>();
            _roommateRepository = serviceProvider.GetService<IBanooClubEFRepository<Roomate>>();
        }

        #endregion

        public async Task<object> GetDashboardInfo()
        {
            return new
            {
                RommatesCount = _roommateRepository.GetQuery().Count(),
                RegisteredUsersCount = _userRepository.GetQuery().Count(),
                TicketsAwaitingResponseCount = _ticketRepository.GetQuery().Where(x => !x.IsRead).Count(),
                ForumsAwaitingConfirmCount = _forumRepository.GetQuery().Where(x => x.Status == ForumStatus.DeActive).Count(),
                ForumsReportedByBots = _forumRepository.GetQuery().Where(x => x.Status == ForumStatus.ReportedByRobot).Count(),
                AdsAwaitingConfirmCount = _adsRepository.GetQuery().Where(x => x.Status == (int)AdsStatus.NotConfirmed).Count(),
                NotConfirmedPostsCount = _postRepository.GetQuery().Where(x => x.Status == (int)PostStatus.NotConfirmed).Count()
            };
        }
    }
}