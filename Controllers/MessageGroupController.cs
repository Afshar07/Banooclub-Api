﻿using BanooClub.Models;
using BanooClub.Services.GroupServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageGroupController : ControllerBase
    {
        private readonly IGroupService groupService;

        public MessageGroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Group inputDto)
        {
            await this.groupService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Group> Get(long logId)
        {
            return groupService.Get(logId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await groupService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<List<Group>> GetAll()
        {
            return await groupService.GetAll();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<bool> ChangeGroupStatus(long groupId)
        {
            return await groupService.ChangeGroupStatus(groupId);
        }
    }
}
