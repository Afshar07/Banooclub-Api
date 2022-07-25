using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ForumCommentServices
{
    public interface IForumCommentService
    {
        Task<long> Create(ForumComment inputDto);
        Task<ForumComment> Update(ForumComment item);
        object GetAll(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<ForumComment> Get(long id);
        Task<object> TopCommenters(int count);
    }
}
