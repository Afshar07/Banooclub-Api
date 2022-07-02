using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.AdsCommentServices
{
    public interface IAdsCommentService
    {
        Task Create(AdsComment inputDto);
        Task<AdsComment> Update(AdsComment item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count);
        Task<AdsComment> Get(long id);
    }
}
