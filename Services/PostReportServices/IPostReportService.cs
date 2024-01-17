using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PostReportServices
{
    public interface IPostReportService
    {
        Task Create(PostReport inputDto);
        Task<PostReport> Update(PostReport item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count,string search);
        Task<object> Get(long id);
        List<PostReport> GetByUserId(long userId);
    }
}
