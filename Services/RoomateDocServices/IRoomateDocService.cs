using BanooClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.RoomateDocServices
{
    public interface IRoomateDocService
    {
        Task Create(RoomateDoc inputDto);
        Task<RoomateDoc> Update(RoomateDoc item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count,string search);
        Task<RoomateDoc> Get(long id);
        RoomateDoc GetByUserId(long userId);
        Task<RoomateDoc> ChangeStatus(long roomateDocId, string StateDesc, int status);
        object GetNotConfirmed(int pageNumber, int count,string search);
        object GetRejected(int pageNumber, int count,string search);
    }
}
