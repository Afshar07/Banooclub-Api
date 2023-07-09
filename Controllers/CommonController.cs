using BanooClub.Services.CommonServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService commonService;
        public CommonController(ICommonService commonService)
        {
            this.commonService = commonService;
        }
        
        [HttpPost]
        [Route("[action]"),Authorize]
        public object GetIndexData()
        {
            return commonService.GetIndexData();
        }

        [HttpPost]
        [Route("[action]") ]
        public object GetUserIndex(long userId)
        {
            return commonService.GetUserIndex(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public object GetUserIndexByUserName(string userName)
        {
            return commonService.GetUserIndexByUserName(userName);
        }
    }
}
