using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.ShippingTypeServices
{
    public interface IShippingTypeService
    {
        Task<long> Create(ShippingType inputDto);
        Task<ShippingType> Update(ShippingType item);
        object GetAll(int pageNumber, int count);
        Task<bool> Delete(long id);
        Task<ShippingType> Get(long id);
    }
}
