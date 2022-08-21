using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PostCommentServices
{
    public interface IPostCommentService
    {
        Task Delete(long id);
        Task Create(PostComment inputDto);
        Task<PostComment> Update(PostComment item);
        Task<byte> ChangePostCommentActivation(long postId);
        Task<List<PostComment>> GetAll();
        Task<PostComment> Get(long id);
        List<PostComment> GetByUserId(long userId);
        List<PostComment> GetByPostId(long postId);
        List<PostComment> GetChildrenByPostId(long postId, long commnetId);
    }
}
