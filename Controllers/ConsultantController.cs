using BanooClub.Models.Consulting;
using BanooClub.Models.DTO;
using BanooClub.Models.Enums;
using BanooClub.Services.ConsultingServices;
using BanooClub.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantController: Controller
    {
        readonly IConsultantService _consultantService;
        readonly IOrderService _orderService;

        public ConsultantController
            (
                IConsultantService consultantService,
                IOrderService orderService
            )
        {
            _consultantService = consultantService;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> Get(long? id)
        {
            return await _consultantService.GetById(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetComments(long? id, int? pageNumber, int? take)
        {
            return await _consultantService.GetComments(id, pageNumber, take);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<object> CreateComment([FromBody] ConsultantNewCommentDTO input)
        {
            return await _consultantService.CreateComment(input);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> CanComment(long? id)
        {
            return await _consultantService.CanComment(id);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<object> CreateVideoConfranceRoom(long? id)
        {
            return await _consultantService.CreateVideoConfranceRoom(id);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<object> CreatePayment(long? id, ConsultTypeEnum? type, TimeSpan? targetTime, DateTime? targetDate)
        {
            return await _consultantService.CreateOrder(id, type, targetTime, targetDate);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<object> GetSchedule([FromQuery] long? id, [FromQuery] List<MyDayOfWeek> days)
        {
            return await _consultantService.GetSchedule(id, days);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<object> ChageStatus(long? id)
        {
            return await _consultantService.ChageStatus(id);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<object> GetMyCansultants()
        {
            return await _orderService.GetMyCansultants();
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<object> GetMyCansultantsForConsulter()
        {
            return await _orderService.GetMyCansultantsForConsulter();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> GetList([FromQuery] ConsultGridFilterDTO input)
        {
            return await _consultantService.GetNotEditedList(input);
        }
    }
}
