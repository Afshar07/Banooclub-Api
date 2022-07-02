using BanooClub.Models;
using BanooClub.Services.UserTypeServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserTypeController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IUserTypeService userTypeService;

        public UserTypeController(BanooClubDBContext context, IUserTypeService userTypeService)
        {
            this.context = context;
            this.userTypeService = userTypeService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] UserType inputDto)
        {
            await this.userTypeService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<UserType> Update([FromBody] UserType inputDto)
        {
            var result = await this.userTypeService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<UserType>> GetAll()
        {
            return userTypeService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<UserType> Get([FromBody] long id)
        {
            return userTypeService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await userTypeService.Delete(id);
        }

        [HttpPost]
        [Route("[action]")]
        public List<UserType> GetByCommand(string sql)
        {
            return userTypeService.GetByCommand(sql).ToList();
        }
        [HttpPost]
        [Route("[action]")]
        public object UpdateByCmd(string name, int id)
        {
            var result = userTypeService.UpdateByCmd(name, id);
            return id;
        }
    }
}
