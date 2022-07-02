using BanooClub.Models;
using System.Threading.Tasks;

namespace BanooClub.Services.TagServices
{
    public interface ITagService
    {
        Task<long> Create(Tag inputDto);
        Task<Tag> Update(Tag item);
        object GetAll(int pageNumber, int count, string searchCommand);
        Task<bool> Delete(long id);
        Task<Tag> Get(long id);
    }
}
