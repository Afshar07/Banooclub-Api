using BanooClub.Models;
using BanooClub.Services.RoomateServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomateController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IRoomateService roomateService;

        public RoomateController(BanooClubDBContext context, IRoomateService roomateService)
        {
            this.context = context;
            this.roomateService = roomateService;
        }

        //[HttpPost]
        //[Route("[action]"), Authorize]
        //public async Task Create([FromBody] Roomate inputDto)
        //{
        //    await roomateService.Create(inputDto);
        //}

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<Roomate> Update([FromBody] Roomate inputDto)
        {
            var result = await this.roomateService.Update(inputDto);
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<List<Roomate>> GetAll(long? mortgageFrom, long? mortgageTo, long? dailyRentFrom, long? dailyRentTo)
        {
            return await roomateService.GetAll(mortgageFrom, mortgageTo, dailyRentFrom, dailyRentTo);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<List<Roomate>> GetAllForAdmin(long? mortgageFrom, long? mortgageTo, long? dailyRentFrom, long? dailyRentTo)
        {
            return await roomateService.GetAllForAdmin(mortgageFrom, mortgageTo, dailyRentFrom, dailyRentTo);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<object> Get(long id)
        {
            return roomateService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public Task<object> GetByUserId(long userId)
        {
            return roomateService.GetByUserId(userId);
        }
        [HttpPost]
        [Route("[action]"), Authorize]
        public Task<object> GetMyRoomate()
        {
            return roomateService.GetMyRoomate();
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task Delete(long id)
        {
            await roomateService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<object> ChangeRoomateActivation(bool state)
        {
            return await roomateService.ChangeRoomateActivation(state);
        }

        //[HttpPost]
        //[Route("[action]"),Authorize]
        //public int CheckAvailablityForActivation()
        //{
        //    return roomateService.CheckAvailablityForActivation();
        //}
    }
}
