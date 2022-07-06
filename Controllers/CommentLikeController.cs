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

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentLikeController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly ICommentLikeService postLikeService;

        public CommentLikeController(BanooClubDBContext context, ICommentLikeService postLikeService)
        {
            this.context = context;
            this.postLikeService = postLikeService;
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public bool Create(long postId)
        {
            return this.postLikeService.Create(postId);
        }

        [HttpPost]
        [Route("[action]")]
        public List<CommentLike> GetByUserId(long userId)
        {
            return postLikeService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<CommentLike> Get(long postLikeId)
        {
            return postLikeService.Get(postLikeId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await postLikeService.Delete(id);
        }
        [HttpPost]
        [Route("[action]")]
        public List<User> GetLikesByPostId(long postId)
        {
            return postLikeService.GetLikesByCommentId(postId);
        }
    }
}
