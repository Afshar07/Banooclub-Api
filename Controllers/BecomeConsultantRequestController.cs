using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices;
using BanooClub.Services.ConsultingServices.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BecomeConsultantRequestController : ControllerBase
    {

        private readonly IBecomeConsultantRequestService _becomeConsultantRequestService;

        public BecomeConsultantRequestController(IBecomeConsultantRequestService becomeConsultantRequestService)
        {
            _becomeConsultantRequestService = becomeConsultantRequestService;
        }

        [HttpPost]
        public async Task<ActionResult<IServiceResult>> CreateRequest(CreateBecomeConsultantRequestDTO dto)
        {
            var result = await _becomeConsultantRequestService.CreateBecomeConsultantRequest(dto);
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IServiceResult>> AcceptRequest(long requestId)
        {
            var result = await _becomeConsultantRequestService.AcceptRequestAndCreateConsultant(requestId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IServiceResult>> RejectRequest(long requestId)
        {
            var result = await _becomeConsultantRequestService.RejectRequest(requestId);
            return Ok(result);
        }

    }
}
