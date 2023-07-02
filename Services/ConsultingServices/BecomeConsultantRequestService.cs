using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using BanooClub.Services.SocialMediaServices;
using BanooClub.Utilities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private BanooClubDBContext _dBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
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
                IBanooClubEFRepository<ConsultantSchedule> consultantScheduleRepository
            )
        {
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
        }

        public async Task<IServiceResult> CreateBecomeConsultantRequest(CreateBecomeConsultantRequestDTO dto)
        {
            var operation = new ServiceResult();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            string errorResult = createUpdateValidation(dto, userId);
            if (string.IsNullOrEmpty(errorResult))
            {
                var request = new BecomeConsultantRequest(userId, dto.NationalCode, dto.MedicalSystemNumber, dto.StateId.Value, dto.CityId.Value, dto.PhoneNumber, dto.ShabaNo, dto.BankName, dto.CartNo, dto.Description, dto.DurationMinut);
                await _repository.InsertAsync(request);
                if (dto.Categories != null && dto.Categories.Count > 0)
                    foreach (var category in dto.Categories)
                        await _becomeConsultantRequestConsultCategoryRepository.InsertAsync(new BecomeConsultantRequestConsultCategory() { CategoryId = category, RequestId = request.Id });
                if (dto.Prices != null && dto.Prices.Count > 0)
                    foreach (var price in dto.Prices)
                        await _becomeConsultantRequestConsultPriceRepository.InsertAsync(new BecomeConsultantRequestConsultPrice() { BecomeConsultantRequestId = request.Id, Price = price.Price.Value, Type = price.Type.Value });

                var listDurations = await _becomeConsultantRequestScheduleService.CreateCleanIfExist(dto.DurationMinut, request.Id);
                if (dto.SelectedStartedTimes != null && dto.SelectedStartedTimes.Count > 0)
                {
                    var startTime = dto.SelectedStartedTimes[0];
                    var endTime = dto.SelectedStartedTimes[1];
                    foreach (var curTime in listDurations)
                    {
                        if (curTime.StartTime >= startTime && curTime.StartTime <= endTime && curTime.EntTime >= startTime && curTime.EntTime <= endTime)
                        {
                            curTime.IsAvailable = true;
                            await _becomeConsultantRequestScheduleRepository.Update(curTime);
                        }
                    }
                }

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

                var outPutImage = _mediaService.SaveImage(dto.ImageFileData, EntityUrls.ConsultationUserProfile);
                if (outPutImage != null && outPut.IsSuccess == true)
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
            if (dto.DurationMinut == null || dto.DurationMinut <= 0)
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
            if (string.IsNullOrEmpty(dto.FileData))
                return "لطفا فایل معرفی خود را وارد کنید";
            var allDuration = GetDuration();
            if (!allDuration.Any(t => t.Id == dto.DurationMinut))
                return "مدت زمان انتخاب شده مجاز نمی باشد";
            if (dto.SelectedStartedTimes == null || dto.SelectedStartedTimes.Count != 2)
                return "لطفا بازه شروع و پایان زمانی انتخاب کنید";

            var startDate = dto.SelectedStartedTimes[0];
            var endDate = dto.SelectedStartedTimes[1];
            if ((endDate - startDate).TotalMinutes <= 0)
                return "زمان شروع و پایان صحیح نمی باشد";

            if ((endDate - startDate).TotalMinutes % dto.DurationMinut != 0)
                return "زمان پایان صحیح نمی باشد";

            return "";
        }

        public async Task<IServiceResult> AcceptRequestAndCreateConsultant(long requestId)
        {
            var operation = new ServiceResult();

            var request = await _repository
                .GetQuery()
                .Where(x => x.Id == requestId)
                .Include(t => t.Categories)
                .Include(t => t.BecomeConsultantRequestConsultPrices)
                .Include(t => t.BecomeConsultantRequestSchedules)
                .FirstOrDefaultAsync();

            if (request == null)
                return operation.Failure("NotFound");

            if (request.Status == BecomeConsultantRequestStatus.Accepted)
                return operation.Failure("AlreadyAccepted");


            request.Status = BecomeConsultantRequestStatus.Accepted;

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
                            while (allPrevScheduale.Count > 0)
                                _consultantScheduleRepository.Erase(allPrevScheduale[0]);

                        foreach (var item in request.BecomeConsultantRequestSchedules)
                            await _consultantScheduleRepository.InsertAsync(new ConsultantSchedule()
                            {
                                ConsultantId = consultant.Id,
                                CreateDate = DateTime.Now,
                                EntTime = item.EntTime,
                                IsAvailable = item.IsAvailable,
                                StartTime = item.StartTime,
                                IsDeleted = item.IsDeleted,
                            });
                    }

                }

                await _dBContext.Database.CommitTransactionAsync();
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

            var request = await _repository.GetQuery().Where(x => x.Id == requestId).FirstOrDefaultAsync();
            if (request == null)
                return operation.Failure("NotFound");

            request.Status = BecomeConsultantRequestStatus.Rejected;

            await _repository.Update(request);

            return operation.Ok();
        }

        public async Task<IServiceResult<PageModel<BecomeConsultantRequestDTO>>> GetRequests(int page, int size, BecomeConsultantRequestStatus? status)
        {

            var operation = new ServiceResult<PageModel<BecomeConsultantRequestDTO>>();

            var query = _repository.GetQuery();

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            var skip = (page - 1) * size;
            var totalCount = await query.CountAsync();
            var data =
                await
                query
                .Select(x => new BecomeConsultantRequestDTO
                {
                    BankName = x.BankName,
                    CartNo = x.CartNo,
                    City = x.City.Name,
                    State = x.State.Name,
                    CityId = x.CityId,
                    Id = x.Id,
                    StateId = x.StateId,
                    PhoneNumber = x.PhoneNumber,
                    ShabaNo = x.ShabaNo,
                    NatiaonCode = x.NationalCode,
                    MedicalSystemNumber = x.MedicalSystemNumber,
                    UserFullName = x.User.Name + " " + x.User.FamilyName,
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
                new IdTitleKeyValueDTO() { Id = 5, Title = "پنج دقیقه" },
                new IdTitleKeyValueDTO() { Id = 10, Title = "ده دقیقه" },
                new IdTitleKeyValueDTO() { Id = 15, Title = "پانزده دقیقه" },
                new IdTitleKeyValueDTO() { Id = 20, Title = "بیست دقیقه" },
                new IdTitleKeyValueDTO() { Id = 25, Title = "بیست و پنج دقیقه" },
                new IdTitleKeyValueDTO() { Id = 30, Title = "سی دقیقه" },
                new IdTitleKeyValueDTO() { Id = 35, Title = "سی و پنج دقیقه" },
                new IdTitleKeyValueDTO() { Id = 40, Title = "چهل دقیقه" },
                new IdTitleKeyValueDTO() { Id = 45, Title = "چهل و پنج دقیقه" },
                new IdTitleKeyValueDTO() { Id = 50, Title = "پنجاه دقیقه" },
                new IdTitleKeyValueDTO() { Id = 55, Title = "پنجاه و پنج دقیقه" },
                new IdTitleKeyValueDTO() { Id = 60, Title = "شست دقیقه" }

            };
        }

        public async Task<CreateBecomeConsultantRequestDTO> GetLast()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();

            return await _repository
                .GetQuery()
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreateDate)
                .Select(t => new CreateBecomeConsultantRequestDTO
                {
                    NationalCode = t.NationalCode,
                    MedicalSystemNumber = t.MedicalSystemNumber,
                    StateId = t.StateId,
                    CityId = t.CityId,
                    PhoneNumber = t.PhoneNumber,
                    ShabaNo = t.ShabaNo,
                    BankName = t.BankName,
                    CartNo = t.CartNo,
                    DurationMinut = t.ConsultingDurationMinut,
                    Description = t.Description,
                    FileData = "/Media/Gallery/VideoConsultation/" + _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequest).Select(tt => tt.PictureUrl).FirstOrDefault(),
                    ImageFileData = "/Media/Gallery/ConsultationUserProfile/" + _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequestProfileImage).Select(tt => tt.PictureUrl).FirstOrDefault(),
                    Prices = t.BecomeConsultantRequestConsultPrices.Select(tt => new CreateBecomeConsultantRequestPriceDTO { Price = tt.Price, Type = tt.Type }).ToList(),
                    Categories = t.Categories.Select(tt => tt.CategoryId).ToList(),
                    SelectedStartedTimes = t.BecomeConsultantRequestSchedules.Where(tt => tt.IsAvailable == true).Select(tt => tt.StartTime).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CreateBecomeConsultantRequestDTO> GetById(long id)
        {
            return await _repository
               .GetQuery()
               .Where(t => t.Id == id)
               .Select(t => new CreateBecomeConsultantRequestDTO
               {
                   NationalCode = t.NationalCode,
                   MedicalSystemNumber = t.MedicalSystemNumber,
                   StateId = t.StateId,
                   CityId = t.CityId,
                   PhoneNumber = t.PhoneNumber,
                   ShabaNo = t.ShabaNo,
                   BankName = t.BankName,
                   CartNo = t.CartNo,
                   DurationMinut = t.ConsultingDurationMinut,
                   Description = t.Description,
                   FileData = "/Media/Gallery/VideoConsultation/" + _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequest).Select(tt => tt.PictureUrl).FirstOrDefault(),
                   ImageFileData = "/Media/Gallery/ConsultationUserProfile/" + _mediaRepository.GetQuery().Where(tt => tt.ObjectId == t.Id && tt.Type == MediaTypes.ConsultationRequestProfileImage).Select(tt => tt.PictureUrl).FirstOrDefault(),
                   Prices = t.BecomeConsultantRequestConsultPrices.Select(tt => new CreateBecomeConsultantRequestPriceDTO { Price = tt.Price, Type = tt.Type }).ToList(),
                   Categories = t.Categories.Select(tt => tt.CategoryId).ToList(),
                   SelectedStartedTimes = t.BecomeConsultantRequestSchedules.Where(tt => tt.IsAvailable == true).Select(tt => tt.StartTime).ToList()
               })
               .FirstOrDefaultAsync();
        }
    }
}
