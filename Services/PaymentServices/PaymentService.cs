using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using BanooClub.Services.SmsServices;
using BanooClub.Utilities;
using Infrastructure;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly IBanooClubEFRepository<ConsultantUserSchedule> _consultantUserScheduleRepository;
        private readonly ISmsSenderService _smsSenderService;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<PaymentService> _paymentServiceLogger;
        const string ApiKey = "3959a1e0-eb38-465f-8549-db88fb7c2e89";

        public PaymentService
            (
                IBanooClubEFRepository<Payment> paymentRepository, IBanooClubEFRepository<Ads> adsRepository, IBanooClubEFRepository<Plan> planRepository,
                IBanooClubEFRepository<Order> orderRepository, IBanooClubEFRepository<ServicePlan> servicePlanRepository,
                IHttpContextAccessor accessor, IBanooClubEFRepository<User> userRepository, IBanooClubEFRepository<Wallet> walletRepository,
                IBanooClubEFRepository<OrderItem> orderItemRepository, ISmsSenderService smsSenderService, IBanooClubEFRepository<ServicePack> servicePackRepository,
                ILogger<PaymentService> paymentServiceLogger, IBanooClubEFRepository<ConsultantUserSchedule> consultantUserScheduleRepository
            )
        {
            this.paymentRepository = paymentRepository;
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.walletRepository = walletRepository;
            this.servicePlanRepository = servicePlanRepository;
            this.servicePackRepository = servicePackRepository;
            this.adsRepository = adsRepository;
            _consultantUserScheduleRepository = consultantUserScheduleRepository;
            this.planRepository = planRepository;
            _smsSenderService = smsSenderService;
            _paymentServiceLogger = paymentServiceLogger;
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
                //callback_uri = "https://banoclub.com/payments/ResponseVerify",
                callback_uri = "https://localhost:44330/Payment/ChangePaymentStatus",
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
                        servicePlanRepository.Insert(new ServicePlan() { CreateDate = DateTime.Now, IsDeleted = false, PlanId = item.PlanId, ObjectId = (long)dbOrder.ServiceId, Type = ServicePlanType.Service, ServicePlanId = 0 });
                    }
                }
                else if (dbOrder.AdsId != null && dbOrder.AdsId != 0)
                {
                    foreach (var item in dbOrderItems)
                    {
                        servicePlanRepository.Insert(new ServicePlan() { CreateDate = DateTime.Now, IsDeleted = false, PlanId = item.PlanId, ObjectId = (long)dbOrder.AdsId, Type = ServicePlanType.Ads, ServicePlanId = 0 });
                    }
                }
                else if ((dbOrder.AdsId == null || dbOrder.AdsId == 0) && (dbOrder.ServiceId == null || dbOrder.ServiceId == 0))
                {
                    foreach (var item in dbOrderItems)
                    {
                        var dbService = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == item.ServiceId);
                        if (dbService != null)
                        {
                            dbService.Maintain -= item.Count;
                            if (dbService.Maintain < 0)
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

        public async Task<int> ChangePaymentStatus(string paymentId, string transId)
        {
            if (!string.IsNullOrEmpty(paymentId) && !string.IsNullOrEmpty(transId) && paymentId.ToLongReturnZiro() > 0)
            {
                long paymentIdLong = paymentId.ToLongReturnZiro();
                var dbPayment = paymentRepository
                    .GetQuery()
                    .FirstOrDefault(z => z.TransId == transId && z.PaymentId == paymentIdLong && z.PaymentStatus == PaymentStatus.Processing);
                if (dbPayment != null)
                {
                    var dbOrder = orderRepository
                       .GetQuery()
                       .Include(t => t.OrderItems)
                       .FirstOrDefault(z => z.OrderId == dbPayment.OrderId);

                    if (dbOrder != null && dbOrder.OrderItems != null)
                    {
                        var foundConsultId = dbOrder.OrderItems.Select(t => t.ConsultantId).FirstOrDefault();
                        if (foundConsultId != null && foundConsultId.Value > 0)
                        {
                            var foundSchadualUser = await _consultantUserScheduleRepository
                                                    .GetQuery()
                                                    .Where(t => t.OrderId == dbOrder.OrderId && t.UserId == dbOrder.UserId && t.ConsultantId == foundConsultId)
                                                    .FirstOrDefaultAsync();
                            if (foundSchadualUser != null)
                            {
                                var createDateT = foundSchadualUser.TargetDate;
                                if (_consultantUserScheduleRepository
                                    .GetQuery()
                                    .Any
                                    (t => 
                                        t.ReserveTime == foundSchadualUser.ReserveTime && t.IsPayed == true && 
                                        t.CreateDate.Year == createDateT.Year && t.CreateDate.Month  == createDateT.Month && t.CreateDate.Day == createDateT.Day)
                                    )
                                {
                                    return -99;
                                }
                            }
                        }
                    }

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

                    _paymentServiceLogger.LogInformation("payment verification start request for verification orderId: {orderId}, transId: {transId}", paymentId, transId);

                    var res = await _client.PostAsync("https://nextpay.org/nx/gateway/verify", content);

                    var result = await res.Content.ReadAsStringAsync();
                    VerifyCheckPay model = JsonConvert.DeserializeObject<VerifyCheckPay>(result);


                    if (dbOrder != null)
                    {
                        if (model.code == 0 && dbOrder.Status == OrderStatus.Submit)
                        {
                            try
                            {
                                if (dbOrder.OrderItems != null)
                                {
                                    var foundConsultId = dbOrder.OrderItems.Select(t => t.ConsultantId).FirstOrDefault();
                                    if (foundConsultId != null && foundConsultId.Value > 0)
                                    {
                                        var foundSchadualUser = await _consultantUserScheduleRepository
                                                    .GetQuery()
                                                    .Where(t => t.OrderId == dbOrder.OrderId && t.UserId == dbOrder.UserId && t.ConsultantId == foundConsultId)
                                                    .FirstOrDefaultAsync();
                                        if (foundSchadualUser != null)
                                        {
                                            foundSchadualUser.IsPayed = true;
                                            await _consultantUserScheduleRepository.Update(foundSchadualUser);
                                            _paymentServiceLogger.LogInformation("success payment update user schedual. orderId: {orderId}, ConsultantUserScheduleId: {ConsultantUserScheduleId}", paymentId, foundSchadualUser.ConsultantUserScheduleId);
                                        }
                                    }
                                }

                                dbOrder.Status = OrderStatus.Payed;
                                dbOrder.IsPayed = true;
                                _paymentServiceLogger.LogInformation("success payment update order. orderId: {orderId}", paymentId);
                                await orderRepository.Update(dbOrder);




                                dbPayment.Status = model.code;
                                dbPayment.PaymentStatus = PaymentStatus.Payed;
                                dbPayment.RefId = model.Shaparak_Ref_Id;
                                _paymentServiceLogger.LogInformation("success payment update payment. paymentId: {paymentId}", dbPayment?.PaymentId);
                                await paymentRepository.Update(dbPayment);

                                if (dbPayment.WalletCharge != null && dbPayment.WalletCharge == true)
                                {
                                    var dbWallet = walletRepository.GetQuery().FirstOrDefault(z => z.UserId == dbPayment.UserId);
                                    if (dbWallet != null)
                                    {
                                        dbWallet.Credit += dbPayment.Amount;
                                        _paymentServiceLogger.LogInformation("success payment update wallet. paymentId: {paymentId}", dbPayment?.PaymentId);
                                        await walletRepository.Update(dbWallet);
                                    }
                                }
                                if ((dbOrder.ServiceId != null && dbOrder.ServiceId != 0) || (dbOrder.AdsId != null && dbOrder.AdsId != 0) && dbPayment.WalletCharge != true)
                                {
                                    var dbOrderItems = orderItemRepository.GetQuery().Where(z => z.OrderId == dbOrder.OrderId).ToList();
                                    foreach (var item in dbOrderItems)
                                    {
                                        var dbPlan = planRepository.GetQuery().FirstOrDefault(z => z.PlanId == item.PlanId);
                                        if (dbPlan.Type == (int)PlanTypes.Ladder)
                                        {
                                            if (dbOrder.ServiceId != null && dbOrder.ServiceId != 0)
                                            {
                                                var dbService = servicePackRepository.GetQuery().FirstOrDefault(z => z.ServicePackId == dbOrder.ServiceId);
                                                dbService.FireDate = DateTime.Now;
                                                _paymentServiceLogger.LogInformation("success payment update service. paymentId: {paymentId}", dbPayment?.PaymentId);
                                                await servicePackRepository.Update(dbService);
                                            }
                                            if (dbOrder.AdsId != null && dbOrder.AdsId != 0)
                                            {
                                                var dbAds = adsRepository.GetQuery().FirstOrDefault(z => z.AdsId == dbOrder.AdsId);
                                                dbAds.FireDate = DateTime.Now;
                                                _paymentServiceLogger.LogInformation("success payment update ads. paymentId: {paymentId}", dbPayment?.PaymentId);
                                                await adsRepository.Update(dbAds);
                                            }
                                        }
                                        if (dbOrder.ServiceId != null && dbOrder.ServiceId != 0)
                                        {
                                            _paymentServiceLogger.LogInformation("success payment insert service plan. paymentId: {paymentId}", dbPayment?.PaymentId);
                                            servicePlanRepository.Insert(new ServicePlan() { CreateDate = DateTime.Now, IsDeleted = false, PlanId = item.PlanId, Type = ServicePlanType.Service, ObjectId = (long)dbOrder.ServiceId, ServicePlanId = 0 });
                                        }
                                        if (dbOrder.AdsId != null && dbOrder.AdsId != 0)
                                        {
                                            _paymentServiceLogger.LogInformation("success payment insert service plan 2. paymentId: {paymentId}", dbPayment?.PaymentId);
                                            servicePlanRepository.Insert(new ServicePlan() { CreateDate = DateTime.Now, IsDeleted = false, PlanId = item.PlanId, Type = ServicePlanType.Ads, ObjectId = (long)dbOrder.AdsId, ServicePlanId = 0 });
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
                                            _paymentServiceLogger.LogInformation("success payment update service pack. paymentId: {paymentId}", dbPayment?.PaymentId);
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
                                    _paymentServiceLogger.LogInformation("success payment send sms. paymentId: {paymentId}", dbPayment?.PaymentId);
                                    await _smsSenderService.UltraFastSend(SMSForPayment);
                                }
                                #endregion

                                return dbPayment.Status;
                            }
                            catch (Exception ex)
                            {
                                _paymentServiceLogger.LogInformation("success payment failed error : {error}, inner error: {innerError}, orderId: {orderId}", ex?.Message, ex?.InnerException?.Message, paymentId);
                            }

                        }
                        else
                        {
                            _paymentServiceLogger.LogInformation("failed payment verificationCode: {code}, orderStatus: {orderStatus}, orderId: {orderId}, transId: {transId}", model?.code, dbOrder?.Status, paymentId, transId);
                        }

                    }
                    else
                    {
                        _paymentServiceLogger.LogInformation("failed payment order not found orderId: {orderId}, transId: {transId}", paymentId, transId);
                    }
                }
                else
                {
                    _paymentServiceLogger.LogInformation("failed payment verification payment not found orderId: {orderId}, transId: {transId}", paymentId, transId);
                }

            }
            _paymentServiceLogger.LogInformation("failed payment verification orderId: {orderId}, transId: {transId}", paymentId, transId);
            return -1;
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

        public async Task<object> CreateByOrder(long? orderId)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;
            var foundOrder = await orderRepository
                .GetQuery()
                .Where(t => t.OrderId == orderId && t.UserId == userId)
                .Select(t => new
                {
                    t.Description,
                    t.OrderId,
                    t.SumPrice,
                    t.UserId,
                    t.User.Name,
                    t.User.FamilyName,
                    t.User.Mobile,
                    hasAnyConsult = t.OrderItems.Any(tt => tt.ConsultantId != null)
                })
                .FirstOrDefaultAsync();

            if (foundOrder != null)
            {
                Payment newItem = new Payment()
                {
                    Amount = foundOrder.SumPrice,
                    OrderId = foundOrder.OrderId,
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    Description = !string.IsNullOrEmpty(foundOrder.Description) ? foundOrder.Description : "پرداخت",
                    PaymentStatus = PaymentStatus.Processing
                };

                _paymentServiceLogger.LogInformation("payment creating price: {price}", newItem.Amount);
                await paymentRepository.InsertAsync(newItem);
                _paymentServiceLogger.LogInformation("payment created price: {paymentId}", newItem.PaymentId);
                #region GetAuthority NextPay
                try
                {
                    var _client = new HttpClient();
                    NextPayModel verifyModel = new NextPayModel()
                    {
                        amount = newItem.Amount,
                        api_key = ApiKey,
                        callback_uri = (foundOrder.hasAnyConsult == true ? "https://banoclub.com/Payment/Verify" : "https://banoclub.com/Payment/ChangePaymentStatus"),
                        currency = "IRT",
                        customer_phone = foundOrder.Mobile,
                        order_id = newItem.PaymentId.ToString()
                    };
                    string jsonComment = JsonConvert.SerializeObject(verifyModel);

                    StringContent content = new StringContent(jsonComment, Encoding.UTF8, "application/json");

                    var res = await _client.PostAsync("https://nextpay.org/nx/gateway/token", content);

                    var result = await res.Content.ReadAsStringAsync();
                    NextPayVerifyResponse model = JsonConvert.DeserializeObject<NextPayVerifyResponse>(result);
                    var endPoint = "";
                    _paymentServiceLogger.LogInformation("payment result code: {code}", model?.code);
                    if (model.code.Equals(-1))
                    {
                        newItem.TransId = model.trans_id;
                        endPoint = $"https://nextpay.org/nx/gateway/payment/{model.trans_id}";
                    }
                    newItem.Status = model.code;
                    #endregion

                    _paymentServiceLogger.LogInformation("payment updating paymentId: {paymentId}", newItem.PaymentId);
                    await paymentRepository.Update(newItem);
                    return new ServiceResult<object>() { IsSuccess = true, Data = endPoint };
                }
                catch (Exception ex)
                {
                    _paymentServiceLogger.LogInformation("payment error: {message}, inner Error: {innerError}", ex.Message, ex?.InnerException?.Message);
                }

            }
            else
            {
                _paymentServiceLogger.LogInformation("payment, order not found. orderId: {orderId}", orderId);
            }
            return new ServiceResult() { IsSuccess = false, ErrorMessage = "خطا در انجام عملیات" };
        }
    }
}
