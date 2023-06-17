using BanooClub.Extentions;
using BanooClub.Models.Consulting;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public class BecomeConsultantRequestService : IBecomeConsultantRequestService
    {
        private readonly IBanooClubEFRepository<BecomeConsultantRequest> _repository;
        private readonly IBanooClubEFRepository<Consultant> _consultantRepository;
        private BanooClubDBContext _dBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BecomeConsultantRequestService(IBanooClubEFRepository<BecomeConsultantRequest> repository, IHttpContextAccessor httpContextAccessor, IBanooClubEFRepository<Consultant> consultantRepository, BanooClubDBContext dBContext)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _consultantRepository = consultantRepository;
            _dBContext = dBContext;
        }

        public async Task<IServiceResult> CreateBecomeConsultantRequest(CreateBecomeConsultantRequestDTO dto)
        {
            var operation = new ServiceResult();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var request = new BecomeConsultantRequest(userId, dto.NatiaonCode, dto.MedicalSystemNumber, dto.StateId, dto.CityId, dto.PhoneNumber, dto.ShabaNo, dto.BankName, dto.CartNo);
            await _repository.InsertAsync(request);
            return operation.Ok();
        }
        public async Task<IServiceResult> AcceptRequestAndCreateConsultant(long requestId)
        {
            var operation = new ServiceResult();

            var request = await _repository.GetQuery().Where(x => x.Id == requestId).FirstOrDefaultAsync();
            if (request == null)
                return operation.Failure("NotFound");

            if (request.Status == BecomeConsultantRequestStatus.Accepted)
                return operation.Failure("AlreadyAccepted");


            request.Status = BecomeConsultantRequestStatus.Accepted;

            await _dBContext.Database.BeginTransactionAsync();

            try
            {
                await _repository.Update(request);

                var consultant = _consultantRepository.GetQuery().Where(x => x.UserId == request.UserId).FirstOrDefault();
                if (consultant == null)
                {
                    consultant = new Consultant(request.UserId, request.NationalCode, request.MedicalSystemNumber, request.StateId, request.CityId, request.PhoneNumber, request.ShabaNo, request.BankName, request.CartNo);
                    await _consultantRepository.InsertAsync(consultant);
                }
                else
                {
                    consultant.Update(request.NationalCode, request.MedicalSystemNumber, request.StateId, request.CityId, request.PhoneNumber, request.ShabaNo, request.BankName, request.CartNo);
                    await _consultantRepository.Update(consultant);
                }
                await _dBContext.Database.CommitTransactionAsync();
            }
            catch (System.Exception)
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

    }

    public interface IBecomeConsultantRequestService
    {
        Task<IServiceResult> AcceptRequestAndCreateConsultant(long requestId);
        Task<IServiceResult> CreateBecomeConsultantRequest(CreateBecomeConsultantRequestDTO dto);
        Task<IServiceResult> RejectRequest(long requestId);
    }
}
