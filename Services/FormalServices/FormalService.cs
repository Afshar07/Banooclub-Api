using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FormalServices
{
    public class FormalService : IFormalService
    {
        private readonly IBanooClubEFRepository<Formal> formalRepository;
        private readonly IBanooClubEFRepository<User> userRepository;

        public FormalService(IBanooClubEFRepository<Formal> formalRepository, IBanooClubEFRepository<User> userRepository)
        {
            this.formalRepository = formalRepository;
            this.userRepository = userRepository;
        }
        public async Task Create(Formal inputDto)
        {
            formalRepository.Insert(inputDto);
        }

        public async Task<Formal> Update(Formal item)
        {
            await formalRepository.Update(item);
            return item;
        }

        public async Task<Formal> Get(long id)
        {
            var dbFormal = formalRepository.GetQuery().FirstOrDefault(z => z.FormalId == id);
            return dbFormal;
        }

        public async Task Delete(long id)
        {
            var dbFormal = formalRepository.GetQuery().FirstOrDefault(z => z.FormalId == id);
            await formalRepository.Delete(dbFormal);
        }

        public async Task<List<Formal>> GetAll()
        {
            List<Formal> formals = new List<Formal>();
            formals = formalRepository.GetQuery().ToList();
            return formals;
        }

        public async Task<List<User>> GetAllUsers(long FormalId)
        {
            var dbUsers = userRepository.GetQuery().Where(z => z.FormalId == FormalId).ToList();
            return dbUsers;
        }

    }
}
