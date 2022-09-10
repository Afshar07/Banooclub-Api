using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.SmsServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BanooClub.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IBanooClubEFRepository<Payment> paymentRepository;
        private readonly IBanooClubEFRepository<User> userRepository;
        private readonly IBanooClubEFRepository<Order> orderRepository;
        private readonly IBanooClubEFRepository<OrderItem> orderItemRepository;
        private readonly IBanooClubEFRepository<Wallet> walletRepository;
        private readonly IBanooClubEFRepository<ServicePlan> servicePlanRepository;
        private readonly IBanooClubEFRepository<ServicePack> servicePackRepository;
        private readonly IBanooClubEFRepository<Plan> planRepository;
        private readonly IBanooClubEFRepository<Ads> adsRepository;
        private readonly ISmsSenderService _smsSenderService;
        private readonly IHttpContextAccessor _accessor;
        const string ApiKey = "3959a1e0-eb38-465f-8549-db88fb7c2e89";

        public PaymentService(IBanooClubEFRepository<Payment> paymentRepository, IBanooClubEFRepository<Ads> adsRepository, IBanooClubEFRepository<Plan> planRepository, IBanooClubEFRepository<Order> orderRepository, IBanooClubEFRepository<ServicePlan> servicePlanRepository
            , IHttpContextAccessor accessor, IBanooClubEFRepository<User> userRepository, IBanooClubEFRepository<Wallet> walletRepository
            , IBanooClubEFRepository<OrderItem> orderItemRepository, ISmsSenderService smsSenderService, IBanooClubEFRepository<ServicePack> servicePackRepository)
        {
            this.paymentRepository = paymentRepository;
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.walletRepository = walletRepository;
            this.servicePlanRepository = servicePlanRepository;
            this.servicePackRepository = servicePackRepository;
            this.adsRepository = adsRepository;
            this.planRepository = planRepository;
            _smsSenderService = smsSenderService;
            _accessor = accessor;
        }
        public async Task<object> Create(Payment inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var mobile = "";
            if (userId != 0)
            {
                inputDto.UserId = userId;
                var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
                mobile = dbUser != null ? dbUser.Mobile : mobile;
            }

            inputDto.CreateDate = System.DateTime.Now;
            inputDto.IsDeleted = false;
            inputDto.PaymentId = 0;

            #region GetAuthority NextPay

            var _client = new HttpClient();
            NextPayModel verifyModel = new NextPayModel()
            {
                amount = inputDto.Amount,
                api_key = ApiKey,
                callback_uri = "https://banooclub.simagar.com/payments/ResponseVerify",
                //callback_uri = "https://localhost:44330/Payment/ChangePaymentStatus",
                currency = "IRT",
                customer_phone = mobile,
                order_id = inputDto.OrderId.ToString()
            };
            string jsonComment = JsonConvert.SerializeObject(verifyModel);

            StringContent content = new StringContent(jsonComment, Encoding.UTF8, "application/json");

            var res = await _client.PostAsync("https://nextpay.org/nx/gateway/token", content);

            var result = res.Content.ReadAsStringAsync().Result;
            NextPayVerifyResponse model = JsonConvert.DeserializeObject<NextPayVerifyResponse>(result);
            var endPoint = $"https://nextpay.org/nx/gateway/payment/";
            if (model.code.Equals(-1))
            {
                inputDto.TransId = model.trans_id;
                endPoint = $"https://nextpay.org/nx/gateway/payment/{model.trans_id}";
            }
            inputDto.Status = model.code;
            #endregion
            var creation = paymentRepository.Insert(inputDto);

            return endPoint;
        }

        public async Task<Payment> Update(Payment item)
        {
            await paymentRepository.Update(item);
            return item;
        }

        public async Task<object> GetAll(int pageNumber, int count, string searchCommand)
        {
            List<Payment> payments = new List<Payment>();
            if (searchCommand != null && searchCommand != "")
            {
                var dbUsersIds = userRepository.GetQuery().Select(x => x.UserId).ToList();
                payments = paymentRepository.GetQuery().Where(z => dbUsersIds.Contains(z.UserId)).ToList();
            }
            else
            {
                payments = paymentRepository.GetQuery().ToList();
            }
            var paymentsCount = payments.Count();

            if (pageNumber != 0 && count != 0)
            {
                payments = payments.OrderByDescending(z => z.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            foreach (var payment in payments)
            {
                payment.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == payment.UserId);

                var order = orderRepository.GetQuery().FirstOrDefault(z => z.OrderId == payment.OrderId);
                var SubOrders = orderItemRepository.GetQuery().Where(z => z.OrderId == order.OrderId).ToList();
                order.SubOrders = SubOrders;

                payment.OrderInfo = order;

            }

            var obj = new
            {
                Payments = payments,
                PaymentsCount = paymentsCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var payment = paymentRepository.GetQuery().FirstOrDefault(z => z.PaymentId == id);
            await paymentRepository.Delete(payment);
        }

        public async Task<Payment> Get(long id)
        {
            var payment = paymentRepository.GetQuery().FirstOrDefault(z => z.PaymentId == id);
            return payment;
        }

        public async Task<int> PayByWallet(long orderId)
        {
            var dbOrder = orderRepository.GetQuery().FirstOrDefault(z => z.OrderId == orderId);
            var dbWallet = walletRepository.GetQuery().FirstOrDefault(z => z.UserId == dbOrder.UserId);

            if (dbWallet != null && dbWallet.Credit > dbOrder.SumPrice && dbOrder.Status == OrderStatus.Submit)
            {
                
                var dbOrderItems = orderItemRepository.GetQuery().Where(z => z.OrderId == dbOrder.OrderId).ToList();
                if (dbOrder.ServiceId != null && dbOrder.ServiceId > 0)
                {
                    foreach (var item in dbOrderItems)
                    {
                        servicePlanRepository.Insert(new ServicePlan() { CreateDate = DateTime.Now, IsDeleted = false, PlanId =item.PlanId, ObjectId = (long)dbOrder.ServiceId, Type = ServicePlanType.Service, ServicePlanId = 0 });
                    }
                }
                else if(dbOrder.AdsId != null && dbOrder.AdsId != 0)
                {
                    foreach (var item in dbOrderItems)
                    {
                        servicePlanRepository.Insert(new ServicePlan() { CreateDate = DateTime.Now, IsDeleted = false, PlanId =item.PlanId, ObjectId = (long)dbOrder.AdsId, Type = ServicePlanType.Ads, ServicePlanId = 0 });
                    }
                }
                else if((dbOrder.AdsId == null || dbOrder.AdsId == 0) && (dbOrder.ServiceId == null || dbOrder.ServiceId == 0))
                {
                    foreach(var item in dbOrderItems)
                    {
                        var dbService = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == item.ServiceId);
                        if(dbService != null )
                        {
                            dbService.Maintain -= item.Count;
                            if(dbService.Maintain < 0)
                            {
                                return 0;
                            }
                            await servicePackRepository.Update(dbService);
                        }
                    } 
                }
                dbWallet.Credit -= dbOrder.SumPrice;
                await walletRepository.Update(dbWallet);
                dbOrder.Status = OrderStatus.Payed;
                dbOrder.IsPayed = true;
                await orderRepository.Update(dbOrder);
            }
            else
            {
                return 0;
            }

            return 1;
        }

        public async Task<int> ChangePaymentStatus(string orderId, string transId)
        {
            var dbPayment = paymentRepository.GetQuery().FirstOrDefault(z => z.TransId == transId && z.OrderId == Convert.ToInt64(orderId));
            CheckPay checkModel = new CheckPay()
            {
                amount = dbPayment.Amount,
                api_key = ApiKey,
                currency = "IRT",
                trans_id = dbPayment.TransId,
            };
            var _client = new HttpClient();
            string jsonComment = JsonConvert.SerializeObject(checkModel);

            StringContent content = new StringContent(jsonComment, Encoding.UTF8, "application/json");

            var res = await _client.PostAsync("https://nextpay.org/nx/gateway/verify", content);

            var result = res.Content.ReadAsStringAsync().Result;
            VerifyCheckPay model = JsonConvert.DeserializeObject<VerifyCheckPay>(result);
            var dbOrder = orderRepository.GetQuery().FirstOrDefault(z => z.OrderId == dbPayment.OrderId);
            if (model.code == 0 && dbOrder.Status == OrderStatus.Submit)
            {
                dbOrder.Status = Models.Enums.OrderStatus.Payed;
                dbOrder.IsPayed = true;
                await orderRepository.Update(dbOrder);

                var dbSubOrders = orderItemRepository.GetQuery().Where(z => z.OrderId == dbOrder.OrderId).ToList();
                if (dbPayment.WalletCharge != null && dbPayment.WalletCharge == true)
                {
                    var dbWallet = walletRepository.GetQuery().FirstOrDefault(z => z.UserId == dbPayment.UserId);
                    if (dbWallet != null)
                    {
                        dbWallet.Credit += dbPayment.Amount;
                        await walletRepository.Update(dbWallet);
                    }
                }
                if ((dbOrder.ServiceId != null && dbOrder.ServiceId != 0) || (dbOrder.AdsId != null && dbOrder.AdsId != 0) && dbPayment.WalletCharge != true )
                {
                    var dbOrderItems = orderItemRepository.GetQuery().Where(z => z.OrderId == dbOrder.OrderId).ToList();
                    foreach (var item in dbOrderItems)
                    {
                        var dbPlan = planRepository.GetQuery().FirstOrDefault(z=>z.PlanId == item.PlanId);
                        if (dbPlan.Type == (int)PlanTypes.Ladder)
                        {
                            if(dbOrder.ServiceId != null && dbOrder.ServiceId != 0)
                            {
                                var dbService = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == dbOrder.ServiceId);
                                dbService.FireDate = DateTime.Now;
                                await servicePackRepository.Update(dbService);
                            }
                            if(dbOrder.AdsId != null && dbOrder.AdsId != 0)
                            {
                                var dbAds = adsRepository.GetQuery().FirstOrDefault(z=>z.AdsId == dbOrder.AdsId);
                                dbAds.FireDate = DateTime.Now;
                                await adsRepository.Update(dbAds);
                            }
                        }
                        if (dbOrder.ServiceId != null && dbOrder.ServiceId != 0)
                        {
                            servicePlanRepository.Insert(new ServicePlan() { CreateDate = DateTime.Now, IsDeleted = false, PlanId =item.PlanId,Type = ServicePlanType.Service, ObjectId = (long)dbOrder.ServiceId, ServicePlanId = 0 });
                        }
                        if (dbOrder.AdsId != null && dbOrder.AdsId != 0)
                        {
                            servicePlanRepository.Insert(new ServicePlan() { CreateDate = DateTime.Now, IsDeleted = false, PlanId =item.PlanId, Type = ServicePlanType.Ads, ObjectId = (long)dbOrder.AdsId, ServicePlanId = 0 });
                        }
                        
                    }
                }
                else if ((dbOrder.AdsId == null || dbOrder.AdsId == 0) && (dbOrder.ServiceId == null || dbOrder.ServiceId == 0) && dbPayment.WalletCharge != true)
                {
                    var dbOrderItems = orderItemRepository.GetQuery().Where(z => z.OrderId == dbOrder.OrderId).ToList();
                    foreach (var item in dbOrderItems)
                    {
                        var dbService = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == item.ServiceId);
                        if (dbService != null)
                        {
                            dbService.Maintain -= item.Count;
                            await servicePackRepository.Update(dbService);
                        }
                    }
                }

                #region Send Message
                var dbUser = userRepository.GetQuery().FirstOrDefault(z => z.UserId == dbPayment.UserId);
                if (dbUser != null && dbUser.Mobile != null)
                {
                    UltraFastSend SMSForPayment = new UltraFastSend()
                    {
                        Mobile = Convert.ToInt64(dbUser.Mobile),
                        TemplateId = 67070,
                        ParameterArray = new List<UltraFastParameters>()
                    {
                        new UltraFastParameters()
                        {
                            Parameter = "RefId" , ParameterValue = $"{model.Shaparak_Ref_Id}"
                        }
                    }.ToArray()
                    };
                    await _smsSenderService.UltraFastSend(SMSForPayment);
                }
                #endregion
            }
            dbPayment.Status = model.code;
            dbPayment.RefId = model.Shaparak_Ref_Id;
            await paymentRepository.Update(dbPayment);



            return dbPayment.Status;
        }
        public async Task<object> GetMyPayments(int pageNumber, int count)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var dbPayments = paymentRepository.GetQuery().Where(z => z.UserId == userId).ToList();
            var paymentsCount = dbPayments.Count();
            if (pageNumber != 0 && count != 0)
            {
                dbPayments = paymentRepository.GetQuery().Where(z => z.UserId == userId).Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            foreach (var pay in dbPayments)
            {
                pay.UserInfo = userRepository.GetQuery().FirstOrDefault(z => z.UserId == pay.UserId);

                var order = orderRepository.GetQuery().FirstOrDefault(z => z.OrderId == pay.OrderId);
                var SubOrders = orderItemRepository.GetQuery().Where(z => z.OrderId == order.OrderId).ToList();
                order.SubOrders = SubOrders;

                pay.OrderInfo = order;

            }
            var obj = new
            {
                Payments = dbPayments,
                PaymentsCount = paymentsCount
            };
            return obj;
        }

        public async Task<object> GetByUserId(long userId, byte count, short pageNumber)
        {
            var query = paymentRepository.GetQuery()
                .Where(x => x.UserId == userId);

            var data = new
            {
                Payments = query.OrderByDescending(x => x.CreateDate)
                .Skip((pageNumber - 1) * count)
                .Take(count).ToList(),
                PaymentCount = query.Count()
            };

            return data;
        }

        public async Task<byte> ChangePaymentStatus(long paymentId, PaymentStatus status)
        {
            var payment = paymentRepository.GetQuery()
                .FirstOrDefault(x => x.PaymentId == paymentId);

            if (payment is null)
                return 0;

            payment.PaymentStatus = status;
            await paymentRepository.Save();

            var order = orderRepository.GetQuery()
                .FirstOrDefault(x => x.OrderId == payment.OrderId);

            order.IsPayed = true;
            await orderRepository.Save();

            return 1;
        }
    }
}
