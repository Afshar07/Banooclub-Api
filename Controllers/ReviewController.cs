using BanooClub.Models;
using BanooClub.Services.ReviewServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IReviewService reviewService;

        public ReviewController(BanooClubDBContext context, IReviewService reviewService)
        {
            this.context = context;
            this.reviewService = reviewService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Review inputDto)
        {
            await this.reviewService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public List<Review> GetByUserId(long userId)
        {
            return reviewService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Review> Get(long reviewId)
        {
            return reviewService.Get(reviewId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await reviewService.Delete(id);
        }
    }
}
