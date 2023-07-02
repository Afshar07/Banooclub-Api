using BanooClub.Models.Consulting;
using BanooClub.Services.ConsultingServices;
using BanooClub.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<object> GetSchedule(long? id)
        {
            return await _consultantService.GetSchedule(id);
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
        public async Task<object> GetList()
        {
            return await _consultantService.GetNotEditedList();
        }
    }
}
