using BanooClub.Models;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BanooClub.Services.BackgroundServices
{
    public class DiscountBackgroundService : IHostedService
    {
        private Timer? _timer = null;

        #region Constructor

        private readonly IBanooClubEFRepository<Discount> _discountRepository;

        public DiscountBackgroundService( IServiceScopeFactory factory)
        {
            _discountRepository = factory.CreateScope().ServiceProvider.GetRequiredService<IBanooClubEFRepository<Discount>>();
        }

        #endregion

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var interval = TimeSpan.FromHours(24); //TimeSpan.FromHours(24);
            //calculate time to run the first time & delay to set the timer
            //DateTime.Today gives time of midnight 00.00
            var nextRunTime = DateTime.Today.AddDays(1).AddHours(1);
            var curTime = DateTime.Now;
            var firstInterval = nextRunTime.Subtract(curTime);
            Action action = () =>
            {
                var t1 = Task.Delay(firstInterval);
                t1.Wait();
                //remove inactive accounts at expected time
                ExpireDiscounts(null);
                //now schedule it to be called every 24 hours for future
                // timer repeates call to RemoveScheduledAccounts every 24 hours.
                _timer = new Timer(
                    ExpireDiscounts,
                    null,
                    TimeSpan.Zero,
                    interval
                );
            };
            
            // no need to await this call here because this task is scheduled to run much much later.
            Task.Run(action);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void ExpireDiscounts(object state)
        {
            var discounts = _discountRepository.GetQuery()
                .Where(x => x.ExpireDate != null && x.ExpireDate <= DateTime.Now)
                .ToList();

            discounts.ForEach(x => x.IsDeleted = true);
            _discountRepository.Save();
        }
    }
}