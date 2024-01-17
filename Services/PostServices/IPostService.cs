using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PostServices
{
    public interface IPostService
    {
        Task Create(Post inputDto);
        Task<Post> Update(Post item);
        Task<bool> Delete(params long[] ids);
        object GetAll(int pageNumber, int count);
        Task<Post> Get(long id);
        Task<long> UpdateByCmd(string title, string description, long postId, int status);
        //object GetMyPost(int pageNumber, int count);
        object GetMyPost(long lastId, int count);
        IServiceResult<object> GetByUserId(long userId, long lastId, int count);
        IServiceResult<object> GetByUserName(string userName, long lastId, int count);
        //Task<List<PostDTO>> GetFollowingPosts();
        object GetNotConfirmed(int pageNumber, int count, string search);
        object GetReported(int pageNumber, int count);
        Task<Post> ChangePostStatus(Post inputDto);
        Task<bool> ChangePostStatusForAdmin(PostStatus status, params long[] ids);
        object GetAllByInfiniteScroll(long lastId, int count, string search);
        Task<object> GetFollowingPosts(long lastId, int count);
    }
}
