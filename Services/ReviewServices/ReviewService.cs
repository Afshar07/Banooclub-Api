using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly IBanooClubEFRepository<Review> reviewRepository;

        public ReviewService(IBanooClubEFRepository<Review> reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task Create(Review inputDto)
        {
            reviewRepository.Insert(inputDto);
        }

        public async Task<Review> Update(Review item)
        {
            await reviewRepository.Update(item);
            return item;
        }

        public async Task<List<Review>> GetAll()
        {
            List<Review> reviews = new List<Review>();
            reviews = reviewRepository.GetQuery().ToList();
            return reviews;
        }

        public async Task Delete(long id)
        {
            var review = reviewRepository.GetQuery().FirstOrDefault(z => z.ReviewId == id);
            await reviewRepository.Delete(review);
        }

        public async Task<Review> Get(long id)
        {
            var review = reviewRepository.GetQuery().FirstOrDefault(z => z.ReviewId == id);
            return review;
        }
        public List<Review> GetByUserId(long userId)
        {
            return reviewRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }
    }
}
