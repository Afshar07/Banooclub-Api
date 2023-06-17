using BanooClub.Models;
using BanooClub.Models.Consulting;
using BanooClub.Services.Common;
using BanooClub.Services.ConsultingServices.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ConsultingServices
{
    public class ConsultCategoryService : IConsultCategoryService
    {
        private readonly IBanooClubEFRepository<ConsultCategory> _consultCategoryRepository;

        public ConsultCategoryService(IBanooClubEFRepository<ConsultCategory> consultCategoryRepository)
        {
            _consultCategoryRepository = consultCategoryRepository;
        }
        public async Task<IServiceResult> CreateConsultCategory(CreateConsultCategoryDTO dto)
        {
            var operation = new ServiceResult();

            if (dto.ParentId.HasValue)
            {
                var exist = await _consultCategoryRepository.GetQuery().Where(x => x.ParentId == dto.ParentId).AnyAsync();
                if (!exist)
                    return operation.Failure("ParentNotFound");
            }

            var category = new ConsultCategory(dto.Title, dto.ParentId);
            await _consultCategoryRepository.InsertAsync(category);
            return operation.Ok();
        }
        public async Task<IServiceResult<PageModel<ConsultCategoryDTO>>> GetCategories(int page, int size)
        {
            var operation = new ServiceResult<PageModel<ConsultCategoryDTO>>();
            var query = _consultCategoryRepository.GetQuery();



            var skip = (page - 1) * size;
            var data = await
                query.Select(x => new ConsultCategoryDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    ParentId = x.ParentId,
                })
                .Skip(skip)
                .Take(skip)
                .ToListAsync();


            var totalCount = await query.CountAsync();

            var pageModel = new PageModel<ConsultCategoryDTO>(page, size, totalCount, data);

            return operation.Ok(pageModel);

        }
        public async Task<IServiceResult<List<ConsultCategoryDTO>>> GetCategories()
        {
            var operation = new ServiceResult<List<ConsultCategoryDTO>>();
            var query = _consultCategoryRepository.GetQuery();

            var data = await
                query.Select(x => new ConsultCategoryDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    ParentId = x.ParentId,
                })
                .ToListAsync();
            return operation.Ok(data);
        }
        public async Task<IServiceResult> Update(UpdateConsultCategoryDTO dto)
        {
            var operation = new ServiceResult();
            var category = await _consultCategoryRepository.GetQuery().Where(x => x.Id == dto.Id).FirstOrDefaultAsync();
            if (category == null)
                return operation.Failure("NotFound");

            category.Title = dto.Title;
            category.ParentId = dto.ParentId;

            await _consultCategoryRepository.Update(category);
            return operation.Ok();
        }
        public async Task<IServiceResult> Delete(long categoryId)
        {
            var operation = new ServiceResult();
            var category = await _consultCategoryRepository.GetQuery().Where(x => x.Id == categoryId).FirstOrDefaultAsync();
            if (category == null)
                return operation.Failure("NotFound");


            await _consultCategoryRepository.Delete(category);
            return operation.Ok();
        }
    }
}
