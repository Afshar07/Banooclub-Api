using Infrastructure;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.WalletServices
{
    public class WalletService : IWalletService
    {
        private readonly IBanooClubEFRepository<Wallet> walletRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly IHttpContextAccessor _accessor;
        public WalletService(IBanooClubEFRepository<Wallet> walletRepository, IBanooClubEFRepository<SocialMedia> mediaRepository
            , IBanooClubEFRepository<User> userRepository, IHttpContextAccessor accessor)
        {
            this.walletRepository = walletRepository;
            this.userRepository = userRepository;
            this.mediaRepository = mediaRepository;
            _accessor = accessor;
        }
        public async Task<long> Create(Wallet inputDto)
        {
            var dbWallet = walletRepository.GetQuery().FirstOrDefault(z => z.UserId == inputDto.UserId);
            if (dbWallet == null)
            {
                inputDto.IsDeleted = false;
                var creation =walletRepository.Insert(inputDto);
                return creation.WalletId;
            }
            else
            {
                throw new System.Exception("this user already has a wallet");
            }

        }

        public async Task<Wallet> Update(Wallet item)
        {
            item.UpdateDate = DateTime.Now;
            await walletRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<Wallet> wallets = new List<Wallet>();
            wallets = walletRepository.GetQuery().OrderByDescending(x => x.WalletId).Skip((pageNumber-1)*count).Take(count).ToList();
            var walletsCount = walletRepository.GetQuery().Count();
            foreach (var wallet in wallets)
            {
                wallet.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == wallet.UserId);
                var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == wallet.UserId && z.Type == Models.Enums.MediaTypes.Profile);
                wallet.UserInfo.SelfieFileData = dbMedia == null ? "" : dbMedia.PictureUrl;
            }
            var obj = new
            {
                Wallets = wallets,
                WalletsCount = walletsCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var wallet = walletRepository.GetQuery().FirstOrDefault(z => z.WalletId == id);
                await walletRepository.Delete(wallet);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<Wallet> Get(long id)
        {
            var wallet = walletRepository.GetQuery().FirstOrDefault(z => z.WalletId == id);
            wallet.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == wallet.UserId);
            var dbMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == wallet.UserId && z.Type == Models.Enums.MediaTypes.Profile);
            wallet.UserInfo.SelfieFileData = dbMedia == null ? "" : dbMedia.PictureUrl;
            return wallet;
        }
        public List<Wallet> GetByUserId(long userId)
        {
            return walletRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }

        public async Task<IServiceResult<Wallet>> UpdateWallet(long credit, long userId)
        {
            //var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
            //        ? _accessor.HttpContext.User.Identity.GetUserId()
            //        : 0;
            var dbWallet = walletRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            dbWallet.Credit += credit;
            dbWallet.UpdateDate = DateTime.Now;

            await walletRepository.Update(dbWallet);
            return new ServiceResult<Wallet>().Ok(dbWallet);
        }
        public IServiceResult<bool> CheckCredit(long Value, long userId)
        {
            //var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
            //        ? _accessor.HttpContext.User.Identity.GetUserId()
            //        : 0;
            var dbWallet = walletRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            var result = dbWallet.Credit >= Value ? true : false;
            return new ServiceResult<bool>().Ok(result);
        }
        public IServiceResult<long> GetCredit()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbWallet = walletRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            if (dbWallet == null)
            {
                var newInstance = new Wallet() { Credit = 0, IsDeleted = false, UpdateDate = DateTime.Now, UserId = userId, WalletId  = 0 };
                dbWallet =walletRepository.Insert(newInstance);
            }
            return new ServiceResult<long>().Ok(dbWallet.Credit);
        }
    }
}
