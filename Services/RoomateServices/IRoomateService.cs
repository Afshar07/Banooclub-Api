using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.RoomateServices
{
    public interface IRoomateService
    {
        Task Create(Roomate inputDto);
        Task<Roomate> Update(Roomate item);
        Task Delete(long id);
        Task<List<Roomate>> GetAll(long? mortgageFrom, long? mortgageTo, long? dailyRentFrom, long? dailyRentTo);
        Task<List<Roomate>> GetAllForAdmin(long? mortgageFrom, long? mortgageTo, long? dailyRentFrom, long? dailyRentTo);
        Task<object> Get(long id);
        Task<object> GetByUserId(long userId);
        Task<object> GetMyRoomate();
        int CheckAvailablityForActivation();
        Task<object> ChangeRoomateActivation(bool state);
    }
}
