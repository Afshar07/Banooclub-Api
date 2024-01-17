using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.DiscountServices
{
    public interface IDiscountService
    {
        Task<object> Create(DiscountDto inputDto);
        Task<Discount> Update(Discount item);
        Task Delete(long id);
        Task<object> GetAll(int pageNumber, int count);
        Task<Discount> Get(long id);
        Task<object> GetAllRefraction(int pageNumber, int count);
        Task<object> GetLatestDiscount(string searchCommand, byte count, short pageNumber);
    }
}