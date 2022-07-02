using BanooClub.Services.SmsServices;
using Microsoft.AspNetCore.Mvc;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly ISmsSenderService smsService;
        public SMSController(ISmsSenderService smsService)
        {
            this.smsService = smsService;
        }
        [HttpPost]
        [Route("[action]")]
        public object VerifyLookUp(string mobile, string token)
        {
            return smsService.VerifyLookUp(mobile, token);
        }
        [HttpPost]
        [Route("[action]")]
        public object KavenegarSms(string mobile, string message)
        {
            return smsService.KavenegarSms(mobile, message);
        }
    }
}
