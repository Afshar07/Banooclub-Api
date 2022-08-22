using BanooClub.Models;
using BanooClub.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.OrderServices
{
    public interface IOrderService
    {
        Task<long> Create(Order inputDto);
        Task<Order> Update(Order item);
        Task Delete(long id);
        Task<object> GetAll(int pageNumber, int count, string searchCommand);
        Task<Order> Get(long id);
        List<Order> GetByUserId(short pageNumber, byte count);
        Task<bool> ChangeOrderStatus(long orderId, OrderStatus status);
    }
}
