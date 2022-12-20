using BanooClub.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        public DiscountBackgroundService(IServiceScopeFactory factory) =>
            _discountRepository = factory.CreateScope().ServiceProvider.GetService<IBanooClubEFRepository<Discount>>();
        
        #endregion

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExpireDiscounts, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }

        private async void ExpireDiscounts(object state)
        {
            try
            {
                var discounts = await _discountRepository.GetQuery()
                    .Where(x => x.ExpireDate != null && x.ExpireDate <= DateTime.Now)
                    .ToListAsync();

                if (discounts != null && discounts.Any())
                {
                    foreach (var discount in discounts)
                    {
                        discount.IsDeleted = true;
                        await _discountRepository.Update(discount);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}