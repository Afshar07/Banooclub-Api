using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.SocialMediaServices
{
    public interface ISocialMediaService
    {
        Task<IServiceResult<Dictionary<string, string>>> Upload(List<IFormFile> files, int objectId, MediaTypes type);
        IServiceResult<int> Create(SocialMedia media);
        Task<IServiceResult<List<SocialMedia>>> GetByObjectId(int objectId, MediaTypes type);
        IServiceResult DeleteByMediaId(int mediaId);
        Task<IServiceResult> DeleteByObjectId(int objectId, MediaTypes type);
        OutPutSaveImage SaveImage(string base64, string path );
        OutPutSaveImage SaveImageNew(string base64, string path ,int? priority);

    }
}
