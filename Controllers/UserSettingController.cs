using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.UserSettingServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserSettingController : ControllerBase
    {
        private readonly BanooClubDBContext context;
        private readonly IUserSettingService userSettingService;

        public UserSettingController(BanooClubDBContext context, IUserSettingService userSettingService)
        {
            this.context = context;
            this.userSettingService = userSettingService;
        }

        //[HttpPost]
        //[Route("[action]")]
        //public async Task Create([FromBody] UserSetting inputDto)
        //{
        //    await this.userSettingService.Create(inputDto);
        //}

        [HttpPost]
        [Route("[action]")]
        public async Task<UserSetting> Update([FromBody] UserSetting inputDto)
        {
            var result = await userSettingService.Update(inputDto);
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public List<UserSetting> GetByUserId()
        {
            return userSettingService.GetByUserId();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<UserSetting> Get(long userSettingId)
        {
            return userSettingService.Get(userSettingId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Delete(long id)
        {
            await userSettingService.Delete(id);
        }

        [HttpPost]
        [Route("[action]"),Authorize(Roles ="Admin")]
        public int ChangeVideoStatusByAdmin(long userSettingId,VideoIdentity videoIdentity)
        {
            return userSettingService.ChangeVideoStatusByAdmin(userSettingId,videoIdentity);
        }
        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public int ChangeLawyerCertificateStatusByAdmin(long userSettingId, DocStatus status)
        {
            return userSettingService.ChangeLawyerCertificateStatusByAdmin(userSettingId, status);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public int ChangeNewspaperDocStatusByAdmin(long userSettingId, DocStatus status)
        {
            return userSettingService.ChangeNewspaperDocStatusByAdmin(userSettingId, status);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetRejectedVideoIdentity(int pageNumber, int count,string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetRejectedVideoIdentity(pageNumber, count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetConfirmedVideoIdentity(int pageNumber, int count,string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetConfirmedVideoIdentity(pageNumber, count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetUploadedVideoIdentity(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetUploadedVideoIdentity(pageNumber, count,search);
        }
        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetUploadedNewspaperDocs(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetUploadedNewspaperDocs(pageNumber, count, search);
        }
        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetConfirmedLawyerCertificates(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetConfirmedLawyerCertificates(pageNumber, count, search);
        }
        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetConfirmedNewspaperDocs(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetConfirmedNewspaperDocs(pageNumber, count, search);
        }
        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetRejectedNewspaperDocs(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetRejectedNewspaperDocs(pageNumber, count, search);
        }
        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetRejectedLawyerCertificates(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetRejectedLawyerCertificates(pageNumber, count, search);
        }
        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetUploadedLawyerCertificates(int pageNumber, int count, string search)
        {
            search = search ==null ? "" : search;
            return userSettingService.GetUploadedLawyerCertificates(pageNumber, count, search);
        }



    }
}
