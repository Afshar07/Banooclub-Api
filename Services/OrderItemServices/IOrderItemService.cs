using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.OrderItemServices
{
    public interface IOrderItemService
    {
        Task Create(OrderItem inputDto);
        Task<OrderItem> Update(OrderItem item);
        Task Delete(long id);
        Task<List<OrderItem>> GetAll();
        Task<OrderItem> Get(long id);
    }
}
