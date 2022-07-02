using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.UserTypeServices
{
    public interface IUserTypeService
    {
        Task Create(UserType inputDto);
        Task<UserType> Update(UserType item);
        Task Delete(long id);
        Task<List<UserType>> GetAll();
        Task<UserType> Get(long id);
        List<UserType> GetByCommand(string sql);
        long UpdateByCmd(string name, long id);
    }
}
