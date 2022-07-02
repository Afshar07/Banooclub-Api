using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ServiceCommentServices
{
    public interface IServiceCommentService
    {
        Task<long> Create(ServiceComment inputDto);
        Task<ServiceComment> Update(ServiceComment item);
        object GetAll(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<ServiceComment> Get(long id);
    }
}
