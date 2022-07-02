using BanooClub.Models;
using BanooClub.Services.MappingServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        public MapController()
        {

        }


        [HttpPost, AllowAnonymous]
        [Route("[action]")]
        public dataDTO GetPositionStack(double lan, double lon)
        {
            var mapService = new MapService(new HttpClient());
            return mapService.GetPositionStack(lan, lon);
        }
        [HttpPost, AllowAnonymous]
        [Route("[action]")]
        public string BigDataReverseGeoCoding(double lan, double lon)
        {
            var mapService = new MapService(new HttpClient());
            return mapService.BigDataReverseGeoCoding(lan, lon);
        }
        [HttpPost, AllowAnonymous]
        [Route("[action]")]
        public string FourSquareReverseGeoCoding(double lan, double lon)
        {
            var mapService = new MapService(new HttpClient());
            return mapService.FourSquareReverseGeoCoding(lan, lon);
        }
    }
}
