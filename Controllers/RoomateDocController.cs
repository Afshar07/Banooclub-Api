using BanooClub.Models;
using BanooClub.Services.RoomateDocServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomateDocController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IRoomateDocService roomateDocService;

        public RoomateDocController(BanooClubDBContext context, IRoomateDocService roomateDocService)
        {
            this.context = context;
            this.roomateDocService = roomateDocService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] RoomateDoc inputDto)
        {
            await roomateDocService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public RoomateDoc GetByUserId(long userId)
        {
            return roomateDocService.GetByUserId(userId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public Task<RoomateDoc> Get(long roomateDocId)
        {
            return roomateDocService.Get(roomateDocId);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Delete(long id)
        {
            await roomateDocService.Delete(id);
        }
        [HttpPost]
        [Route("[action]")]
        public Task<RoomateDoc> Update(RoomateDoc inputDto)
        {
            return roomateDocService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public Task<RoomateDoc> ChangeStatus(long roomateDocId, string StateDesc, int status)
        {
            return roomateDocService.ChangeStatus(roomateDocId, StateDesc, status);
        }
        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetNotConfirmed(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return roomateDocService.GetNotConfirmed(pageNumber,count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetRejected(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return roomateDocService.GetRejected(pageNumber,count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetAll(int pageNumber, int count,string search)
        {
            search = search ==null ? "" : search;
            return roomateDocService.GetAll(pageNumber,count,search);
        }
    }
}
