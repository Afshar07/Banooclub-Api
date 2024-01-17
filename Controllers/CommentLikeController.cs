using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.CommentLikeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BanooClub.Models.Enums;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentLikeController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly ICommentLikeService commentLikeService;

        public CommentLikeController(BanooClubDBContext context, ICommentLikeService commentLikeService)
        {
            this.context = context;
            this.commentLikeService = commentLikeService;
        }

        [HttpPost]
        [Route("[action]")]
        public bool Create(long objectId, CommentType type)
        {
            return commentLikeService.Create(objectId, type);
        }

        [HttpPost]
        [Route("[action]")]
        public List<CommentLike> GetByUserId(long userId, CommentType type)
        {
            return commentLikeService.GetByUserId(userId, type);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<CommentLike> Get(long commentLikeId)
        {
            return commentLikeService.Get(commentLikeId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await commentLikeService.Delete(id);
        }
        [HttpPost]
        [Route("[action]")]
        public List<User> GetLikesByCommentId(long objectId, CommentType type)
        {
            return commentLikeService.GetLikesByCommentId(objectId, type);
        }
    }
}
