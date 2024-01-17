using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.CNTRYServices
{
    public interface ICNTRYService
    {
        Task<List<CNTRY>> GetCNTRIESWithRedis();
        object GetCNTRIES(int pageNumber, int count);
    }
}
