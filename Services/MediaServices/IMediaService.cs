using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.MediaServices
{
    public interface IMediaService
    {
        Task Create(Media inputDto);
        Task<Media> Update(Media item);
        Task Delete(long id);
        Task<List<Media>> GetAll();
        Task<Media> Get(long id);
    }
}
