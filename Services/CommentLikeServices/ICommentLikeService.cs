using BanooClub.Models;
using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.CommentLikeServices
{
    public interface ICommentLikeService
    {
        bool Create(long objectId, CommentType type);
        Task<CommentLike> Update(CommentLike item);
        Task Delete(long id);
        Task<List<CommentLike>> GetAll();
        Task<CommentLike> Get(long id);
        List<CommentLike> GetByUserId(long userId, CommentType type);
        List<User> GetLikesByCommentId(long objectId, CommentType type);
    }
}
