using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.ServiceCategoryServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService serviceCategoryService;

        public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
        {
            this.serviceCategoryService = serviceCategoryService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] ServiceCategory inputDto)
        {
            return await this.serviceCategoryService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ServiceCategory> Update([FromBody] ServiceCategory inputDto)
        {
            return await this.serviceCategoryService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<ServiceCategory> Get(long serviceCategoryId)
        {
            return await serviceCategoryService.Get(serviceCategoryId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await serviceCategoryService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            return serviceCategoryService.GetAll(pageNumber, count, searchCommand);
        }
    }
}
