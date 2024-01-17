using BanooClub.Models;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public interface IConsultCategoryService
    {
        Task<IServiceResult> CreateConsultCategory(CreateConsultCategoryDTO dto);
        Task<IServiceResult> Delete(long categoryId);
        Task<IServiceResult<PageModel<ConsultCategoryDTO>>> GetCategories(int page, int size);
        Task<IServiceResult<List<ConsultCategoryDTO>>> GetCategories();
        Task<IServiceResult> Update(UpdateConsultCategoryDTO dto);
    }
}
