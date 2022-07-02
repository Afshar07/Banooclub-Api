using BanooClub.Models;
using BanooClub.Services.PackageServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IPackageService packageService;

        public PackageController(BanooClubDBContext context, IPackageService packageService)
        {
            this.context = context;
            this.packageService = packageService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Create([FromBody] Package inputDto)
        {
            await this.packageService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Package> Update([FromBody] Package inputDto)
        {
            var result = await this.packageService.Update(inputDto);
            return result;
        }



        [HttpPost]
        [Route("[action]")]
        public Task<List<Package>> GetAll()
        {
            return packageService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<Package> Get(long id)
        {
            return packageService.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await packageService.Delete(id);
        }
    }
}
