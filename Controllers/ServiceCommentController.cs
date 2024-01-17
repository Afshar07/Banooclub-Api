using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BanooClub.Models;
using BanooClub.Services.ServiceCommentServices;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCommentController : ControllerBase
    {
        private readonly IServiceCommentService serviceCommentService;

        public ServiceCommentController(IServiceCommentService serviceCommentService)
        {
            this.serviceCommentService = serviceCommentService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<long> Create([FromBody] ServiceComment inputDto)
        {
            return await this.serviceCommentService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ServiceComment> Update([FromBody] ServiceComment inputDto)
        {
            return await this.serviceCommentService.Update(inputDto);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public async Task<ServiceComment> Get(long serviceCommentId)
        {
            return await serviceCommentService.Get(serviceCommentId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> Delete(long id)
        {
            return await serviceCommentService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"), AllowAnonymous]
        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            return serviceCommentService.GetAll(pageNumber, count, searchCommand);
        }
    }
}
