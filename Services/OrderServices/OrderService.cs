using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IBanooClubEFRepository<Order> orderRepository;
        private readonly IBanooClubEFRepository<OrderItem> orderItemRepository;
        private readonly IBanooClubEFRepository<ServicePack> servicePackRepository;
        private readonly IBanooClubEFRepository<Ads> adsRepository;
        private readonly IBanooClubEFRepository<Payment> paymentRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly IHttpContextAccessor _accessor;

        public OrderService(IBanooClubEFRepository<Order> orderRepository, IHttpContextAccessor accessor, IBanooClubEFRepository<SocialMedia> mediaRepository,
            IBanooClubEFRepository<OrderItem> orderItemRepository, IBanooClubEFRepository<User> userRepository,
            IBanooClubEFRepository<Ads> adsRepository, IBanooClubEFRepository<Payment> paymentRepository,
            IBanooClubEFRepository<ServicePack> servicePackRepository)
        {
            this.orderRepository = orderRepository;
            _accessor = accessor;
            this.orderItemRepository = orderItemRepository;
            this.adsRepository = adsRepository;
            this.servicePackRepository = servicePackRepository;
            this.paymentRepository = paymentRepository;
            this.userRepository = userRepository;
            this.mediaRepository = mediaRepository;
        }
        public async Task<long> Create(Order inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;
            inputDto.UserId = userId;
            inputDto.Status = OrderStatus.Submit;
            inputDto.CreateDate = System.DateTime.Now;
            var dbOrderCreation = orderRepository.Insert(inputDto);
            foreach (var item in inputDto.SubOrders)
            {
                item.OrderId = dbOrderCreation.OrderId;
                orderItemRepository.Insert(item);
            }
            return dbOrderCreation.OrderId;
        }

        public async Task<Order> Update(Order item)
        {
            await orderRepository.Update(item);
            return item;
        }

        public async Task<object> GetAll(int pageNumber, int count, string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<Order> orders = new List<Order>();
            orders = orderRepository.GetQuery().Where(z => z.OrderId.ToString().Contains(searchCommand)).Skip((pageNumber - 1) * count).Take(count).ToList();
            var ordersCount = orderRepository.GetQuery().Where(z => z.OrderId.ToString().Contains(searchCommand)).Count();

            foreach (var dbOrder in orders)
            {
                if (dbOrder.ServiceId != null && dbOrder.ServiceId != 0)
                {
                    dbOrder.ServiceInfo = servicePackRepository.GetQuery().FirstOrDefault(x => x.ServicePackId == dbOrder.ServiceId);
                }
                if (dbOrder.AdsId != null && dbOrder.AdsId != 0)
                {
                    dbOrder.AdsInfo = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == dbOrder.AdsId);
                }

                var SubOrders = orderItemRepository.GetQuery().Where(z => z.OrderId == dbOrder.OrderId).ToList();
                dbOrder.SubOrders = SubOrders;

                dbOrder.UserInfo = userRepository.GetQuery()
                    .FirstOrDefault(x => x.UserId == dbOrder.UserId);
            }

            var obj = new
            {
                Orders = orders,
                OrdersCount = ordersCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var order = orderRepository.GetQuery().FirstOrDefault(z => z.OrderId == id);
            await orderRepository.Delete(order);
        }

        public async Task<Order> Get(long id)
        {
            var order = orderRepository.GetQuery().FirstOrDefault(z => z.OrderId == id);
            order.SubOrders = new List<OrderItem>();
            var SubOrders = orderItemRepository.GetQuery().Where(z => z.OrderId == id).ToList();

            order.SubOrders = SubOrders;
            foreach (var sub in order.SubOrders)
            {
                if (sub.ServiceId != null)
                {
                    sub.ServiceInfo = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == sub.ServiceId);
                    if (sub.ServiceInfo != null)
                    {
                        sub.ServiceInfo.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == sub.ServiceInfo.UserId);
                        var dbVendorMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == sub.ServiceInfo.UserId && z.Type == MediaTypes.Profile);
                        if (dbVendorMedia != null)
                        {
                            sub.ServiceInfo.UserInfo.SelfieFileData = dbVendorMedia != null ? dbVendorMedia.PictureUrl : "";
                        }
                        var ServiceMedias = mediaRepository.GetQuery().Where(z => z.ObjectId == sub.ServiceInfo.ServicePackId && z.Type == MediaTypes.Service).ToList();
                        sub.ServiceInfo.Medias = new List<FileData>();
                        foreach (var item in ServiceMedias)
                        {
                            var data = new FileData()
                            {
                                Base64 = item.PictureUrl,
                                Priority = item.Priority,
                                UploadType = 1
                            };
                            sub.ServiceInfo.Medias.Add(data);
                        }
                    }
                }
            }

            order.PaymentInfo = paymentRepository.GetQuery().OrderByDescending(x => x.CreateDate).FirstOrDefault(z => z.OrderId == order.OrderId);

            return order;
        }

        public object GetByUserId(short pageNumber, byte count)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;

            var dbOrders = orderRepository.GetQuery()
                .Where(z => z.UserId == userId)
                .Skip((pageNumber - 1) * count)
                .Take(count)
                .ToList();

            foreach (var dbOrder in dbOrders)
            {
                if (dbOrder.ServiceId != null && dbOrder.ServiceId != 0)
                {
                    dbOrder.ServiceInfo = servicePackRepository.GetQuery().FirstOrDefault(x => x.ServicePackId == dbOrder.ServiceId);
                }
                if (dbOrder.AdsId != null && dbOrder.AdsId != 0)
                {
                    dbOrder.AdsInfo = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == dbOrder.AdsId);
                }

                var SubOrders = orderItemRepository.GetQuery().Where(z => z.OrderId == dbOrder.OrderId).ToList();
                dbOrder.SubOrders = SubOrders;

                dbOrder.UserInfo = userRepository.GetQuery()
                    .FirstOrDefault(x => x.UserId == dbOrder.UserId);
            }

            var data = new
            {
                Orders = dbOrders,
                OrderCount = orderRepository.GetQuery()
                .Where(z => z.UserId == userId).Count()
            };

            return data;
        }

        public async Task<bool> ChangeOrderStatus(long orderId, OrderStatus status)
        {
            try
            {
                var dbOrder = orderRepository.GetQuery().FirstOrDefault(z => z.OrderId == orderId);
                dbOrder.Status = status;
                if (status == OrderStatus.Payed)
                {
                    dbOrder.IsPayed = true;
                }
                await orderRepository.Update(dbOrder);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object GetByUserIdForAdmin(short pageNumber, byte count, long userId)
        {
            var dbOrders = orderRepository.GetQuery()
                .Where(z => z.UserId == userId)
                .Skip((pageNumber - 1) * count)
                .Take(count)
                .ToList();

            foreach (var dbOrder in dbOrders)
            {
                if (dbOrder.ServiceId != null && dbOrder.ServiceId != 0)
                {
                    dbOrder.ServiceInfo = servicePackRepository.GetQuery().FirstOrDefault(x => x.ServicePackId == dbOrder.ServiceId);
                }
                if (dbOrder.AdsId != null && dbOrder.AdsId != 0)
                {
                    dbOrder.AdsInfo = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == dbOrder.AdsId);
                }

                var SubOrders = orderItemRepository.GetQuery().Where(z => z.OrderId == dbOrder.OrderId).ToList();
                dbOrder.SubOrders = SubOrders;
            }

            var data = new
            {
                Orders = dbOrders,
                OrderCount = orderRepository.GetQuery()
                .Where(z => z.UserId == userId).Count()
            };

            return data;
        }

        public async Task<object> GetMyCansultants()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;

            return await orderItemRepository
                .GetQuery()
                .Where(t => t.ConsultantId != null && t.ConsultantId > 0 && t.Order.UserId == userId && t.Consultant != null)
                .Select(t => new 
                {
                    id = t.OrderItemId,
                    consultantId = t.ConsultantId,
                    orderId = t.OrderId,
                    name = t.Consultant.User.Name,
                    lName = t.Consultant.User.FamilyName,
                    type = t.ConsultType,
                    createDate = t.Order.CreateDate,
                    price = t.Price,
                    status = t.Order.User.ConsultantUserSchedules.Where(tt => tt.ConsultantId == t.ConsultantId).Select(tt => tt.Status).FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<object> GetMyCansultantsForConsulter()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;

            return await orderItemRepository
                .GetQuery()
                .Where(t => t.ConsultantId != null && t.ConsultantId > 0 && t.Consultant != null && t.Order.IsPayed == true && t.Consultant.UserId == userId)
                .Select(t => new
                {
                    id = t.OrderItemId,
                    name = t.Order.User.Name,
                    consultantId = t.ConsultantId,
                    lName = t.Order.User.FamilyName,
                    type = t.ConsultType,
                    createDate = t.Order.CreateDate,
                    price = t.Price,
                    status = t.Order.User.ConsultantUserSchedules.Where(tt => tt.ConsultantId == t.ConsultantId).Select(tt => tt.Status).FirstOrDefault()
                })
                .ToListAsync();
        }
    }
}
