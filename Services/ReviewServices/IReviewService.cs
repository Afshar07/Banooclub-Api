using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ReviewServices
{
    public interface IReviewService
    {
        Task Create(Review inputDto);
        Task<Review> Update(Review item);
        Task Delete(long id);
        Task<List<Review>> GetAll();
        Task<Review> Get(long id);
        List<Review> GetByUserId(long userId);
    }
}
