using BanooClub.Models;
using BanooClub.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.WalletServices
{
    public interface IWalletService
    {
        Task<long> Create(Wallet inputDto);
        Task<Wallet> Update(Wallet item);
        object GetAll(int pageNumber, int count);
        Task<bool> Delete(long id);
        Task<Wallet> Get(long id);
        List<Wallet> GetByUserId(long userId);
        Task<IServiceResult<Wallet>> UpdateWallet(long credit, long userId);
        IServiceResult<bool> CheckCredit(long Value, long userId);
        IServiceResult<long> GetCredit();
    }
}
