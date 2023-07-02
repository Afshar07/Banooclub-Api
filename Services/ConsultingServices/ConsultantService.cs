using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using BanooClub.Services.SkyroomService;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public class ConsultantService : IConsultantService
    {
        IBanooClubEFRepository<Consultant> _consultantRepository;
        IBanooClubEFRepository<BecomeConsultantRequest> _becomeConsultantRequestRepository;
        IBanooClubEFRepository<SocialMedia> _socialMediaRepository;
        IBanooClubEFRepository<Order> _orderRepository;
        IBanooClubEFRepository<OrderItem> _orderItemRepository;
        IBanooClubEFRepository<ConsultantVideoConferanceUrl> _consultantVideoConferanceUrlRepository;
        IBanooClubEFRepository<ConsultantUserSchedule> _consultantUserScheduleRepository;
        IBanooClubEFRepository<SocialMedia> _mediaRepository;
        ISkyroomService _skyroomService;
        IHttpContextAccessor _accessor;
        public ConsultantService
            (
                IBanooClubEFRepository<Consultant> consultantRepository,
                IBanooClubEFRepository<BecomeConsultantRequest> becomeConsultantRequestRepository,
                IBanooClubEFRepository<SocialMedia> socialMediaRepository,
                IBanooClubEFRepository<Order> orderRepository,
                IBanooClubEFRepository<OrderItem> orderItemRepository,
                IBanooClubEFRepository<ConsultantVideoConferanceUrl> consultantVideoConferanceUrlRepository,
                IBanooClubEFRepository<ConsultantUserSchedule> consultantUserScheduleRepository,
                IBanooClubEFRepository<SocialMedia> mediaRepository,
                ISkyroomService skyroomService,
                IHttpContextAccessor accessor
            )
        {
            _consultantRepository = consultantRepository;
            _becomeConsultantRequestRepository = becomeConsultantRequestRepository;
            _socialMediaRepository = socialMediaRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _consultantVideoConferanceUrlRepository = consultantVideoConferanceUrlRepository;
            _skyroomService = skyroomService;
            _consultantUserScheduleRepository = consultantUserScheduleRepository;
            _mediaRepository = mediaRepository;
            _accessor = accessor;
        }

        public async Task<ServiceResult> ChageStatus(long? id)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;

            var foundUserConsult = await _consultantUserScheduleRepository
                .GetQuery()
                .Where(t => t.ConsultantId == id && t.UserId == userId)
                .FirstOrDefaultAsync();
            if (foundUserConsult != null)
            {
                foundUserConsult.Status = ConsultantUserScheduleStatus.Finished;
                await _consultantUserScheduleRepository.Update(foundUserConsult);
                return new ServiceResult() { IsSuccess = true };
            }

            return new ServiceResult() { IsSuccess = false, ErrorMessage = "اطلاعاتی یافت نشد" };
        }

        public async Task<ServiceResult<long>> CreateOrder(long? id, ConsultTypeEnum? type, TimeSpan? targetTime, DateTime? targetDate)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;
            if (userId > 0 && targetTime != null && targetDate != null && type != null)
            {
                var foundPrice = await _consultantRepository
                .GetQuery()
                .Where(t => t.Id == id && t.ConsultantSchedules
                    .Any(tt =>
                        tt.StartTime == targetTime && tt.IsAvailable == true) &&
                        !t.ConsultantUserSchedules.Any(tt => tt.TargetDate.Year == targetDate.Value.Year && tt.TargetDate.Month == targetDate.Value.Month && tt.TargetDate.Day == targetDate.Value.Day && tt.ReserveTime == targetTime && tt.IsPayed == true)
                       )
                .SelectMany(t => t.ConsultantPrices)
                .Where(t => t.Type == type)
                .Select(t => t.Price)
                .FirstOrDefaultAsync();

                if (foundPrice > 0)
                {
                    var newOrder = new Order()
                    {
                        CreateDate = DateTime.Now,
                        Description = "خرید بابت مشاوره",
                        SumPrice = foundPrice,
                        UserId = userId,
                        Status = Models.Enums.OrderStatus.Submit,
                    };
                    await _orderRepository.InsertAsync(newOrder);
                    await _orderItemRepository.InsertAsync(new OrderItem()
                    {
                        OrderId = newOrder.OrderId,
                        Count = 1,
                        Price = foundPrice,
                        Title = "خرید بابت مشاوره",
                        ConsultType = type,
                        ConsultantId = id.Value
                    });
                    await _consultantUserScheduleRepository.InsertAsync(new ConsultantUserSchedule()
                    {
                        ConsultantId = id.Value,
                        UserId = userId,
                        CreateDate = DateTime.Now,
                        TargetDate = targetDate.Value,
                        ReserveTime = targetTime.Value,
                        Status = ConsultantUserScheduleStatus.Created,
                        OrderId = newOrder.OrderId
                    });

                    return new ServiceResult<long>() { IsSuccess = true, Data = newOrder.OrderId };
                }
            }

            return new ServiceResult<long>() { IsSuccess = false, ErrorMessage = "خطا در انجام عملیات" };
        }

        public async Task<ServiceResult<string>> CreateVideoConfranceRoom(long? id)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;
            //var userId = 2;

            var foundItem = await _consultantRepository
                .GetQuery()
                .Where(t => t.Id == id && t.OrderItems.Any(tt => tt.Order.IsPayed == true && tt.Order.UserId == userId && tt.ConsultType != null && tt.ConsultType == ConsultTypeEnum.VedioConsultancy))
                .Select(t => new
                {
                    fromUserName = (!string.IsNullOrEmpty(t.User.Name) ? t.User.Name : "") + " " + (!string.IsNullOrEmpty(t.User.FamilyName) ? t.User.FamilyName : ""),
                    toName = t.OrderItems.Select(tt => tt.Order.User.Name).FirstOrDefault(),
                    toFamily = t.OrderItems.Select(tt => tt.Order.User.FamilyName).FirstOrDefault(),
                    orderId = t.OrderItems.Select(tt => tt.OrderId).FirstOrDefault(),
                    t.Id
                })
                .FirstOrDefaultAsync();

            if (foundItem != null && foundItem.orderId > 0)
            {
                var foundVideoUrl = await _consultantVideoConferanceUrlRepository
                    .GetQuery()
                    .Where(t => t.ConsultantId == foundItem.Id && t.OrderId == foundItem.orderId)
                    .Select(t => t.ConferanceUrl)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(foundVideoUrl))
                    return new ServiceResult<string>() { Data = foundVideoUrl, IsSuccess = true };
                else
                {
                    try
                    {
                        var result = await _skyroomService.CreateRoome(new InputModel<CreateRoomInputModel>()
                        {
                            @params = new CreateRoomInputModel()
                            {
                                guest_login = true,
                                max_users = 2,
                                name = Guid.NewGuid().ToString() + "_" + id,
                                title = foundItem.fromUserName + " VS " + foundItem.toName + " " + foundItem.toFamily,
                                op_login_first = false
                            }
                        });
                        try
                        {
                            var url = await _skyroomService.GetRoomUrl(new InputModel<GetRomeInfoInputModel>()
                            {
                                @params = new GetRomeInfoInputModel()
                                {
                                    language = "fa",
                                    room_id = result.result
                                }
                            });

                            if (!string.IsNullOrEmpty(url.result))
                            {
                                await _consultantVideoConferanceUrlRepository.InsertAsync(new ConsultantVideoConferanceUrl()
                                {
                                    ConferanceUrl = url.result,
                                    ConsultantId = foundItem.Id,
                                    OrderId = foundItem.orderId
                                });
                                return new ServiceResult<string>() { IsSuccess = true, Data = url.result };
                            }
                            else
                            {
                                return new ServiceResult<string>() { IsSuccess = false, ErrorMessage = "خطا در دریافت کنفرانس" };
                            }
                        }
                        catch
                        {
                            return new ServiceResult<string>() { IsSuccess = false, ErrorMessage = "خطا در دریافت کنفرانس" };
                        }

                    }
                    catch
                    {
                        return new ServiceResult<string>() { IsSuccess = false, ErrorMessage = "خطا در ساخت کنفرانس" };
                    }

                }
            }

            return new ServiceResult<string>() { IsSuccess = false, ErrorMessage = "اطلاعاتی یافت نشد" };
        }

        public async Task<ServiceResult<object>> GetById(long? id)
        {
            var tempResult =
                (
                    await _consultantRepository
                    .GetQuery()
                    .Where(t => t.Id == id)
                    .OrderByDescending(t => t.Id)
                    .Take(1)
                    .Select(t => new
                    {
                        id = t.Id,
                        name = t.User.Name,
                        lName = t.User.FamilyName,
                        cats = t.Categories.Select(tt => tt.ConsultCategory.Title).ToList(),
                        des = t.Description,
                        v = _socialMediaRepository.GetQuery().Where(tt => tt.Type == Models.Enums.MediaTypes.Consultation && tt.ObjectId == t.Id).Select(tt => tt.PictureUrl).FirstOrDefault(),
                        upi = _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationProfileImage).Select(tt => tt.PictureUrl).FirstOrDefault(),
                        medicalSystemNumber = t.MedicalSystemNumber,
                        city = t.City.Name,
                        state = t.State.Name,
                        cd = t.CreateDate,
                        prices = t.ConsultantPrices.Select(tt => new
                        {
                            tt.Price,
                            tt.Type
                        }).ToList()
                    })
                    .ToListAsync()
                )
                .Select(t => new
                {
                    t.id,
                    t.name,
                    t.lName,
                    t.cats,
                    t.des,
                    v = !string.IsNullOrEmpty(t.v) ? ("/Media/Gallery/VideoConsultation/" + t.v) : "",
                    upi = !string.IsNullOrEmpty(t.upi) ? ("/Media/Gallery/ConsultationUserProfile/" + t.upi) : "",
                    t.medicalSystemNumber,
                    t.city,
                    t.state,
                    t.cd
                })
                .FirstOrDefault();

            return new ServiceResult<object>()
            {
                IsSuccess = tempResult != null,
                Data = tempResult
            };
        }

        public async Task<object> GetNotEditedList()
        {
            List<object> result = new List<object>();
            var tempResult = await _consultantRepository.GetQuery()
                .Where(t =>
                        _becomeConsultantRequestRepository
                                .GetQuery()
                                .Where(tt => tt.UserId == t.UserId)
                                .OrderByDescending(tt => tt.CreateDate)
                                .Select(tt => tt.Status)
                                .FirstOrDefault() == BecomeConsultantRequestStatus.Accepted
                        )
                .Select(t => new
                {
                    id = t.Id,
                    cats = t.Categories.Select(tt => tt.ConsultCategory.Title).ToList(),
                    name = t.User.Name,
                    lName = t.User.FamilyName,
                    userProfile = _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationProfileImage).Select(tt => tt.PictureUrl).FirstOrDefault(),

                })
                .ToListAsync();

            var allCats = tempResult.SelectMany(t => t.cats).GroupBy(t => t).Select(t => t.Key).ToList();
            foreach (var cat in allCats)
            {
                result.Add(new
                {
                    title = cat,
                    users = tempResult
                            .Where(t => t.cats.Any(tt => tt == cat))
                            .Select(t => new
                            {
                                t.id,
                                t.name,
                                t.lName,
                                userProfile = !string.IsNullOrEmpty(t.userProfile) ? ("/Media/Gallery/ConsultationUserProfile/" + t.userProfile) : ""
                            })
                            .ToList()
                });
            }

            return result;
        }

        public async Task<object> GetSchedule(long? id)
        {
            return (
                    await _consultantRepository
                    .GetQuery()
                    .Where(t => t.Id == id)
                    .SelectMany(t => t.ConsultantSchedules)
                    .Select(t => new
                    {
                        t.StartTime,
                        t.EntTime,
                        t.IsAvailable,
                        hasPayedUser = _consultantUserScheduleRepository.GetQuery().Any(tt => tt.ConsultantId == id && tt.ReserveTime == t.StartTime && tt.IsPayed == true),
                        hasNotPayedUser = _consultantUserScheduleRepository.GetQuery().Any(tt => tt.ConsultantId == id && tt.ReserveTime == t.StartTime && tt.IsPayed != true),
                    })
                    .ToListAsync()
                )
                .Select(t => new
                {
                    t.StartTime,
                    t.EntTime,
                    status = getSchedualStatus(t.hasPayedUser, t.hasNotPayedUser, t.IsAvailable)
                })
                .ToList()
                ;
        }

        int getSchedualStatus(bool hasPayedUser, bool hasNotPayedUser, bool isAvailable)
        {
            if (hasPayedUser)
                return 3;
            if (hasNotPayedUser)
                return 2;
            if (isAvailable)
                return 1;

            return -1;
        }
    }
}
