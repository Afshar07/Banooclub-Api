using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.AccountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManagerService _accountManagerService;

        public AccountController(IAccountManagerService signInManagerService)
        {
            _accountManagerService = signInManagerService;
        }

        [HttpPost("GetUser"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var result = await _accountManagerService.GetById(userId);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserSignDTO model)
        {
            model.Mail = model.Mail.ToLower();
            var result = await _accountManagerService.SignUp(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);

        }

        [HttpPost("BirthDateList")]
        public async Task<IActionResult> BirthDateList()
        {
           
            var result = await _accountManagerService.BirthDateList();

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);

        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserSignDTO model)
        {
            model.Mail = model.Mail.ToLower();
            var result = await _accountManagerService.SignIn(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("CheckUserName")]
        public async Task<bool> CheckUserName(string userName)
        {
            var result = await _accountManagerService.CheckUserName(userName);
            return result;
        }

        [HttpPost("SendConfirmationCodeToMobile")]
        public async Task<IActionResult> SendConfirmationCodeToMobile(string phoneNumber)
            => Ok((await _accountManagerService.SendConfirmationCode(phoneNumber)).Data);

        [HttpPost("SendConfirmationCodeToEmail")]
        public async Task<IActionResult> SendConfirmationCodeToEmail(string email)
            => Ok((await _accountManagerService.SendConfirmationCodeToEmail(email)).Data);

        [HttpPost("ValidateConfirmationCode")]
        public async Task<IActionResult> ValidateConfirmationCode(string phoneNumber, string code)
        {
            var result = await _accountManagerService.ValidateConfirmationCode(phoneNumber, code);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.ErrorMessage);

        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(AuthTypes type,string PhoneOrEmail)
            => Ok(await _accountManagerService.ForgotPassword(type,PhoneOrEmail));
        //[HttpPost("UpdateUser"), Authorize(Roles = "Admin")]
        //public IActionResult UpdateUser(User model)
        //{
        //    var result = _accountManagerService.UpdateUser(model);

        //    if (result.IsSuccess)
        //        return Ok(result.Data);

        //    return BadRequest(result.ErrorMessage);
        //}

        //[HttpPost("DeleteUser"), Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteUser(int userId)
        //{
        //    var result = await _accountManagerService.DeleteUser(userId);

        //    if (result.IsSuccess)
        //        return Ok();

        //    return BadRequest();
        //}
        [HttpPost("ChangeRole")]
        public async Task<bool> ChangeRole(long userId, long UserTypeId)
        {
            return await _accountManagerService.ChangeRole(userId, UserTypeId);
        }
        [HttpGet("GetConfirmationCode/{phoneNumber}")]
        public async Task<IActionResult> GetConfirmationCode([FromServices] IDistributedCache cache, string phoneNumber)
            => Ok(Encoding.UTF8.GetString(await cache.GetAsync(phoneNumber)));

        [HttpPost("CompletelyRemoveUser"),Authorize(Roles ="Admin")]
        public async Task<IActionResult> RemoveUser(long userId)
            => Ok((await _accountManagerService.RemoveUser(userId)));

    }
}
