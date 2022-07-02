using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ForumServices
{
    public interface IForumService
    {
        Task<long> Create(Forum inputDto);
        Task<Forum> Update(Forum item);
        Task<object> GetAll(int pageNumber, int count, string searchCommand);
        Task<object> GetMyForums(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<Forum> Get(long id);
    }
}
