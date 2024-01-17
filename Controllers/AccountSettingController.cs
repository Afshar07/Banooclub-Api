using BanooClub.Models;
using BanooClub.Services.AccountSettingServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountSettingController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IAccountSettingService accountSettingService;

        public AccountSettingController(BanooClubDBContext context, IAccountSettingService accountSettingService)
        {
            this.context = context;
            this.accountSettingService = accountSettingService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] AccountSetting inputDto)
        {
            await this.accountSettingService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public List<AccountSetting> GetByUserId(long userId)
        {
            return accountSettingService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<AccountSetting> Get(long accountSettingId)
        {
            return accountSettingService.Get(accountSettingId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await accountSettingService.Delete(id);
        }
    }
}
