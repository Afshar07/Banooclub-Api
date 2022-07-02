using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.AccountServices
{
    public interface IAccountManagerService
    {
        Task<IServiceResult<object>> SignIn(UserSignDTO model);
        Task<IServiceResult<object>> SignUp(UserSignDTO model);
        Task<IServiceResult<User>> GetById(int userId);
        Task<IServiceResult<List<User>>> GetByIds(List<int> ids);
        Task<IServiceResult<object>> SendConfirmationCode(string phoneNumber);
        Task<IServiceResult<object>> SendConfirmationCodeToEmail(string Email);
        Task<IServiceResult> ValidateConfirmationCode(string phoneNumber, string code);
        //Task<IServiceResult<User>> CreateUser(User user);
        Task<IServiceResult<User>> UpdateUser(User user);
        Task<IServiceResult> DeleteUser(int userId);
        //Task<IServiceResult> ResetPassord(User user);
        Task<bool> CheckUserName(string userName);
        Task<IServiceResult> RemoveUser(long userId);
        Task<IServiceResult<object>> ForgotPassword(AuthTypes type, string PhoneOrEmail);
    }
}
