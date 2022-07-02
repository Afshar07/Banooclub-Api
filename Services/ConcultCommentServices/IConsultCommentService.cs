using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.ConcultCommentServices
{
    public interface IConsultCommentService
    {
        Task Create(ConsultComment inputDto);
        Task<ConsultComment> Update(ConsultComment item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count);
        Task<ConsultComment> Get(long id);
    }
}
