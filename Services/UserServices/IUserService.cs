using BanooClub.Models;
using BanooClub.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.UserServices
{
    public interface IUserService
    {
        Task Create(User inputDto);
        Task<IServiceResult<User>> Update(User item);
        Task<IServiceResult<User>> UpdateUserByAdmin(User item);
        Task Delete(long id);
        object GetAll(int pageNumber, int count,string search);
        Task<object> GetAllSP();
        User Get(long id);
        List<User> SearchByName(string name);
        Task<bool> DeleteMedia(string mediaName);
        Task<bool> UploadPhotoGallery(FileData fileData);
        Task<bool> UploadVideoGallery(FileData fileData);
        IServiceResult<object> GetVideosByUserId(long userId);
        IServiceResult<object> GetPhotosByUserId(long userId);
        List<string> GetMyVideos();
        List<string> GetMyPhotos();
        User GetByToken();
        object GetAllUserforUser(long userId, int count, string search);
        User GetByUserName(string userName);
    }
}
