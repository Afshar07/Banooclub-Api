using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ForumCategoryServices
{
    public interface IForumCategoryService
    {
        Task<long> Create(ForumCategory inputDto);
        Task<ForumCategory> Update(ForumCategory item);
        object GetAll(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<ForumCategory> Get(long id);
    }
}
