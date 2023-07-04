﻿using BanooClub.Extentions;
using BanooClub.Migrations;
using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Models.DTO;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using BanooClub.Services.SkyroomService;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Specialized;
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
        IBanooClubEFRepository<ConsultantUserScheduleRating> _consultantUserScheduleRatingRepository;
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
                IBanooClubEFRepository<ConsultantUserScheduleRating> consultantUserScheduleRatingRepository,
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
            _consultantUserScheduleRatingRepository = consultantUserScheduleRatingRepository;
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

        public async Task<object> CreateComment(ConsultantNewCommentDTO input)
        {
            string errorResult = createCommentValidation(input);
            if (string.IsNullOrEmpty(errorResult))
            {
                var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                  ? _accessor.HttpContext.User.Identity.GetUserId()
                  : 0;
                if (userId > 0)
                {
                    var ConsultantUserScheduleId = await _consultantUserScheduleRepository
                        .GetQuery()
                        .Where(t => t.ConsultantId == input.id && t.IsPayed == true && t.UserId == userId && !t.ConsultantUserScheduleRatings.Any())
                        .Select(t => t.ConsultantUserScheduleId)
                        .FirstOrDefaultAsync();

                    if (ConsultantUserScheduleId > 0)
                    {
                        await _consultantUserScheduleRatingRepository
                       .InsertAsync(new ConsultantUserScheduleRating()
                       {
                           ConsultantUserScheduleId = ConsultantUserScheduleId,
                           CreateDate = DateTime.Now,
                           Description = input.description,
                           Rate = input.rate.Value
                       });
                    }
                    else
                        errorResult = "اطلاعاتی یافت نشد";
                }
                else
                    errorResult = "لطفا ابتدا لاگین کنید";
            }


            return new ServiceResult() { IsSuccess = false, ErrorMessage = errorResult };
        }

        private string createCommentValidation(ConsultantNewCommentDTO input)
        {
            if (input == null)
                return "لطفا کلیه اطلاعات را وارد کنید";
            if (input.id == null || input.id <= 0)
                return "لطفا شناسه مشاوره را وارد کنید";
            if (input.rate == null)
                return "لطفا امتیاز خود را وارد کنید";
            if (input.rate <= 0 || input.rate > 5)
                return "امتیاز وارد شده مجاز نمی باشد";
            if (!string.IsNullOrEmpty(input.description) && input.description.Length > 4000)
                return "توضیحات طولانی می باشد";


            return "";
        }

        public async Task<ServiceResult<long>> CreateOrder(long? id, ConsultTypeEnum? type, TimeSpan? targetTime, DateTime? targetDate)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                   ? _accessor.HttpContext.User.Identity.GetUserId()
                   : 0;
            if (userId > 0 && targetTime != null && targetDate != null && type != null)
            {
                var dayOFWeek = (MyDayOfWeek) ((int)targetDate.Value.DayOfWeek);
                var foundPrice = await _consultantRepository
                .GetQuery()
                .Where(t => t.Id == id && t.ConsultantSchedules
                    .Any(tt =>
                        tt.StartTime == targetTime && tt.IsAvailable == true && tt.DayOfWeek == dayOFWeek) &&
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
                        }).ToList(),
                        lastRequestStatus = _becomeConsultantRequestRepository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.Status).FirstOrDefault(),
                        rating = t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Any(tt => tt.IsConfirm == true) ? t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Where(tt => tt.IsConfirm == true).Average(t => t.Rate) : 0,
                        commentCount = t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Any(tt => tt.IsConfirm == true && !string.IsNullOrEmpty(tt.Description)) ? t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Count(tt => tt.IsConfirm == true && !string.IsNullOrEmpty(tt.Description)) : 0,
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
                    t.cd,
                    t.prices,
                    t.lastRequestStatus,
                    t.rating,
                    t.commentCount
                })
                .FirstOrDefault();

            return new ServiceResult<object>()
            {
                IsSuccess = tempResult != null,
                Data = tempResult
            };
        }

        public async Task<object> GetComments(long? id, int? pageNumber, int? take)
        {
            if (pageNumber == null || pageNumber <= 0)
                pageNumber = 1;
            if (take == null || take <= 0)
                take = 10;

            return await _consultantRepository
                .GetQuery()
                .Where(t => t.Id == id)
                .SelectMany(t => t.ConsultantUserSchedules)
                .SelectMany(t => t.ConsultantUserScheduleRatings)
                .Where(t => t.IsConfirm == true)
                .OrderByDescending(t => t.CreateDate)
                .Skip((pageNumber.Value - 1) * take.Value)
                .Take(take.Value)
                .Select(t => new
                {
                    userFullname = t.ConsultantUserSchedule.User.Name + " " + t.ConsultantUserSchedule.User.FamilyName,
                    t.CreateDate,
                    t.Description,
                    t.Rate,

                })
                .ToListAsync();
        }

        public async Task<object> GetNotEditedList(Models.DTO.ConsultGridFilterDTO input)
        {
            input = input ?? new Models.DTO.ConsultGridFilterDTO();
            var quiryResult = _consultantRepository.GetQuery()
                .Where(t =>
                        _becomeConsultantRequestRepository
                                .GetQuery()
                                .Where(tt => tt.UserId == t.UserId)
                                .OrderByDescending(tt => tt.CreateDate)
                                .Select(tt => tt.Status)
                                .FirstOrDefault() == BecomeConsultantRequestStatus.Accepted
                        );

            if (input.catIds != null && input.catIds.Count > 0)
                quiryResult = quiryResult.Where(t => t.Categories.Any(tt => input.catIds.Contains(tt.ConsultCategoryId)));
            if (!string.IsNullOrEmpty(input.searchInput))
                quiryResult = quiryResult.Where(t => (t.User.Name + " " + t.User.FamilyName).Contains(input.searchInput));
            if (input.isActive == true)
            {
                var targetDate = DateTime.Now.AddMinutes(-5);
                quiryResult = quiryResult.Where(t => t.User.Activities.Any(tt => tt.CreateDate >= targetDate));
            }

            var tempResultQuiry = quiryResult.Select(t => new
            {
                id = t.Id,
                cats = t.Categories.Select(tt => tt.ConsultCategory.Title).ToList(),
                name = t.User.Name,
                lName = t.User.FamilyName,
                userProfile = "/Media/Gallery/ConsultationUserProfile/" + _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationProfileImage).Select(tt => tt.PictureUrl).FirstOrDefault(),
                medicalSystemNumber = t.MedicalSystemNumber,
                rating = t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Any(tt => tt.IsConfirm == true) ? t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Where(tt => tt.IsConfirm == true).Average(t => t.Rate) : 0,
                commentCount = t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Any(tt => tt.IsConfirm == true && !string.IsNullOrEmpty(tt.Description)) ? t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Count(tt => tt.IsConfirm == true && !string.IsNullOrEmpty(tt.Description)) : 0,
                prices = t.ConsultantPrices.Select(tt => tt.Type).ToList(),
                successCount =
                            t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Any(tt => tt.IsConfirm == true && tt.Rate >= 3) ?
                                    t.ConsultantUserSchedules.SelectMany(tt => tt.ConsultantUserScheduleRatings).Where(tt => tt.IsConfirm == true && tt.Rate >= 3).Count() :
                                    0,
            });

            if (input.sort != null && input.sort == ConsultGridFilterDTOSortType.Rating)
                tempResultQuiry = tempResultQuiry.OrderByDescending(t => t.rating);
            else
                tempResultQuiry = tempResultQuiry.OrderByDescending(t => t.id);

            if (input.pageNumber == null || input.pageNumber <= 0)
                input.pageNumber = 1;
            if (input.take == null || input.take <= 0)
                input.take = 10;

            return await
                tempResultQuiry
                .Skip((input.pageNumber.Value - 1) * input.take.Value)
                .Take(input.take.Value)
                .ToListAsync();
        }

        public async Task<object> GetSchedule(long? id, System.Collections.Generic.List<MyDayOfWeek> days)
        {
            var quiryResult = _consultantRepository
                    .GetQuery()
                    .Where(t => t.Id == id)
                    .SelectMany(t => t.ConsultantSchedules);

            if (days != null && days.Count > 0)
                quiryResult = quiryResult.Where(t => days.Contains(t.DayOfWeek));

            return (
                    await quiryResult
                    .Select(t => new
                    {
                        t.DayOfWeek,
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
                    t.DayOfWeek,
                    t.StartTime,
                    t.EntTime,
                    status = getSchedualStatus(t.hasPayedUser, t.hasNotPayedUser, t.IsAvailable)
                })
                .GroupBy(t => t.DayOfWeek)
                .Select(t => new 
                {
                    DayOfWeek = t.Key,
                    hours = t.ToList()
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

        public async Task<object> CanComment(long? id)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                 ? _accessor.HttpContext.User.Identity.GetUserId()
                 : 0;
            if (userId > 0)
            {
                var ConsultantUserScheduleId = await _consultantUserScheduleRepository
                .GetQuery()
                    .Where(t => t.ConsultantId == id && t.IsPayed == true && t.UserId == userId && !t.ConsultantUserScheduleRatings.Any())
                    .Select(t => t.ConsultantUserScheduleId)
                    .FirstOrDefaultAsync();
                return ConsultantUserScheduleId > 0;
            }

            return false;
        }
    }
}
