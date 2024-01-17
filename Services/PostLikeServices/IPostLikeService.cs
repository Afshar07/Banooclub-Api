using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PostLikeServices
{
    public interface IPostLikeService
    {
        bool Create(long postId);
        Task<PostLike> Update(PostLike item);
        Task Delete(long id);
        Task<List<PostLike>> GetAll();
        Task<PostLike> Get(long id);
        List<PostLike> GetByUserId(long userId);
        List<User> GetLikesByPostId(long postId);
    }
}
