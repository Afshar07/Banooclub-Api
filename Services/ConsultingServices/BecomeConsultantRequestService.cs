using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using BanooClub.Services.SmsServices;
using BanooClub.Services.SocialMediaServices;
using BanooClub.Utilities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace BanooClub.Services.ConsultingServices
{
    public class BecomeConsultantRequestService : IBecomeConsultantRequestService
    {
        private readonly IBanooClubEFRepository<BecomeConsultantRequest> _repository;
        private readonly IBanooClubEFRepository<City> _cityRepository;
        private readonly IBanooClubEFRepository<User> _userRepository;
        private readonly IBanooClubEFRepository<ConsultCategory> _consultCategoryRepository;
        private readonly IBanooClubEFRepository<BecomeConsultantRequestSchedule> _becomeConsultantRequestScheduleRepository;
        private readonly IBanooClubEFRepository<State> _stateRepository;
        private readonly IBanooClubEFRepository<Consultant> _consultantRepository;
        private readonly IBanooClubEFRepository<ConsultantSchedule> _consultantScheduleRepository;
        private readonly IBanooClubEFRepository<BecomeConsultantRequestConsultCategory> _becomeConsultantRequestConsultCategoryRepository;
        private readonly IBanooClubEFRepository<BecomeConsultantRequestConsultPrice> _becomeConsultantRequestConsultPriceRepository;
        private readonly IBanooClubEFRepository<ConsultantConsultCategory> _consultantConsultCategoryRepository;
        private readonly IBanooClubEFRepository<ConsultantPrice> _consultantPriceRepository;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        private readonly ISocialMediaService _mediaService;
        private readonly IBecomeConsultantRequestScheduleService _becomeConsultantRequestScheduleService;
        private readonly ISmsSenderService _smsSenderService;
        private BanooClubDBContext _dBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public BecomeConsultantRequestService
            (
                IBanooClubEFRepository<BecomeConsultantRequest> repository,
                IHttpContextAccessor httpContextAccessor,
                IBanooClubEFRepository<Consultant> consultantRepository,
                BanooClubDBContext dBContext,
                IBanooClubEFRepository<City> cityRepository,
                IBanooClubEFRepository<State> stateRepository,
                IBanooClubEFRepository<ConsultCategory> consultCategoryRepository,
                IBanooClubEFRepository<BecomeConsultantRequestConsultCategory> becomeConsultantRequestConsultCategoryRepository,
                IBanooClubEFRepository<BecomeConsultantRequestConsultPrice> becomeConsultantRequestConsultPriceRepository,
                ISocialMediaService mediaService,
                IBanooClubEFRepository<SocialMedia> mediaRepository,
                IBanooClubEFRepository<ConsultantConsultCategory> consultantConsultCategoryRepository,
                IBanooClubEFRepository<ConsultantPrice> consultantPriceRepository,
                IBecomeConsultantRequestScheduleService becomeConsultantRequestScheduleService,
                IBanooClubEFRepository<BecomeConsultantRequestSchedule> becomeConsultantRequestScheduleRepository,
                IBanooClubEFRepository<ConsultantSchedule> consultantScheduleRepository,
                IBanooClubEFRepository<User> userRepository,
                ISmsSenderService smsSenderService,
                IConfiguration configuration
            )
        {
            _userRepository = userRepository;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _consultantRepository = consultantRepository;
            _dBContext = dBContext;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _consultCategoryRepository = consultCategoryRepository;
            _becomeConsultantRequestConsultCategoryRepository = becomeConsultantRequestConsultCategoryRepository;
            _becomeConsultantRequestConsultPriceRepository = becomeConsultantRequestConsultPriceRepository;
            _mediaService = mediaService;
            _mediaRepository = mediaRepository;
            _consultantConsultCategoryRepository = consultantConsultCategoryRepository;
            _consultantPriceRepository = consultantPriceRepository;
            _becomeConsultantRequestScheduleService = becomeConsultantRequestScheduleService;
            _becomeConsultantRequestScheduleRepository = becomeConsultantRequestScheduleRepository;
            _consultantScheduleRepository = consultantScheduleRepository;
            _smsSenderService = smsSenderService;
            _configuration = configuration;
        }

        public async Task<IServiceResult> CreateBecomeConsultantRequest(CreateBecomeConsultantRequestDTO dto)
        {
            var operation = new ServiceResult();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            string errorResult = createUpdateValidation(dto, userId);
            if (string.IsNullOrEmpty(errorResult))
            {
                var request = new BecomeConsultantRequest(userId, dto.NationalCode, dto.MedicalSystemNumber, dto.StateId.Value, dto.CityId.Value, dto.PhoneNumber, dto.ShabaNo, dto.BankName, dto.CartNo, dto.Description, dto.DurationMinute);
                await _repository.InsertAsync(request);
                if (dto.Categories != null && dto.Categories.Count > 0)
                    foreach (var category in dto.Categories)
                        await _becomeConsultantRequestConsultCategoryRepository.InsertAsync(new BecomeConsultantRequestConsultCategory() { CategoryId = category, RequestId = request.Id });
                if (dto.Prices != null && dto.Prices.Count > 0)
                    foreach (var price in dto.Prices)
                        await _becomeConsultantRequestConsultPriceRepository.InsertAsync(new BecomeConsultantRequestConsultPrice() { BecomeConsultantRequestId = request.Id, Price = price.Price.Value, Type = price.Type.Value });

                if (dto.StartAndEndWork.Count == 2)
                {
                    var startTime = dto.StartAndEndWork[0];
                    var endTime = dto.StartAndEndWork[1];
                    var durationList = await _becomeConsultantRequestScheduleService.CreateCleanIfExist(dto.DurationMinute, request.Id, startTime, endTime);
                }

                if (!string.IsNullOrEmpty(dto.FileData))
                {
                    var outPut = _mediaService.SaveImage(dto.FileData, EntityUrls.Consultation);
                    if (outPut != null && outPut.IsSuccess == true)
                    {
                        SocialMedia dbMedia = new SocialMedia()
                        {
                            ObjectId = request.Id,
                            PictureUrl = outPut.ImageName,
                            Type = MediaTypes.ConsultationRequest,
                            Priority = 0,
                            UpdateDate = DateTime.Now,
                        };
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
                else
                {
                    var foundPrevVideoObjectId = await _repository
                        .GetQuery()
                        .Where(t => t.UserId == userId && _mediaRepository.GetQuery().Any(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequest))
                        .OrderByDescending(t => t.Id)
                        .Select(t => t.Id)
                        .FirstOrDefaultAsync();
                    if (foundPrevVideoObjectId > 0)
                    {
                        var imageUrl = await _mediaRepository.GetQuery().Where(t => t.Type == MediaTypes.ConsultationRequest && t.ObjectId == foundPrevVideoObjectId).Select(t => t.PictureUrl).FirstOrDefaultAsync();
                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            SocialMedia dbMedia = new SocialMedia()
                            {
                                ObjectId = request.Id,
                                PictureUrl = imageUrl,
                                Type = MediaTypes.ConsultationRequest,
                                Priority = 0,
                                UpdateDate = DateTime.Now,
                            };
                            await _mediaRepository.InsertAsync(dbMedia);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(dto.ImageFileData))
                {
                    var outPutImage = _mediaService.SaveImage(dto.ImageFileData, EntityUrls.ConsultationUserProfile);
                    if (outPutImage != null && outPutImage.IsSuccess == true)
                    {
                        SocialMedia dbMedia = new SocialMedia()
                        {
                            ObjectId = request.Id,
                            PictureUrl = outPutImage.ImageName,
                            Type = MediaTypes.ConsultationRequestProfileImage,
                            Priority = 0,
                            UpdateDate = DateTime.Now,
                        };
                        await _mediaRepository.InsertAsync(dbMedia);
                    }
                }
                else
                {
                    var foundPrevVideoObjectId = await _repository
                        .GetQuery()
                        .Where(t => t.UserId == userId && _mediaRepository.GetQuery().Any(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequestProfileImage))
                        .OrderByDescending(t => t.Id)
                        .Select(t => t.Id)
                        .FirstOrDefaultAsync();
                    if (foundPrevVideoObjectId > 0)
                    {
                        var imageUrl = await _mediaRepository.GetQuery().Where(t => t.Type == MediaTypes.ConsultationRequestProfileImage && t.ObjectId == foundPrevVideoObjectId).Select(t => t.PictureUrl).FirstOrDefaultAsync();
                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            SocialMedia dbMedia = new SocialMedia()
                            {
                                ObjectId = request.Id,
                                PictureUrl = imageUrl,
                                Type = MediaTypes.ConsultationRequestProfileImage,
                                Priority = 0,
                                UpdateDate = DateTime.Now,
                            };
                            await _mediaRepository.InsertAsync(dbMedia);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(request.PhoneNumber) && request.PhoneNumber.IsMobile())
                {
                    var foundUser = await _userRepository.GetQuery().Where(t => t.UserId == userId).Select(t => new { t.Name, t.FamilyName }).FirstOrDefaultAsync();
                    var result = await _smsSenderService.UltraFastSend(new UltraFastSend()
                    {
                        Mobile = Convert.ToInt64(request.PhoneNumber),
                        TemplateId = _configuration.GetValue<int>("smsTemplateIds:newRequestSMSTemplate"),
                        ParameterArray = new List<UltraFastParameters>()
                        {
                            new UltraFastParameters()
                            {
                                Parameter = "username" ,
                                ParameterValue =  foundUser?.Name + " " + foundUser?.FamilyName
                            }
                        }.ToArray()
                    });
                }

                return operation.Ok();
            }
            return operation.Failure(errorResult);
        }

        private string createUpdateValidation(CreateBecomeConsultantRequestDTO dto, long userId)
        {
            if (dto == null)
                return "لطفا کلیه پارامتر ها را وارد کنید";
            if (userId <= 0)
                return "لطفا ابتدا لاگین کنید";
            if (string.IsNullOrEmpty(dto.NationalCode))
                return "لطفا کد ملی را وارد کنید";
            if (!dto.NationalCode.IsNationalCode())
                return "کد ملی مجاز نمی باشد";
            if (dto.StateId == null || dto.StateId <= 0)
                return "لطفا استان را انتخاب کنید";
            if (!_stateRepository.GetQuery().Any(t => t.StateId == dto.StateId))
                return "استان یافت نشد";
            if (dto.CityId == null || dto.CityId <= 0)
                return "لطفا شهر را انتخاب کنید";
            if (!_cityRepository.GetQuery().Any(t => t.StateId == dto.StateId && t.CityId == dto.CityId))
                return "شهر یافت نشد";
            if (string.IsNullOrEmpty(dto.PhoneNumber))
                return "لطفا شماره همراه را وارد کنید";
            if (!dto.PhoneNumber.IsMobile())
                return "شماره همراه وارد شده مجاز نمی باشد";
            if (string.IsNullOrEmpty(dto.MedicalSystemNumber))
                return "شماره نظام پزشکی را وارد کنید";
            if (string.IsNullOrEmpty(dto.ShabaNo))
                return "لطفا شماره شبا را وارد کنید";
            if (!dto.ShabaNo.IsShaba())
                return "شماره شبا مجاز نمی باشد";
            if (string.IsNullOrEmpty(dto.CartNo))
                return "لطفا شماره کارت خود را وارد کنید";
            if (dto.CartNo.Length != 16)
                return "شماره کارت باید 16 عدد باشد";
            if (string.IsNullOrEmpty(dto.BankName))
                return "لطفا نام بانک را وارد کنید";
            if (dto.DurationMinute == null || dto.DurationMinute <= 0)
                return "لطفا بازه نوبت دهی خود را وارد کنید";
            if (dto.Categories == null || dto.Categories.Count <= 0)
                return "لطفا حداقل یک گروه بندی را انتخاب کنید";
            if (dto.Prices == null || dto.Prices.Count <= 0)
                return "لطفا حداقل یک قیمت وارد کنید";
            foreach (var price in dto.Prices)
                if (dto.Prices.Any(t => t != price && t.Type == price.Type))
                    return "نوع مبلغ وارد شده تکراری می باشد";
            foreach (var catId in dto.Categories)
                if (!_consultCategoryRepository.GetQuery().Any(t => t.Id == catId))
                    return "گروه بندی مجاز نمی باشد";
            foreach (var price in dto.Prices)
                if (price.Type == null || price.Price == null || price.Price <= 0)
                    return "مبلغ مجاز نمی باشد";
            if (string.IsNullOrEmpty(dto.Description))
                return "لطفا توضیحات را وارد کنید";
            if (dto.Description.Length > 4000)
                return "توضیحات طولانی می باشد";
            if (string.IsNullOrEmpty(dto.FileData) && !_repository.GetQuery().Any(t => t.UserId == userId))
                return "لطفا ویدیو معرفی خود را وارد کنید";
            if (string.IsNullOrEmpty(dto.ImageFileData) && !_repository.GetQuery().Any(t => t.UserId == userId))
                return "لطفا تصویر پروفایل خود را وارد کنید";
            var allDuration = GetDuration();
            if (!allDuration.Any(t => t.Id == dto.DurationMinute))
                return "مدت زمان انتخاب شده مجاز نمی باشد";
            if (dto.StartAndEndWork == null || dto.StartAndEndWork.Count != 2)
                return "لطفا بازه شروع و پایان زمانی انتخاب کنید";

            var startDate = dto.StartAndEndWork[0];
            var endDate = dto.StartAndEndWork[1];
            if ((endDate - startDate).TotalMinutes <= 0)
                return "زمان شروع و پایان صحیح نمی باشد";

            if ((endDate - startDate).TotalMinutes % dto.DurationMinute != 0)
                return "زمان پایان صحیح نمی باشد";

            return "";
        }

        public async Task<IServiceResult> AcceptRequestAndCreateConsultant(long requestId)
        {
            var operation = new ServiceResult();

            var request = await _repository
                .GetQuery()
                .Include(t => t.User)
                .Where(x => x.Id == requestId)
                .Include(t => t.Categories)
                .Include(t => t.BecomeConsultantRequestConsultPrices)
                .Include(t => t.BecomeConsultantRequestSchedules)
                .FirstOrDefaultAsync();

            if (request == null)
                return operation.Failure("NotFound");

            if (request.Status == BecomeConsultantRequestStatus.Accepted)
                return operation.Failure("AlreadyAccepted");

            var foundMedia = await _mediaRepository
                .GetQuery()
                .Where(t => t.ObjectId == request.Id && t.Type == MediaTypes.ConsultationRequest)
                .FirstOrDefaultAsync();
            var foundMediaProfile = await _mediaRepository
               .GetQuery()
               .Where(t => t.ObjectId == request.Id && t.Type == MediaTypes.ConsultationRequestProfileImage)
               .FirstOrDefaultAsync();

            await _dBContext.Database.BeginTransactionAsync();

            try
            {
                request.Status = BecomeConsultantRequestStatus.Accepted;

                await _repository.Update(request);

                var consultant = _consultantRepository
                    .GetQuery()
                    .Include(t => t.Categories)
                    .Include(t => t.ConsultantPrices)
                    .Include(t => t.ConsultantSchedules)
                    .Where(x => x.UserId == request.UserId)
                    .FirstOrDefault();

                if (consultant == null)
                {
                    consultant = new Consultant(request.UserId, request.NationalCode, request.MedicalSystemNumber, request.StateId, request.CityId, request.PhoneNumber, request.ShabaNo, request.BankName, request.CartNo, request.Description, request.ConsultingDurationMinut);
                    await _consultantRepository.InsertAsync(consultant);
                    if (request.Categories != null && request.Categories.Count > 0)
                        foreach (var cat in request.Categories)
                            await _consultantConsultCategoryRepository.InsertAsync(new ConsultantConsultCategory()
                            {
                                ConsultantId = consultant.Id,
                                ConsultCategoryId = cat.CategoryId
                            });

                    if (request.BecomeConsultantRequestConsultPrices != null && request.BecomeConsultantRequestConsultPrices.Count > 0)
                        foreach (var price in request.BecomeConsultantRequestConsultPrices)
                            await _consultantPriceRepository.InsertAsync(new ConsultantPrice()
                            {
                                ConsultantId = consultant.Id,
                                Type = price.Type,
                                Price = price.Price
                            });

                    if (foundMedia != null)
                    {
                        await _mediaRepository.InsertAsync(new SocialMedia()
                        {
                            ObjectId = consultant.Id,
                            PictureUrl = foundMedia.PictureUrl,
                            Priority = 0,
                            Type = MediaTypes.Consultation,
                        });
                    }
                    if (foundMediaProfile != null)
                    {
                        await _mediaRepository.InsertAsync(new SocialMedia()
                        {
                            ObjectId = consultant.Id,
                            PictureUrl = foundMediaProfile.PictureUrl,
                            Priority = 0,
                            Type = MediaTypes.ConsultationProfileImage,
                        });
                    }

                    if (request.BecomeConsultantRequestSchedules != null && request.BecomeConsultantRequestSchedules.Count > 0)
                        foreach (var item in request.BecomeConsultantRequestSchedules)
                            await _consultantScheduleRepository.InsertAsync(new ConsultantSchedule()
                            {
                                ConsultantId = consultant.Id,
                                CreateDate = DateTime.Now,
                                EntTime = item.EntTime,
                                IsAvailable = item.IsAvailable,
                                StartTime = item.StartTime,
                                IsDeleted = item.IsDeleted,
                                DayOfWeek = item.DayOfWeek
                            });
                }
                else
                {
                    consultant.Update(request.NationalCode, request.MedicalSystemNumber, request.StateId, request.CityId, request.PhoneNumber, request.ShabaNo, request.BankName, request.CartNo, request.Description, request.ConsultingDurationMinut);
                    await _consultantRepository.Update(consultant);

                    if (consultant.Categories != null && consultant.Categories.Count > 0)
                        while (consultant.Categories != null && consultant.Categories.Count > 0)
                            _consultantConsultCategoryRepository.Erase(consultant.Categories[0]);

                    if (consultant.ConsultantPrices != null && consultant.ConsultantPrices.Count > 0)
                        while (consultant.ConsultantPrices != null && consultant.ConsultantPrices.Count > 0)
                            _consultantPriceRepository.Erase(consultant.ConsultantPrices[0]);

                    if (request.Categories != null && request.Categories.Count > 0)
                        foreach (var cat in request.Categories)
                            await _consultantConsultCategoryRepository.InsertAsync(new ConsultantConsultCategory()
                            {
                                ConsultantId = consultant.Id,
                                ConsultCategoryId = cat.CategoryId
                            });

                    if (request.BecomeConsultantRequestConsultPrices != null && request.BecomeConsultantRequestConsultPrices.Count > 0)
                        foreach (var price in request.BecomeConsultantRequestConsultPrices)
                            await _consultantPriceRepository.InsertAsync(new ConsultantPrice()
                            {
                                ConsultantId = consultant.Id,
                                Type = price.Type,
                                Price = price.Price
                            });

                    var prevImageList = await _mediaRepository
                            .GetQuery()
                            .Where(t => t.ObjectId == consultant.Id && t.Type == MediaTypes.Consultation)
                            .ToListAsync();

                    if (prevImageList != null && prevImageList.Count > 0)
                        foreach (var prevImageItem in prevImageList)
                            _mediaRepository.Erase(prevImageItem);

                    if (foundMedia != null)
                    {
                        await _mediaRepository.InsertAsync(new SocialMedia()
                        {
                            ObjectId = consultant.Id,
                            PictureUrl = foundMedia.PictureUrl,
                            Priority = 0,
                            Type = MediaTypes.Consultation,
                        });
                    }

                    if (foundMediaProfile != null)
                    {
                        await _mediaRepository.InsertAsync(new SocialMedia()
                        {
                            ObjectId = consultant.Id,
                            PictureUrl = foundMediaProfile.PictureUrl,
                            Priority = 0,
                            Type = MediaTypes.ConsultationProfileImage,
                        });
                    }

                    if (request.BecomeConsultantRequestSchedules != null && request.BecomeConsultantRequestSchedules.Count > 0)
                    {
                        var allPrevScheduale = await _consultantScheduleRepository.GetQuery().Where(t => t.ConsultantId == consultant.Id).ToListAsync();
                        if (allPrevScheduale != null)
                            foreach (var prevSchedual in allPrevScheduale)
                                await _consultantScheduleRepository.Delete(prevSchedual);

                        foreach (var item in request.BecomeConsultantRequestSchedules)
                            await _consultantScheduleRepository.InsertAsync(new ConsultantSchedule()
                            {
                                ConsultantId = consultant.Id,
                                CreateDate = DateTime.Now,
                                EntTime = item.EntTime,
                                IsAvailable = item.IsAvailable,
                                StartTime = item.StartTime,
                                IsDeleted = item.IsDeleted,
                                DayOfWeek = item.DayOfWeek
                            });
                    }
                }
                await _dBContext.Database.CommitTransactionAsync();

                if (!string.IsNullOrEmpty(request.PhoneNumber) && request.PhoneNumber.IsMobile())
                {
                    var result = await _smsSenderService.UltraFastSend(new UltraFastSend()
                    {
                        Mobile = Convert.ToInt64(request.PhoneNumber),
                        TemplateId = _configuration.GetValue<int>("smsTemplateIds:confirmSMSTemplate"),
                        ParameterArray = new List<UltraFastParameters>()
                        {
                            new UltraFastParameters()
                            {
                                Parameter = "username" , 
                                ParameterValue =  request.User?.Name + " " + request.User?.FamilyName
                            }
                        }.ToArray()
                    });
                }
            }
            catch
            {
                await _dBContext.Database.RollbackTransactionAsync();
                throw;
            }
            return operation.Ok();
        }

        public async Task<IServiceResult> RejectRequest(long requestId)
        {
            var operation = new ServiceResult();

            var request = await _repository.GetQuery().Include(t => t.User).Where(x => x.Id == requestId).FirstOrDefaultAsync();
            if (request == null)
                return operation.Failure("NotFound");

            request.Status = BecomeConsultantRequestStatus.Rejected;

            await _repository.Update(request);
            if (!string.IsNullOrEmpty(request.PhoneNumber) && request.PhoneNumber.IsMobile())
            {
                var result = await _smsSenderService.UltraFastSend(new UltraFastSend()
                {
                    Mobile = Convert.ToInt64(request.PhoneNumber),
                    TemplateId = _configuration.GetValue<int>("smsTemplateIds:rejectSMSTemplate"),
                    ParameterArray = new List<UltraFastParameters>()
                        {
                            new UltraFastParameters()
                            {
                                Parameter = "username" ,
                                ParameterValue =  request.User?.Name + " " + request.User?.FamilyName
                            }
                        }.ToArray()
                });
            }

            return operation.Ok();
        }

        public async Task<IServiceResult<PageModel<BecomeConsultantRequestDTO>>> GetRequests(int page, int size, BecomeConsultantRequestStatus? status)
        {

            var operation = new ServiceResult<PageModel<BecomeConsultantRequestDTO>>();

            var query = _repository
                .GetQuery()
                .Select(t => new 
                {
                    BankName = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.BankName).FirstOrDefault(),
                    Id = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.Id).FirstOrDefault(),
                    CartNo = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.CartNo).FirstOrDefault(),
                    City = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.City.Name).FirstOrDefault(),
                    State = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.State.Name).FirstOrDefault(),
                    PhoneNumber = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.PhoneNumber).FirstOrDefault(),
                    ShabaNo = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.ShabaNo).FirstOrDefault(),
                    NatiaonCode = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.NationalCode).FirstOrDefault(),
                    MedicalSystemNumber = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.MedicalSystemNumber).FirstOrDefault(),
                    UserFullName = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.User.Name + " " + tt.User.FamilyName).FirstOrDefault(),
                    Status = _repository.GetQuery().Where(tt => tt.UserId == t.UserId).OrderByDescending(tt => tt.Id).Select(tt => tt.Status).FirstOrDefault()
                })
                .Distinct();

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            var skip = (page - 1) * size;

            var totalCount = await query.CountAsync();
            var data = await
                query.Select(x => new BecomeConsultantRequestDTO
                {
                    Id = x.Id,
                    BankName = x.BankName,
                    CartNo = x.CartNo,
                    City = x.City,
                    State = x.State,
                    PhoneNumber = x.PhoneNumber,
                    ShabaNo = x.ShabaNo,
                    NatiaonCode = x.NatiaonCode,
                    MedicalSystemNumber = x.MedicalSystemNumber,
                    UserFullName = x.UserFullName,
                    Status = x.Status
                })
                .OrderByDescending(t => t.Id)
                .Skip(skip)
                .Take(size)
                .ToListAsync();

            var pageModel = new PageModel<BecomeConsultantRequestDTO>(page, size, totalCount, data);
            return operation.Ok(pageModel);
        }

        public List<IdTitleKeyValueDTO> GetDuration()
        {
            return new List<IdTitleKeyValueDTO>()
            {
                new IdTitleKeyValueDTO() { Id = 20, Title = "بیست دقیقه" },
                new IdTitleKeyValueDTO() { Id = 40, Title = "چهل دقیقه" },

            };
        }

        public async Task<object> GetLast()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();

            return (await _repository
                .GetQuery()
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreateDate)
                .Select(t => new
                {
                    t.NationalCode,
                    t.MedicalSystemNumber,
                    t.StateId,
                    t.CityId,
                    t.PhoneNumber,
                    t.ShabaNo,
                    t.BankName,
                    t.CartNo,
                    DurationMinute = t.ConsultingDurationMinut,
                    t.Description,
                    FileData = _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequest).Select(tt => tt.PictureUrl).FirstOrDefault(),
                    ImageFileData = _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequestProfileImage).Select(tt => tt.PictureUrl).FirstOrDefault(),
                    Prices = t.BecomeConsultantRequestConsultPrices.Select(tt => new CreateBecomeConsultantRequestPriceDTO { Price = tt.Price, Type = tt.Type }).ToList(),
                    Categories = t.Categories.Select(tt => new { id = tt.CategoryId, title = tt.Category.Title }).ToList(),
                    StartAndEndWork = t.BecomeConsultantRequestSchedules.Where(tt => tt.DayOfWeek == DayOfWeek.Sunday).ToList(),
                    userfullname = t.User.Name + " " + t.User.FamilyName,
                    consultantId = _consultantRepository.GetQuery().Where(tt => tt.UserId == userId).Select(tt => tt.Id).FirstOrDefault()
                })
                .Take(1)
                .ToListAsync())
                .Select(t => new
                {
                    FileData = !string.IsNullOrEmpty(t.FileData) ? ("/Media/Gallery/VideoConsultation/" + t.FileData) : "",
                    ImageFileData = !string.IsNullOrEmpty(t.ImageFileData) ? ("/Media/Gallery/ConsultationUserProfile/" + t.ImageFileData) : "",
                    t.NationalCode,
                    t.MedicalSystemNumber,
                    t.StateId,
                    t.CityId,
                    t.PhoneNumber,
                    t.ShabaNo,
                    t.BankName,
                    t.CartNo,
                    t.DurationMinute,
                    t.Description,
                    t.Prices,
                    t.Categories,
                    t.userfullname,
                    startAndEndWork = t.StartAndEndWork != null && t.StartAndEndWork.Count > 0 ? new List<TimeSpan>() { t.StartAndEndWork.OrderBy(tt => tt.StartTime).Select(tt => tt.StartTime).FirstOrDefault(), t.StartAndEndWork.OrderByDescending(tt => tt.EntTime).Select(tt => tt.EntTime).FirstOrDefault() } : null,
                    t.consultantId
                })
                .FirstOrDefault();
        }

        public async Task<object> GetById(long id)
        {
            return (await _repository
               .GetQuery()
               .Where(t => t.Id == id)
               .OrderByDescending(t => t.CreateDate)
                .Select(t => new
                {
                    t.NationalCode,
                    t.MedicalSystemNumber,
                    t.StateId,
                    t.CityId,
                    t.PhoneNumber,
                    t.ShabaNo,
                    t.BankName,
                    t.CartNo,
                    DurationMinute = t.ConsultingDurationMinut,
                    t.Description,
                    FileData = _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequest).Select(tt => tt.PictureUrl).FirstOrDefault(),
                    ImageFileData = _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequestProfileImage).Select(tt => tt.PictureUrl).FirstOrDefault(),
                    Prices = t.BecomeConsultantRequestConsultPrices.Select(tt => new CreateBecomeConsultantRequestPriceDTO { Price = tt.Price, Type = tt.Type }).ToList(),
                    Categories = t.Categories.Select(tt => new { id = tt.CategoryId, title = tt.Category.Title }).ToList(),
                    userfullname = t.User.Name + " " + t.User.FamilyName,
                    cityName = t.City.Name,
                    stateName = t.State.Name,
                    StartAndEndWork = t.BecomeConsultantRequestSchedules.Where(tt => tt.DayOfWeek == DayOfWeek.Sunday).ToList()
                })
                .Take(1)
                .ToListAsync())
                .Select(t => new
                {
                    FileData = !string.IsNullOrEmpty(t.FileData) ? ("/Media/Gallery/VideoConsultation/" + t.FileData) : "",
                    ImageFileData = !string.IsNullOrEmpty(t.ImageFileData) ? ("/Media/Gallery/ConsultationUserProfile/" + t.ImageFileData) : "",
                    t.NationalCode,
                    t.MedicalSystemNumber,
                    t.StateId,
                    t.CityId,
                    t.PhoneNumber,
                    t.ShabaNo,
                    t.BankName,
                    t.CartNo,
                    t.DurationMinute,
                    t.Description,
                    t.Prices,
                    t.Categories,
                    t.userfullname,
                    t.cityName,
                    t.stateName,
                    startAndEndWork = t.StartAndEndWork != null && t.StartAndEndWork.Count > 0 ? new List<TimeSpan>() { t.StartAndEndWork.OrderBy(tt => tt.StartTime).Select(tt => tt.StartTime).FirstOrDefault(), t.StartAndEndWork.OrderByDescending(tt => tt.EntTime).Select(tt => tt.EntTime).FirstOrDefault() } : null
                })
                .FirstOrDefault();
        }

        public async Task<IServiceResult> Delete(long requestId)
        {
            var foundItem = await _repository.GetQuery().Where(t => t.Id == requestId).FirstOrDefaultAsync();
            if (foundItem != null)
            {
                if (foundItem.Status == BecomeConsultantRequestStatus.Accepted)
                    return new ServiceResult() { IsSuccess = false, ErrorMessage = "امکان حذف وجود ندارد" };
                await _repository.Delete(foundItem);
                return new ServiceResult() { IsSuccess = true };
            }

            return new ServiceResult() { IsSuccess = false, ErrorMessage = "خطا در انجام عملیات" };
        }
    }
}
