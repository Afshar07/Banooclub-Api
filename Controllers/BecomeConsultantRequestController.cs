﻿using BanooClub.Models.Consulting;
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

        public BecomeConsultantRequestController
            (
                IBecomeConsultantRequestService becomeConsultantRequestService
            )
        {
            _becomeConsultantRequestService = becomeConsultantRequestService;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<IServiceResult>> CreateRequest(CreateBecomeConsultantRequestDTO dto)
        {
            var result = await _becomeConsultantRequestService.CreateBecomeConsultantRequest(dto);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<IServiceResult>> Get()
        {
            var result = await _becomeConsultantRequestService.GetLast();
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<IServiceResult>> GetById(long Id)
        {
            var result = await _becomeConsultantRequestService.GetById(Id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public ActionResult GetDurationList()
        {
            return Ok(_becomeConsultantRequestService.GetDuration());
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IServiceResult>> AcceptRequest(long requestId)
        {
            var result = await _becomeConsultantRequestService.AcceptRequestAndCreateConsultant(requestId);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IServiceResult>> RejectRequest(long requestId)
        {
            var result = await _becomeConsultantRequestService.RejectRequest(requestId);
            return Ok(result);
        }

        //[HttpPost("[action]")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<IServiceResult>> Delete(long requestId)
        //{
        //    var result = await _becomeConsultantRequestService.Delete(requestId);
        //    return Ok(result);
        //}

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IServiceResult>> GetRequests(int page = 1, int size = 10, BecomeConsultantRequestStatus? status = null)
        {
            var result = await _becomeConsultantRequestService.GetRequests(page, size, status);
            return Ok(result);
        }
    }
}
