using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.LikeServices
{
    public class LikeService : ILikeService
    {
        private readonly IBanooClubEFRepository<Like> likeRepository;
        private readonly IHttpContextAccessor _accessor;

        public LikeService(IBanooClubEFRepository<Like> likeRepository, IHttpContextAccessor accessor)
        {
            this.likeRepository = likeRepository;
            _accessor = accessor;
        }
        public async Task<LikeResponse> Create(Like inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            var dbCommentLike = likeRepository.GetQuery().FirstOrDefault(z => z.UserId == userId && z.ObjectId == inputDto.ObjectId && z.Type == inputDto.Type);
            if (dbCommentLike == null)
            {
                likeRepository.Insert(inputDto);
                return LikeResponse.Success;
            }
            else
            {
                if (inputDto.Status == LikeStatus.Like)
                {
                    if (dbCommentLike.Status == LikeStatus.Like)
                    {
                        return LikeResponse.HasAlreadyLiked;
                    }
                    else
                    {
                        dbCommentLike.Status = LikeStatus.Like;
                    }
                }
                if (inputDto.Status == LikeStatus.DisLike)
                {
                    if (dbCommentLike.Status == LikeStatus.DisLike)
                    {
                        return LikeResponse.HasAlreadyDisliked;
                    }
                    else
                    {
                        dbCommentLike.Status =LikeStatus.DisLike;
                    }
                }
                await likeRepository.Update(dbCommentLike);
                return LikeResponse.Success;
            }

        }

        public async Task<Like> Update(Like item)
        {
            await likeRepository.Update(item);
            return item;
        }

        public async Task<List<Like>> GetAll()
        {
            List<Like> commentLikes = new List<Like>();
            commentLikes = likeRepository.GetQuery().ToList();
            return commentLikes;
        }

        public async Task Delete(long id)
        {
            var commentLike = likeRepository.GetQuery().FirstOrDefault(z => z.LikeId == id);
            await likeRepository.Delete(commentLike);
        }

        public async Task<Like> Get(long id)
        {
            var commentLike = likeRepository.GetQuery().FirstOrDefault(z => z.LikeId == id);
            return commentLike;
        }
    }
}
