using BanooClub.Models;
using BanooClub.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PostServices
{
    public interface IPostService
    {
        Task Create(Post inputDto);
        Task<Post> Update(Post item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count);
        Task<Post> Get(long id);
        Task<long> UpdateByCmd(string content, long postId);
        object GetMyPost(int pageNumber, int count);
        IServiceResult<object> GetByUserId(long userId);
        Task<List<PostDTO>> GetFollowingPosts();
        object GetNotConfirmed(int pageNumber, int count, string search);
        object GetReported(int pageNumber, int count);
        Task<Post> ChangePostStatus(Post item);
    }
}
