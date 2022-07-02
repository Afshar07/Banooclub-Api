using BanooClub.Models;
using BanooClub.Services.Common;
using System.Threading.Tasks;

namespace BanooClub.Services.JWTServices
{
    public interface IGenerateJwtService
    {
        Task<IServiceResult<object>> GenerateAsync(User user);
    }
}
