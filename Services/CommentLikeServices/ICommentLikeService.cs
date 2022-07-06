using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.CommentLikeServices
{
    public interface ICommentLikeService
    {
        bool Create(long postId);
        Task<CommentLike> Update(CommentLike item);
        Task Delete(long id);
        Task<List<CommentLike>> GetAll();
        Task<CommentLike> Get(long id);
        List<CommentLike> GetByUserId(long userId);
        List<User> GetLikesByCommentId(long postId);
    }
}
