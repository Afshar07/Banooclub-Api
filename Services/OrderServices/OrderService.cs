using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Models.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private readonly IBanooClubEFRepository<ConsultantUserSchedule> _consultantUserSchedule;
        private readonly IHttpContextAccessor _accessor;

        public OrderService(IBanooClubEFRepository<Order> orderRepository, IHttpContextAccessor accessor, IBanooClubEFRepository<SocialMedia> mediaRepository,
            IBanooClubEFRepository<OrderItem> orderItemRepository, IBanooClubEFRepository<User> userRepository,
            IBanooClubEFRepository<Ads> adsRepository, IBanooClubEFRepository<Payment> paymentRepository,
            IBanooClubEFRepository<ServicePack> servicePackRepository,
            IBanooClubEFRepository<ConsultantUserSchedule> consultantUserSchedule)
        {
            this.orderRepository = orderRepository;
            _accessor = accessor;
            this.orderItemRepository = orderItemRepository;
            this.adsRepository = adsRepository;
            this.servicePackRepository = servicePackRepository;
            this.paymentRepository = paymentRepository;
            this.userRepository = userRepository;
            this.mediaRepository = mediaRepository;
            _consultantUserSchedule = consultantUserSchedule;
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
            var order = await orderRepository.GetQuery().FirstOrDefaultAsync(z => z.OrderId == id);
            order.SubOrders = new List<OrderItem>();
            var SubOrders = await orderItemRepository.GetQuery().Where(z => z.OrderId == id).ToListAsync();

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
            catch
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

        public async Task<object> GetMyCansultants(Models.DTO.MyConsultantUserScheduleDTO input)
        {
            input = input ?? new Models.DTO.MyConsultantUserScheduleDTO();
            if (input.pageNumber == null || input.pageNumber <= 0)
                input.pageNumber = 1;
            if (input.take == null || input.take <= 0)
                input.take = 10;

            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;

            var quiryResult = _consultantUserSchedule
                .GetQuery()
                .Where(t => t.ConsultantId > 0 && t.Order.UserId == userId && t.Consultant != null);

            if (!string.IsNullOrEmpty(input.fullname))
                quiryResult = quiryResult.Where(t => (t.Consultant.User.Name + " " + t.Consultant.User.FamilyName).Contains(input.fullname));
            if (input.id != null && input.id > 0)
                quiryResult = quiryResult.Where(t => t.ConsultantUserScheduleId == input.id);


            var total = await quiryResult.CountAsync();
            quiryResult = quiryResult.OrderByDescending(t => t.ConsultantUserScheduleId).Skip((input.pageNumber.Value - 1) * input.take.Value).Take(input.take.Value);

            var data = (await
                quiryResult
                .Select(t => new
                {
                    id = t.ConsultantUserScheduleId,
                    consultantId = t.ConsultantId,
                    orderId = t.OrderId,
                    name = t.Consultant.User.Name,
                    lName = t.Consultant.User.FamilyName,
                    userName = t.Consultant.User.UserName,
                    type = t.Order.OrderItems.Select(tt => tt.ConsultType).FirstOrDefault(),
                    createDate = t.Order.CreateDate,
                    price = t.Order.SumPrice,
                    status = t.Status,
                    description = t.Description,
                    isPayed = t.IsPayed,
                    userPic = mediaRepository.GetQuery().Where(z => z.ObjectId == t.ConsultantId && z.Type == MediaTypes.ConsultationProfileImage).Select(t => t.PictureUrl).FirstOrDefault(),
                    cats = t.Consultant.Categories.Select(tt => tt.ConsultCategory.Title).ToList(),
                    targetDate = t.TargetDate,
                    hour = t.ReserveTime,
                    consulterUserId = t.Consultant.UserId,
                    hasComment = t.ConsultantUserScheduleRatings.Any()
                })
                .ToListAsync()
                )
                .Select(t => new
                {
                    t.id,
                    t.consultantId,
                    t.orderId,
                    t.name,
                    t.lName,
                    t.type,
                    t.createDate,
                    t.price,
                    t.status,
                    t.description,
                    t.isPayed,
                    userPic = !string.IsNullOrEmpty(t.userPic) ? ("Media/Gallery/ConsultationUserProfile/" + t.userPic) : "",
                    cats = t.cats != null ? string.Join(',', t.cats) : "",
                    t.targetDate,
                    t.hour,
                    t.consulterUserId,
                    canComment = t.hasComment == false && t.isPayed == true && t.status == ConsultantUserScheduleStatus.Finished,
                    t.userName
                })
                .ToList<object>();
            return new PageModel<object>(input.pageNumber.Value, input.take.Value, total, data);
        }

        public async Task<object> GetMyCansultantsForConsulter(Models.DTO.MyConsultantUserScheduleDTO input)
        {
            input = input ?? new Models.DTO.MyConsultantUserScheduleDTO();
            if (input.pageNumber == null || input.pageNumber <= 0)
                input.pageNumber = 1;
            if (input.take == null || input.take <= 0)
                input.take = 10;

            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;

            var quiryResult = _consultantUserSchedule
                .GetQuery()
                .Where(t => t.ConsultantId > 0 && t.Consultant != null && t.Order.IsPayed == true && t.Consultant.UserId == userId);

            if (!string.IsNullOrEmpty(input.fullname))
                quiryResult = quiryResult.Where(t => (t.Order.User.Name + " " + t.Order.User.FamilyName).Contains(input.fullname));
            if (input.id != null && input.id > 0)
                quiryResult = quiryResult.Where(t => t.ConsultantUserScheduleId == input.id);

            var total = await quiryResult.CountAsync();
            quiryResult = quiryResult.OrderByDescending(t => t.ConsultantUserScheduleId).Skip((input.pageNumber.Value - 1) * input.take.Value).Take(input.take.Value);
            var data = (await
                quiryResult
                .Select(t => new
                {
                    id = t.ConsultantUserScheduleId,
                    name = t.Order.User.Name,
                    consultantId = t.ConsultantId,
                    lName = t.Order.User.FamilyName,
                    userName = t.Order.User.UserName,
                    type = t.Order.OrderItems.Select(tt => tt.ConsultType).FirstOrDefault(),
                    createDate = t.Order.CreateDate,
                    price = t.Order.SumPrice,
                    status = t.Status,
                    description = t.Description,
                    isPayed = t.IsPayed,
                    userPic = mediaRepository.GetQuery().Where(z => z.ObjectId == t.Consultant.UserId && z.Type == MediaTypes.Profile).Select(t => t.PictureUrl).FirstOrDefault(),
                    targetDate = t.TargetDate,
                    hour = t.ReserveTime,
                    t.Order.UserId
                })
                .ToListAsync())
                 .Select(t => new
                 {
                     t.id,
                     t.consultantId,
                     t.name,
                     t.lName,
                     t.type,
                     t.createDate,
                     t.price,
                     t.status,
                     t.description,
                     t.isPayed,
                     userPic = !string.IsNullOrEmpty(t.userPic) ? ("Media/Gallery/Profile/" + t.userPic) : "",
                     t.targetDate,
                     t.hour,
                     pationUserId = t.UserId,
                     t.userName
                 })
                .ToList<object>();

            return new PageModel<object>(input.pageNumber.Value, input.take.Value, total, data);
        }
    }
}
