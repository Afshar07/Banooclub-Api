using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PostNKServices
{
    public interface IPostNKService
    {
        Task Create(PostNK inputDto);
        Task<PostNK> Update(PostNK item);
        Task Delete(long id);
        Task<object> GetAll(int pageNumber, int count,string search);
        Task<PostNK> Get(long id);
    }
}
