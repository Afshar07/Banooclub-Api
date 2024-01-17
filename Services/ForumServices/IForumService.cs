using BanooClub.Models;
using BanooClub.Models.Enums;
using System.Threading.Tasks;

namespace BanooClub.Services.ForumServices
{
    public interface IForumService
    {
        Task<long> Create(Forum inputDto);
        Task<Forum> Update(Forum item);
        Task<Forum> ReportForum(long forumId);
        Task<Forum> ChangeForumStatus(long forumId, ForumStatus status);
        Task<object> GetByUserId(long userId, byte count, short pageNumber);

        Task<object> GetAll(int pageNumber, int count, string searchCommand, bool? noComments, 
            bool? mostRated,bool? mostComments, long? categoryId, bool? mostViewed);

        Task<object> GetAllForAdmin(int pageNumber, int count, string searchCommand, bool? noComments, bool? mostRated,
            bool? mostComments, ForumStatus? status, long? categoryId, bool? mostViewed);

        Task<object> GetMyForums(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<Forum> Get(long id);
    }
}
