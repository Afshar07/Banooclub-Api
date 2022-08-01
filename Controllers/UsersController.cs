using BanooClub.Models;
using BanooClub.Services.UserServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly BanooClubDBContext context;
        private readonly IUserService userService;

        public UsersController(BanooClubDBContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task Create([FromBody] User inputDto)
        {
            await this.userService.Create(inputDto);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> Update([FromBody] User inputDto)
        {
            var result = await userService.Update(inputDto);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }
        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<bool> UploadMediaGallery([FromBody] FileData fileData)
        {
            var result = await userService.UploadMediaGallery(fileData);
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetMediaGalleryByUserName(string userName)
        {
            var result = userService.GetMediaGalleryByUserName(userName);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<object> GetMyMediaGallery()
        {
            var result = await userService.GetMyMediaGallery();
            return result;
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<bool> UploadPhotoGallery([FromBody] FileData fileData)
        {
            var result = await userService.UploadPhotoGallery(fileData);
            return result;
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<bool> UploadVideoGallery([FromBody] FileData fileData)
        {
            var result = await userService.UploadVideoGallery(fileData);
            return result;
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public List<string> GetMyVideos()
        {
            var result =  userService.GetMyVideos();
            return result;
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public List<string> GetMyPhotos()
        {
            var result =  userService.GetMyPhotos();
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetVideosByUserId(long userId)
        {
            var result =  userService.GetVideosByUserId(userId);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetPhotosByUserId(long userId)
        {
            var result =  userService.GetPhotosByUserId(userId);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserByAdmin([FromBody] User inputDto)
        {
            var result = await userService.UpdateUserByAdmin(inputDto);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }


        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public object GetAll(int pageNumber, int count,string search)
        {
            search = search ==null ? "" : search;
            return userService.GetAll(pageNumber,count,search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Admin")]
        public Task<object> GetAllSP()
        {
            var result = userService.GetAllSP();
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public User Get(long id)
        {
            return userService.Get(id);
        }

        [HttpPost]
        [Route("[action]"),Authorize]
        public User GetByToken()
        {
            return userService.GetByToken();
        }


        //[HttpPost]
        //[Route("[action]"), Authorize(Roles = "Admin")]
        //public async Task Delete(long id)
        //{
        //    await userService.Delete(id);
        //}
        [HttpPost]
        [Route("[action]"), Authorize]
        public List<User> SearchByName(string name)
        {
            return userService.SearchByName(name);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<bool> DeleteMedia (string mediaName)
        {
            return await userService.DeleteMedia(mediaName);
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public object GetAllUserforUser(long userId, int count, string search)
        {
            search = search ==null ? "" : search;
            return userService.GetAllUserforUser(userId, count, search);
        }

        [HttpPost]
        [Route("[action]"), Authorize(Roles = "Vendor")]
        public async Task<object> UserDashboards()
        {
            return await userService.UserDashboards();
        }
    }
}
