using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ViewServices
{
    public interface IViewService
    {
        Task<long> Create(View inputDto);
        Task<View> Update(View item);
        object GetAll(int pageNumber, int count);
        Task<bool> Delete(long id);
        Task<View> Get(long id);
    }
}
