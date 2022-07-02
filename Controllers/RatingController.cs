using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.RatingServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost]
        [Route("[action]")]
        public long Create( int rate,long objectId,RatingType type)
        {
            return   this.ratingService.CreateObjectRate(rate,objectId,type).Result.Data;
        }

        [HttpPost]
        [Route("[action]")]
        public Rating Update([FromBody] Rating inputDto)
        {
            return  this.ratingService.UpdateProductRate(inputDto).Result.Data;
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public RateOveral Get(long objectId,RatingType type)
        {
            return  ratingService.GetByObjectIdAndType(objectId,type).Result.Data;
        }

        [HttpPost]
        [Route("[action]"),Authorize(Roles ="Admin")]
        public bool Delete(long id)
        {
            return ratingService.DeleteProductRate(id).Result.Data;
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll()
        {
            return  ratingService.GetAll();
        }
    }
}
