using BanooClub.Models.DTO;
using BanooClub.Services.ConsultingServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantUserScheduleRatingController : Controller
    {
        readonly IConsultantUserScheduleRatingService _consultantUserScheduleRatingService;
        public ConsultantUserScheduleRatingController
            (
                IConsultantUserScheduleRatingService consultantUserScheduleRatingService
            )
        {
            _consultantUserScheduleRatingService = consultantUserScheduleRatingService;
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<object> Get(long? id)
        {
            return await _consultantUserScheduleRatingService.GetById(id);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<object> Confirm(long? id)
        {
            return await _consultantUserScheduleRatingService.Confirm(id);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<object> Delete(long? id)
        {
            return await _consultantUserScheduleRatingService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<object> GetList([FromQuery] ConsultantUserScheduleRatingGridFilterDTO input)
        {
            return await _consultantUserScheduleRatingService.GetList(input);
        }
    }
}
