using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.WalletServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] Wallet inputDto)
        {
            return await this.walletService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Wallet> Update([FromBody] Wallet inputDto)
        {
            return await this.walletService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<Wallet> Get(long walletId)
        {
            return await walletService.Get(walletId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await walletService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count)
        {
            return walletService.GetAll(pageNumber, count);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public object GetCredit()
        {
            return walletService.GetCredit();
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public object CheckCredit(long value , long userId)
        {
            return walletService.CheckCredit(value,userId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles ="Admin")]
        public object GetByUserId(long userId)
        {
            return walletService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public object UpdateWallet(long credit , long userId)
        {
            return walletService.UpdateWallet(credit ,userId);
        }

    }
}
