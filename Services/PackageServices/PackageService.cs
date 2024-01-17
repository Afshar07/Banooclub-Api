using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.PackageServices
{
    public class PackageService : IPackageService
    {
        private readonly IBanooClubEFRepository<Package> packageRepository;

        public PackageService(IBanooClubEFRepository<Package> packageRepository)
        {
            this.packageRepository = packageRepository;
        }
        public async Task Create(Package inputDto)
        {
            packageRepository.Insert(inputDto);
        }

        public async Task<Package> Update(Package item)
        {
            await packageRepository.Update(item);
            return item;
        }

        public async Task<List<Package>> GetAll()
        {
            List<Package> packages = new List<Package>();
            packages = packageRepository.GetQuery().ToList();
            return packages;
        }

        public async Task Delete(long id)
        {
            var package = packageRepository.GetQuery().FirstOrDefault(z => z.PackageId == id);
            await packageRepository.Delete(package);
        }

        public async Task<Package> Get(long id)
        {
            var package = packageRepository.GetQuery().FirstOrDefault(z => z.PackageId == id);
            return package;
        }

    }
}
