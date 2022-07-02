using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.AccountSettingServices
{
    public interface IAccountSettingService
    {
        Task Create(AccountSetting inputDto);
        Task<AccountSetting> Update(AccountSetting item);
        Task Delete(long id);
        Task<List<AccountSetting>> GetAll();
        Task<AccountSetting> Get(long id);
        List<AccountSetting> GetByUserId(long userId);
    }
}
