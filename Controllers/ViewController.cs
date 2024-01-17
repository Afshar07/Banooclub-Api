using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.ViewServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly IViewService viewService;

        public ViewController(IViewService viewService)
        {
            this.viewService = viewService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] View inputDto)
        {
            return await this.viewService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<View> Update([FromBody] View inputDto)
        {
            return await this.viewService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<View> Get(long viewId)
        {
            return await viewService.Get(viewId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await viewService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count)
        {
            return viewService.GetAll(pageNumber, count);
        }
    }
}
