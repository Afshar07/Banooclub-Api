using BanooClub.Models.DTO;
using BanooClub.Services.ConsultantUserSchedulesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantUserSchedulesController : Controller
    {
        readonly IConsultantUserSchedulesService _consultantUserSchedulesService;
        public ConsultantUserSchedulesController
            (
                IConsultantUserSchedulesService consultantUserSchedulesService
            )
        {
            _consultantUserSchedulesService = consultantUserSchedulesService;
        }
        
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<object> Get(long? id)
        {
            return await _consultantUserSchedulesService.GetById(id);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<object> GetList([FromQuery] ConsultantUserSchedulesGridFilterDTO input)
        {
            return await _consultantUserSchedulesService.GetList(input);
        }
    }
}
