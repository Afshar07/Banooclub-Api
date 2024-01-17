using BanooClub.Models;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices;
using BanooClub.Services.ConsultingServices.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultCategoryController : ControllerBase
    {
        private readonly IConsultCategoryService _consultCategoryService;

        public ConsultCategoryController(IConsultCategoryService consultCategoryService)
        {
            _consultCategoryService = consultCategoryService;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IServiceResult>> Create(CreateConsultCategoryDTO dto)
        {
            var result = await _consultCategoryService.CreateConsultCategory(dto);
            return Ok(result);
        }

        [HttpPost("[action]")]
        
        public async Task<ActionResult<IServiceResult<PageModel<ConsultCategoryDTO>>>> Get(int page = 1, int size = 10)
        {
            var result = await _consultCategoryService.GetCategories(page,size);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<IServiceResult<List<ConsultCategoryDTO>>>> GetAll()
        {
            var result = await _consultCategoryService.GetCategories();
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IServiceResult>> Delete(long id)
        {
            var result = await _consultCategoryService.Delete(id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IServiceResult>> Update(UpdateConsultCategoryDTO dto)
        {
            var result = await _consultCategoryService.Update(dto);
            return Ok(result);
        }
    }
}
