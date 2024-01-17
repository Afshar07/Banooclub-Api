using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.AccountSettingServices
{
    public class AccountSettingService : IAccountSettingService
    {
        private readonly IBanooClubEFRepository<AccountSetting> accountSettingRepository;

        public AccountSettingService(IBanooClubEFRepository<AccountSetting> accountSettingRepository)
        {
            this.accountSettingRepository = accountSettingRepository;
        }
        public async Task Create(AccountSetting inputDto)
        {
            accountSettingRepository.Insert(inputDto);
        }

        public async Task<AccountSetting> Update(AccountSetting item)
        {
            await accountSettingRepository.Update(item);
            return item;
        }

        public async Task<List<AccountSetting>> GetAll()
        {
            List<AccountSetting> accountSettings = new List<AccountSetting>();
            accountSettings = accountSettingRepository.GetQuery().ToList();
            return accountSettings;
        }

        public async Task Delete(long id)
        {
            var accountSetting = accountSettingRepository.GetQuery().FirstOrDefault(z => z.AccountSettingId == id);
            await accountSettingRepository.Delete(accountSetting);
        }

        public async Task<AccountSetting> Get(long id)
        {
            var accountSetting = accountSettingRepository.GetQuery().FirstOrDefault(z => z.AccountSettingId == id);
            return accountSetting;
        }
        public List<AccountSetting> GetByUserId(long userId)
        {
            return accountSettingRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }
    }
}
