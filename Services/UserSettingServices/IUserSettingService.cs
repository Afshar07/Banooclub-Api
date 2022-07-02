using BanooClub.Models;
using BanooClub.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.UserSettingServices
{
    public interface IUserSettingService
    {
        Task Create(UserSetting inputDto);
        Task<UserSetting> Update(UserSetting item);
        Task Delete(long id);
        Task<List<UserSetting>> GetAll();
        Task<UserSetting> Get(long id);
        List<UserSetting> GetByUserId();
        int ChangeVideoStatusByAdmin(long userSettingId, VideoIdentity Status);

        object GetRejectedVideoIdentity(int pageNumber, int count, string search);
        object GetConfirmedVideoIdentity(int pageNumber, int count, string search);
        object GetUploadedVideoIdentity(int pageNumber, int count, string search);
        int ChangeLawyerCertificateStatusByAdmin(long userSettingId, DocStatus Status);
        int ChangeNewspaperDocStatusByAdmin(long userSettingId, DocStatus Status);
        public object GetUploadedNewspaperDocs(int pageNumber, int count, string search);
        public object GetUploadedLawyerCertificates(int pageNumber, int count, string search);
        public object GetConfirmedNewspaperDocs(int pageNumber, int count, string search);
        public object GetConfirmedLawyerCertificates(int pageNumber, int count, string search);
        public object GetRejectedLawyerCertificates(int pageNumber, int count, string search);
        public object GetRejectedNewspaperDocs(int pageNumber, int count, string search);

    }
}
