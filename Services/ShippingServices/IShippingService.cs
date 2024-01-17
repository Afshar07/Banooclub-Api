using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ShippingServices
{
    public interface IShippingService
    {
        Task<long> Create(Shipping inputDto);
        Task<Shipping> Update(Shipping item);
        object GetAll(int pageNumber, int count);
        Task<bool> Delete(long id);
        Task<Shipping> Get(long id);
    }
}
