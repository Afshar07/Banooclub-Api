using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.OrderItemServices
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IBanooClubEFRepository<OrderItem> orderItemRepository;

        public OrderItemService(IBanooClubEFRepository<OrderItem> orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }
        public async Task Create(OrderItem inputDto)
        {
            orderItemRepository.Insert(inputDto);
        }

        public async Task<OrderItem> Update(OrderItem item)
        {
            await orderItemRepository.Update(item);
            return item;
        }

        public async Task<List<OrderItem>> GetAll()
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            orderItems = orderItemRepository.GetQuery().ToList();
            return orderItems;
        }

        public async Task Delete(long id)
        {
            var orderItem = orderItemRepository.GetQuery().FirstOrDefault(z => z.OrderItemId == id);
            await orderItemRepository.Delete(orderItem);
        }

        public async Task<OrderItem> Get(long id)
        {
            var orderItem = orderItemRepository.GetQuery().FirstOrDefault(z => z.OrderItemId == id);
            return orderItem;
        }
    }
}
