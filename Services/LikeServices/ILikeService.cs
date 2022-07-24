using BanooClub.Models;
using BanooClub.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.LikeServices
{
    public interface ILikeService
    {
        Task<LikeResponse> Create(Like inputDto);
        Task<Like> Update(Like item);
        Task Delete(long id);
        Task<List<Like>> GetAll();
        Task<Like> Get(long id);
    }
}
