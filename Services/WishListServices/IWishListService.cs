using BanooClub.Models;
using BanooClub.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.WishListServices
{
    public interface IWishListService
    {
        Task Create(WishList inputDto);
        Task<WishList> Update(WishList item);
        Task Delete(long objectId, WishListType type);
        Task<List<WishList>> GetAll();
        Task<WishList> Get(long id);
        object GetByUserId(long userId);
        Task Toggle(long objectId, WishListType type);
        List<WishList> GetByWishList();
    }
}
