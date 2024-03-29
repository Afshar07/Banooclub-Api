﻿using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.ForumServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumService forumService;

        public ForumController(IForumService forumService)
        {
            this.forumService = forumService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] Forum inputDto)
        {
            return await this.forumService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Forum> Update([FromBody] Forum inputDto)
        {
            return await this.forumService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<Forum> ReportForum(long forumId)
        {
            return await this.forumService.ReportForum(forumId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<Forum> ChangeForumStatus(long forumId, ForumStatus status)
        {
            return await this.forumService.ChangeForumStatus(forumId, status);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<Forum> Get(long forumId)
        {
            return await forumService.Get(forumId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await forumService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetAll(int pageNumber, int count, string searchCommand, bool? noComments,
            bool? mostRated, bool? mostComments, long? categoryId, bool? mostViewed)
        {
            return await forumService.GetAll(pageNumber, count, searchCommand, noComments,
                mostRated, mostComments, categoryId, mostViewed);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<object> GetByUserId(long userId, byte count, short pageNumber)
        {
            return await forumService.GetByUserId(userId, count, pageNumber);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<object> GetMyForums(int pageNumber, int count, string searchCommand)
        {
            return await forumService.GetMyForums(pageNumber, count, searchCommand);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<object> GetAllForAdmin(int pageNumber, int count, string searchCommand, bool? noComments,
            bool? mostRated, bool? mostComments, ForumStatus? status, long? categoryId, bool? mostViewed)
        {
            return await forumService.GetAllForAdmin(pageNumber, count, searchCommand, noComments, mostRated,
                mostComments, status, categoryId, mostViewed);
        }
    }
}
