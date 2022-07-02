using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.UserCommentServices
{
    public interface IUserCommentService
    {
        Task Create(UserComment inputDto);
        Task<UserComment> Update(UserComment item);
        Task Delete(long id);
        Task<List<UserComment>> GetAll();
        Task<UserComment> Get(long id);
    }
}
