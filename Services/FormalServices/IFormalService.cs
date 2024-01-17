using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BanooClub.Services.FormalServices


{
    public interface IFormalService
    {
        Task Create(Formal inputDto);
        Task<Formal> Update(Formal item);
        Task Delete(long id);
        Task<Formal> Get(long id);
        Task<List<Formal>> GetAll();
        Task<List<User>> GetAllUsers(long formalId);
    }
}
