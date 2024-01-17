using BanooClub.Models;
using BanooClub.Services.ActivityServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IActivityService activityService;

        public ActivityController(BanooClubDBContext context, IActivityService activityService)
        {
            this.context = context;
            this.activityService = activityService;
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public async Task Create([FromBody] Activity inputDto)
        {
            await this.activityService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetByUserId(long userId,int pageNumber,int count)
        {
            return activityService.GetByUserId(userId,pageNumber,count);
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public object GetMyActivities(int pageNumber, int count)
        {
            return activityService.GetMyActivities(pageNumber,count);
        }

        [HttpPost]
        [Route("[action]"),Authorize(Roles ="Admin")]
        public Task<Activity> Get(long activityId)
        {
            return activityService.Get(activityId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await activityService.Delete(id);
        }
    }
}
