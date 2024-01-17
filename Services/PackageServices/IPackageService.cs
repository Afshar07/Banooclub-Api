using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.PackageServices
{
    public interface IPackageService
    {
        Task Create(Package inputDto);
        Task<Package> Update(Package item);
        Task Delete(long id);
        Task<List<Package>> GetAll();
        Task<Package> Get(long id);
    }
}
