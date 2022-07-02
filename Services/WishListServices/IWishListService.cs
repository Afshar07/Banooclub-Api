using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.WishListServices
{
    public interface IWishListService
    {
        Task Create(WishList inputDto);
        Task<WishList> Update(WishList item);
        Task Delete(long id);
        Task<List<WishList>> GetAll();
        Task<WishList> Get(long id);
        List<WishList> GetByUserId(long userId);
    }
}
