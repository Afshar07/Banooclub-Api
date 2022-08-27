using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using BanooClub.Services.CryptographyServices;
using BanooClub.Services.DateTimeServices;
using BanooClub.Services.JWTServices;
using BanooClub.Services.SmsServices;
using BanooClub.Settings;
using Infrastructure;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MimeKit;
using Newtonsoft.Json;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BanooClub.Services.AccountServices
{
    public class AccountManagerService : IAccountManagerService
    {
        private readonly IBanooClubEFRepository<User> _userRepository;
        private readonly IBanooClubEFRepository<ViewHistory> _viewHistoryRepository;
        private readonly IBanooClubEFRepository<UserSetting> _userSettingRepository;
        private readonly IBanooClubEFRepository<UserType> _roleRepository;
        private readonly IEncryptService _encryptService;
        private readonly IGenerateJwtService _generateJwtService;
        private readonly IDistributedCache _cache;
        private readonly ISmsSenderService _smsSenderService;
        private readonly ILogger<AccountManagerService> _logger;
        private readonly IDateTime _dateTime;
        private readonly IConfirmationCodeSetting _confirmationCodeSetting;
        private readonly IDecryptService _decryptService;
        private readonly IDistributedCache distributedCache;
        private static Random random = new Random();

        public AccountManagerService(IEncryptService encryptService, IGenerateJwtService generateJwtService,
            IDistributedCache cache, ILogger<AccountManagerService> logger, IDateTime dateTime, IDistributedCache distributedCache,
        IBanooClubEFRepository<UserType> roleRepository, IBanooClubEFRepository<User> userRepository, IBanooClubEFRepository<ViewHistory> viewHistoryRepository,
            IConfirmationCodeSetting confirmationCodeSetting, ISmsSenderService smsSenderService, IDecryptService decryptService, IBanooClubEFRepository<UserSetting> userSettingRepository)
        {
            _encryptService = encryptService;
            _generateJwtService = generateJwtService;
            _cache = cache;
            _smsSenderService = smsSenderService;
            _logger = logger;
            _dateTime = dateTime;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _confirmationCodeSetting = confirmationCodeSetting;
            _decryptService = decryptService;
            _userSettingRepository = userSettingRepository;
            _viewHistoryRepository = viewHistoryRepository;
            this.distributedCache = distributedCache;
        }

        #region SignIn
        public async Task<IServiceResult<object>> SignInWithMobileOrEmail(long Type, string Mobile, string Mail, string Password, string VerifyCode)
        {
            var user = new User();
            if (Type == (int)AuthTypes.Mobile)
            {
                user = await _userRepository.GetQuery().Where(u => u.Mobile.Equals(Mobile)).FirstOrDefaultAsync();
            }
            if (Type == (int)AuthTypes.Email)
            {
                user = await _userRepository.GetQuery().Where(u => u.Email.Equals(Mail)).FirstOrDefaultAsync();
            }

            if (user == null)
            {
                var result = new
                {
                    Data = "",
                    Status = (int)SignStatus.NoUserFound
                };
                return new ServiceResult<object>().Ok(result);
            }

            if(user.Status != 1)
            {
                var result = new
                {
                    Data = "",
                    Status = (int)SignStatus.UserNotActive
                };
                return new ServiceResult<object>().Ok(result);
            }


            if (!string.IsNullOrEmpty(Mobile))
            {
                var validatePassword = false;
                var validateCode = false;

                if (!(user.Password is null) && !string.IsNullOrEmpty(Password))
                {
                    if (_encryptService.Encrypt(Password).Data == user.Password)
                        validatePassword = true;
                    else
                    {
                        var AAA = new
                        {
                            Data = "",
                            Status = (int)SignStatus.WrongUserPass
                        };
                        return new ServiceResult<object>().Ok(AAA);
                    }
                }

                if (!string.IsNullOrEmpty(VerifyCode))
                {
                    var passwordCache = await _cache.GetAsync(user.Mobile);
                    validateCode = passwordCache != null && VerifyCode.Equals(Encoding.UTF8.GetString(passwordCache));
                }

                if (validatePassword || validateCode)
                {
                    var jwtResult = await _generateJwtService.GenerateAsync(user);
                    if (jwtResult.IsSuccess)
                    {
                        await _cache.RemoveAsync(user.Mobile);
                        var BBB = new
                        {
                            Data = jwtResult.Data,
                            Status = (int)SignStatus.Success
                        };
                        return new ServiceResult<object>().Ok(BBB);
                    }
                }
                else
                {
                    var Obj = new
                    {
                        Data = "",
                        Status = (int)SignStatus.WrongUserPass
                    };
                    return new ServiceResult<object>().Ok(Obj);
                }
            }
            if (!string.IsNullOrEmpty(Mail))
            {
                var validatePassword = false;
                var validateCode = false;

                if (!(user.Password is null) && !string.IsNullOrEmpty(Password))
                {
                    if (_encryptService.Encrypt(Password).Data == user.Password)
                        validatePassword = true;
                    else
                    {
                        var AAA = new
                        {
                            Data = "",
                            Status = (int)SignStatus.WrongUserPass
                        };
                        return new ServiceResult<object>().Ok(AAA);
                    }
                }

                if (!string.IsNullOrEmpty(VerifyCode))
                {
                    var passwordCache = await _cache.GetAsync(user.Email);
                    validateCode = passwordCache != null && VerifyCode.Equals(Encoding.UTF8.GetString(passwordCache));
                }

                if (validatePassword || validateCode)
                {
                    var jwtResult = await _generateJwtService.GenerateAsync(user);
                    if (jwtResult.IsSuccess)
                    {
                        await _cache.RemoveAsync(user.Mobile);
                        var BBB = new
                        {
                            Data = jwtResult.Data,
                            Status = (int)SignStatus.Success
                        };
                        return new ServiceResult<object>().Ok(BBB);
                    }
                }
                else
                {
                    var Obj = new
                    {
                        Data = "",
                        Status = (int)SignStatus.WrongUserPass
                    };
                    return new ServiceResult<object>().Ok(Obj);
                }
            }

            var FFF = new
            {
                Data = "",
                Status = (int)SignStatus.WrongUserPass
            };
            return new ServiceResult<object>().Ok(FFF);
        }

        public async Task<IServiceResult<object>> SignInWithoutPassword(string encryptedMail)
        {
            var decriptContents = _decryptService.DecryptMail(encryptedMail).Data;
            var decriptContent = decriptContents.Split("///");
            var ReqTime = Convert.ToDateTime(decriptContent[1].Replace('/', ':'));
            var DecriptedMail = decriptContent[0].Replace('/', '@');
            var dateTimeNow = System.DateTime.UtcNow;
            if (dateTimeNow < ReqTime.AddSeconds(30))
            {
                var user = await _userRepository.GetQuery().Where(u => u.Mobile.ToUpper().Equals(DecriptedMail.ToUpper())).FirstOrDefaultAsync();
                if (!(user is null) && !string.IsNullOrEmpty(DecriptedMail))
                {
                    var jwtResult = await _generateJwtService.GenerateAsync(user);
                    if (jwtResult.IsSuccess)
                    {
                        await _cache.RemoveAsync(user.Mobile);
                        return new ServiceResult<object>().Ok(jwtResult.Data);
                    }
                }
                return new ServiceResult<object>().Ok(0);
            }
            else
            {
                return new ServiceResult<object>().Ok(0);
            }
        }

        public async Task<IServiceResult<object>> SignIn(UserSignDTO model)
        {
            switch (model.Type)
            {
                case (int)AuthTypes.Mobile:
                    var mobileResult = await SignInWithMobileOrEmail(model.Type, model.Mobile, model.Mail, model.Password, model.VerifyCode);
                    return new ServiceResult<object>().Ok(mobileResult.Data);
                case (int)AuthTypes.Email:
                    var mailResult = await SignInWithMobileOrEmail(model.Type, model.Mobile, model.Mail, model.Password, model.VerifyCode);
                    return new ServiceResult<object>().Ok(mailResult.Data);
                case (int)AuthTypes.WithoutPassword:
                    var result = await SignInWithoutPassword(model.EncryptedMail);
                    return new ServiceResult<object>().Ok(result.Data);
            }
            return new ServiceResult<object>().Ok(0);
        }

        #endregion

        #region SignUp

        public async Task<IServiceResult<object>> SignUpWithMobileAndMail(System.DateTime? BirthDate, string firstname, string lastName, string mobile, string email, string code, long signUpType, string password,string userName,long? ServiceCategoryId,string IntroducerCode,long? stateId,long? cityId,RelationStatus? relationState)
        {
            var EmailUsers = new List<User>();
            var MobileUsers = new List<User>();
            if (!string.IsNullOrEmpty(mobile))
            {
                MobileUsers = await _userRepository.GetQuery().Where(user => user.Mobile.Equals(mobile)).ToListAsync();
            }
            if (!string.IsNullOrEmpty(email))
            {
                EmailUsers = await _userRepository.GetQuery().Where(user => user.Email.Equals(email.ToLower())).ToListAsync();
            }

            if (!(MobileUsers.Any() || EmailUsers.Any()))
            {
                var confirmCode = new byte[] { };
                if(signUpType == (int)AuthTypes.Mobile)
                {
                    confirmCode = await _cache.GetAsync(mobile);
                }
                if(signUpType == (int)AuthTypes.Email)
                {
                    confirmCode=await _cache.GetAsync(email);
                } 
                if (!(confirmCode is null))
                {
                    if (Encoding.UTF8.GetString(confirmCode) == code)
                    {
                        #region Old Insert
                        //var user = new User()
                        //{
                        //    Mobile = mobile,
                        //    Name = firstname,
                        //    FamilyName = lastName,
                        //    IsDeleted = false,
                        //    Email = email,
                        //    Password = password,
                        //    Status = 1,
                        //    Type = (await _roleRepository.GetQuery().Where(p => p.Name.Equals("Subscriber")).FirstOrDefaultAsync())
                        //        .UserTypeId
                        //};
                        //var userCreation = _userRepository.Insert(user);

                        //var dbUserSetting = new UserSetting()
                        //{
                        //    UserSettingId = 0,
                        //    UserId = userCreation.UserId,
                        //    Bio = "",
                        //    Flag = "",
                        //    BirthDate = System.DateTime.Now,
                        //    IsPrivateRoomate = true,
                        //    Gender = 0,
                        //    IsDeleted = false,
                        //    IsPrivateSocial = true,
                        //    IsPrivate = true,
                        //    IsPrivateAds = true,
                        //    UserTag = ""
                        //};
                        //_userSettingRepository.Insert(dbUserSetting);

                        //var dbViewHistory = new ViewHistory()
                        //{
                        //    ViewHistoryId = 0,
                        //    UserId = userCreation.UserId,
                        //    IsDeleted = false,
                        //    UpdateDate = System.DateTime.Now,
                        //    FirstPostId = 0,
                        //    LastPostId = 0
                        //};
                        //_viewHistoryRepository.Insert(dbViewHistory);
                        #endregion


                        if(IntroducerCode == null || IntroducerCode =="")
                        {
                            IntroducerCode = null;
                        }
                        var ServiceCatId = "";
                        if(ServiceCategoryId == null)
                        {
                            ServiceCatId ="NULL";
                        }
                        else
                        {
                            ServiceCatId = $"{ServiceCategoryId}";
                        }
                        var STATEID = stateId != null ? $"{stateId}" : "NULL";
                        var CITYID = cityId != null ? $"{cityId}" : "NULL";
                        var RELATIONSTATE = relationState != null ? $"{(int)relationState}" : "NULL";

                        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                        var UserCode = new string(Enumerable.Repeat(chars, 8)
                            .Select(s => s[random.Next(s.Length)]).ToArray());

                        var cmd = "BEGIN TRANSACTION [Tran1]" +
                            "BEGIN TRY" +
                            " declare @LASTID bigint" +
                            " INSERT INTO[User].Users(Mobile, Name, Type, FamilyName, Email, IsDeleted, Password, Status,FormalId,UserName,ServiceCategoryId,UserCode,IntroducerCode,BirthDate,StateId,CityId,RelationState)" +
                            $" VALUES('{mobile}', N'{firstname}', 3, N'{lastName}', '{email}', 0, '{password}', 1,0,N'{userName}',{ServiceCatId},N'{UserCode}',N'{IntroducerCode}','{BirthDate}',{STATEID},{CITYID},{RELATIONSTATE})" +
                            " SET @LASTID = SCOPE_IDENTITY()" +
                            " INSERT INTO[User].UserSettings(ActiveRoomate,IsPrivateActivity, VideoIdentityStatus, IsPrivateRoomate, IsPrivateAds, IsPrivateSocial, Flag, Bio, IsDeleted, BirthDate, UserTag, UserId ,LawyerCertificateStatus,NewspaperStatus )" +
                            "  VALUES(0,0,1, 0, 0, 0, '', '', 0, GETDATE(), '', @LASTID , 4 , 4)" +
                            " INSERT INTO[Social].ViewHistories(FirstPostId, LastPostId, UserId, UpdateDate, IsDeleted)" +
                            " VALUES(0, 0, @LASTID, GETDATE(), 0)" +
                            " COMMIT TRANSACTION[Tran1]" +
                            "   select * from[User].Users where userid = @LASTID" +
                            "  END TRY" +
                            " BEGIN CATCH" +
                            " ROLLBACK TRANSACTION[Tran1]" +
                            " END CATCH";

                        var user = _userRepository.SqlExecute(cmd).ToList();
                        //var user = JsonSerializer.Deserialize<User[]>(Dapper);
                        if (signUpType == (int)AuthTypes.Mobile)
                        {
                            _logger.LogInformation($"sign up ----> {mobile}");
                        }
                        if (signUpType == (int)AuthTypes.Email)
                        {
                            _logger.LogInformation($"sign up ----> {email}");
                        }


                        var jwtResult = await _generateJwtService.GenerateAsync(user.First());
                        if (jwtResult.IsSuccess)
                        {
                            await _cache.RemoveAsync(mobile);
                            await _cache.RemoveAsync(email);

                            var UserNames = _userRepository.GetQuery().Select(z => z.UserName).ToList();
                            var cacheKey = "BanooClubUserName";
                            var serializedCustomerList = JsonConvert.SerializeObject(UserNames);
                            var UserNameList = Encoding.UTF8.GetBytes(serializedCustomerList);
                            await distributedCache.SetAsync(cacheKey, UserNameList);


                            return new ServiceResult<object>().Ok(jwtResult.Data);
                        }
                        return new ServiceResult<object>().Failure(jwtResult.ErrorMessage);
                    }
                    return new ServiceResult<object>().Ok((int)SignStatus.WrongCode);
                }
                return new ServiceResult<object>().Ok((int)SignStatus.CodeExpire);
            }
            return new ServiceResult<object>().Ok((int)SignStatus.UserExists);
        }
        public async Task<IServiceResult<object>> SignUpWithoutCode(string firstname, string lastName, string encryptedMail)
        {
            var decriptContents = _decryptService.DecryptMail(encryptedMail).Data;
            var decriptContent = decriptContents.Split("///");
            var ReqTime = Convert.ToDateTime(decriptContent[1].Replace('/', ':'));
            var DecriptedMail = decriptContent[0].Replace('/', '@');
            var dateTimeNow = System.DateTime.UtcNow;
            if (dateTimeNow < ReqTime.AddSeconds(30))
            {
                var users = await _userRepository.GetQuery().Where(user => user.Mobile.Equals(DecriptedMail)).ToListAsync();
                if (!users.Any())
                {
                    var user = new User()
                    {
                        Mobile = DecriptedMail,
                        Name = firstname,
                        FamilyName = lastName,
                        Password = null,
                        IsDeleted = false,
                        Email = DecriptedMail,
                        Status = 1,
                        Type = 3
                    };
                    await _userRepository.InsertAsync(user);
                    _logger.LogInformation($"sign up ----> {DecriptedMail}");

                    var jwtResult = await _generateJwtService.GenerateAsync(user);
                    if (jwtResult.IsSuccess)
                    {
                        await _cache.RemoveAsync(DecriptedMail);
                        return new ServiceResult<object>().Ok(jwtResult.Data);
                    }

                    return new ServiceResult<object>().Failure(jwtResult.ErrorMessage);
                }
                return new ServiceResult<object>().Failure("This email is already registered in the system");
            }
            else
            {
                return new ServiceResult<object>().Ok(0);
            }

        }
        public async Task<IServiceResult<object>> SignUp(UserSignDTO model)
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$");
            Match match = regex.Match(model.Password);
            if (match.Success)
            {
                model.Password = _encryptService.Encrypt(model.Password).Data;
            }
            else
            {
                return new ServiceResult<object>().Ok((int)SignStatus.PassFormatNotMatched);
            }
            if(await CheckUserName(model.UserName))
            {
                switch (model.Type)
                {
                    case (int)AuthTypes.Mobile:
                        var mobileResult = await SignUpWithMobileAndMail(model.BirthDate, model.FirstName, model.LastName, model.Mobile, model.Mail, model.VerifyCode, model.Type, model.Password, model.UserName,model.ServiceCategoryId,model.IntroducerCode,model.StateId,model.CityId,model.RelationState);
                        return new ServiceResult<object>().Ok(mobileResult.Data);
                    case (int)AuthTypes.Email:
                        var mailResult = await SignUpWithMobileAndMail(model.BirthDate, model.FirstName, model.LastName, model.Mobile, model.Mail, model.VerifyCode, model.Type, model.Password, model.UserName, model.ServiceCategoryId, model.IntroducerCode, model.StateId, model.CityId, model.RelationState);
                        return new ServiceResult<object>().Ok(mailResult.Data);
                    case (int)AuthTypes.WithoutPassword:
                        var result = await SignUpWithoutCode(model.FirstName, model.LastName, model.EncryptedMail);
                        return new ServiceResult<object>().Ok(result.Data);
                }
            }
            else
            {
                return new ServiceResult<object>().Ok((int)SignStatus.UserNameExists);
            }
            return new ServiceResult<object>().Ok(0);
        }
        #endregion

        public async Task<bool> CheckUserName(string userName)
        {
            var cacheKey = "BanooClubUserName";
            string serializedCustomerList;
            var userNames = new List<string>();
            var UserNameList = await distributedCache.GetAsync(cacheKey);
            if (UserNameList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(UserNameList);
                userNames = JsonConvert.DeserializeObject<List<string>>(serializedCustomerList);
            }
            else
            {
                userNames = _userRepository.GetQuery().Select(z=>z.UserName).ToList();
                serializedCustomerList = JsonConvert.SerializeObject(userNames);
                UserNameList = Encoding.UTF8.GetBytes(serializedCustomerList);
                await distributedCache.SetAsync(cacheKey, UserNameList);
            }
            if (userNames.Contains(userName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public async Task<IServiceResult> ValidateConfirmationCode(string phoneNumber, string code)
        {
            var passwordCache = await _cache.GetAsync(phoneNumber);
            if (passwordCache != null && code.Equals(Encoding.UTF8.GetString(passwordCache)))
                return new ServiceResult().Ok();

            return new ServiceResult().Failure("");
        }
        public async Task<IServiceResult<User>> UpdateUser(User user)
        {
            await _userRepository.Update(user);
            return new ServiceResult<User>().Ok(user);
        }
        public async Task<IServiceResult<User>> GetById(int userId)
        {
            var model = _userRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            model.Password = null;
            return new ServiceResult<User>().Ok(model);
        }
        public Task<IServiceResult<List<User>>> GetByIds(List<int> ids)
        {
            var model = _userRepository.GetQuery().Where(p => ids.Any(i => i.Equals(p.UserId))).ToList();
            model.ForEach(z => z.Password = null);
            return Task.FromResult(new ServiceResult<List<User>>().Ok(model));
        }
        public async Task<IServiceResult> DeleteUser(int userId)
        {
            var user = (await GetById(userId)).Data;
            await _userRepository.Delete(user);
            return new ServiceResult().Ok();
        }

        public async Task<IServiceResult> RemoveUser(long userId)
        {
            try
            {
                string cmd = $"Delete [User].Users where UserId = {userId} "+
                  $" Delete[User].UserSettings where UserId = {userId} "+
                   $" Delete Social.ViewHistories where UserId = {userId} "+
                   $" Delete Social.RoomateDocs Where UserId = {userId} "+
                   $" Delete Social.PostComments where UserId = {userId} "+
                   $" Delete Social.PostLikes where UserId = {userId} "+
                   $" Delete Social.Messages where UserId = {userId} "+
                   $" Delete Social.Posts where UserId = {userId} "+
                   $" Delete Social.Reviews where UserId = {userId} "+
                   $" Delete Social.FollowRequests where FollowerUserId = {userId} and FollowingUserId = {userId} "+
                   $" Delete Social.Followings where FollowingUserId = {userId} "+
                   $" Delete Social.Followers where FollowerUserId = {userId} "+
                   $" Delete Social.AccountSettings where UserId = {userId} "+
                   $" Delete Social.Activities where UserId = {userId} "+
                   $" Delete Social.MessageRecipients where UserId = {userId} "+
                   $" Delete Common.Tickets where UserId = {userId} "+
                   $" Delete Common.Logs where UserId = {userId} "+
                   $" Delete Common.WishLists where UserId = {userId} "+
                   $" Delete Common.PostReports where ReporterUserId =1 and UserId = {userId} "+
                   $" Delete Service.Roomates where UserId = {userId} "+
                   $" Delete Service.Ads where UserId = {userId} "+
                   $" Delete Service.AdsComments where UserId = {userId} "+
                   $" Delete Service.UserComments where UserId = {userId}";

                await _userRepository.DapperSqlQuery(cmd);
                var cacheKey = "UserNames";
                var userNames = _userRepository.GetQuery().Select(z => z.UserName).ToList();
                var serializedCustomerList = JsonConvert.SerializeObject(userNames);
                var UserNameList = Encoding.UTF8.GetBytes(serializedCustomerList);
                await distributedCache.SetAsync(cacheKey, UserNameList);
                return new ServiceResult().Ok();
            }
            catch (Exception ex)
            {
                return new ServiceResult().Failure("something went wrong !!!");
            }
            

        }
        public async Task<IServiceResult<object>> SendConfirmationCode(string phoneNumber)
        {
            var hasUser = (await _userRepository.GetQuery().Where(u => u.Mobile.Equals(phoneNumber)).AnyAsync()) ? 1 : 0;
            if(hasUser ==0)
            {
                if (await _cache.GetAsync(phoneNumber) is null)
                {
                    var random = new Random();
                    var confirmCode = new string(Enumerable.Repeat(_confirmationCodeSetting.Chars, _confirmationCodeSetting.CodeLength).Select(s => s[random.Next(_confirmationCodeSetting.Chars.Length)]).ToArray());
                    UltraFastSend member = new UltraFastSend()
                    {
                        Mobile = Convert.ToInt64(phoneNumber),
                        TemplateId = 52260,
                        ParameterArray = new List<UltraFastParameters>()
                    {
                        new UltraFastParameters()
                        {
                            Parameter = "VerificationCode" , ParameterValue = $"{confirmCode}"
                        }
                    }.ToArray()
                    };
                    var result = await _smsSenderService.UltraFastSend(member);
                    if (result.IsSuccess)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(confirmCode.ToCharArray());
                        await _cache.SetAsync(phoneNumber, bytes, new DistributedCacheEntryOptions() { AbsoluteExpiration = _dateTime.Now().AddMinutes(2) });
                        return new ServiceResult<object>().Ok(new { hasUser, message = "A Confirmation code has been sent to you" });
                    }
                    return new ServiceResult<object>().Ok(new { hasUser, message = result.ErrorMessage });
                }
                return new ServiceResult<object>().Ok(new { hasUser, message = "Confirmation code has not expired" });
            }
            else
            {
                return new ServiceResult<object>().Ok(new { hasUser, message = "User Exists" });
            }
        }
        public async Task<IServiceResult<object>> SendConfirmationCodeToEmail(string Email)
        {
            var hasUser = (await _userRepository.GetQuery().Where(u => u.Email.ToUpper().Equals(Email.ToUpper())).AnyAsync()) ? 1 : 0;
            if (await _cache.GetAsync(Email) is null)
            {
                try
                {
                    var random = new Random();
                    var confirmCode = new string(Enumerable.Repeat(_confirmationCodeSetting.Chars, _confirmationCodeSetting.CodeLength).Select(s => s[random.Next(_confirmationCodeSetting.Chars.Length)]).ToArray());
                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("BanooClub", "info@BanooClub.simagar.com");
                    message.From.Add(from);

                    MailboxAddress to = new MailboxAddress("User", $"{Email}");
                    message.To.Add(to);

                    message.Subject = "This email is from BanooClub";

                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = $"your validation code is {confirmCode}";
                    message.Body = bodyBuilder.ToMessageBody();

                    SmtpClient client = new SmtpClient();
                    client.Connect("95.216.101.100", 587, false);
                    client.Authenticate("info@BanooClub.simagar.com", "123a@a.com");

                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                    byte[] bytes = Encoding.UTF8.GetBytes(confirmCode.ToCharArray());
                    await _cache.SetAsync(Email, bytes, new DistributedCacheEntryOptions() { AbsoluteExpiration = _dateTime.Now().AddMinutes(4) });
                    return new ServiceResult<object>().Ok(new { hasUser, message = "A Confirmation code has been sent to your Email" });
                }
                catch (Exception ex)
                {
                    return new ServiceResult<object>().Ok(new { hasUser, message = "network problem" });
                }
            }
            return new ServiceResult<object>().Ok(new { hasUser, message = "Confirmation code has not expired" });
        }

        public async Task<IServiceResult<object>> ForgotPassword(AuthTypes type,string PhoneOrEmail)
        {
            string PassWord = "";
            User dbUser =new User();
            if(type == AuthTypes.Email)
            {
                dbUser = _userRepository.GetQuery().FirstOrDefault(z => z.Email == PhoneOrEmail);
            }
            if (type==AuthTypes.Mobile)
            {
                dbUser=_userRepository.GetQuery().FirstOrDefault(z => z.Mobile == PhoneOrEmail);
            }
            if (dbUser == null)
            {
                var Obj = new
                {
                    Status = (int)SignStatus.NoUserFound
                };
                return new ServiceResult<object>().Ok(Obj);
            }
            PassWord = _decryptService.Decrypt(dbUser.Password).Data;

            if (type==AuthTypes.Email)
            {
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("BanooClub", "info@BanooClub.simagar.com");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("User", $"{PhoneOrEmail}");
                message.To.Add(to);

                message.Subject = "This email is from BanooClub";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = $"your password is {PassWord}";
                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("95.216.101.100", 587, false);
                client.Authenticate("info@BanooClub.simagar.com", "123a@a.com");

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            if (type == AuthTypes.Mobile)
            {
                UltraFastSend member = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64(PhoneOrEmail),
                    TemplateId = 63752,
                    ParameterArray = new List<UltraFastParameters>()
                    {
                        new UltraFastParameters()
                        {
                            Parameter = "Password" , ParameterValue = $"{PassWord}"
                        }
                    }.ToArray()
                };
                var result = await _smsSenderService.UltraFastSend(member);
            }

            var Obj1 = new
            {
                Status = (int)SignStatus.Success
            };
            return new ServiceResult<object>().Ok(Obj1);    

        }
        public string creatRandomPassword()
        {
            string chars = "0123456789ABCDEFGHIJKLMNOPQSTUVWXYZabcdefghijklmnpqrstuvwxyz!@#$%^&*";
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$");
            Random randomNnmber = new Random();
            int len = randomNnmber.Next(8, 15);//get the max(15) and min(8)
            string randomStr = "";

            for (int i = 0; i < len; i++)
            {
                randomStr += chars[randomNnmber.Next(chars.Length)];
            }
            Match match = regex.Match(randomStr);
            if (!match.Success)
            {
                Console.WriteLine($"{randomStr}");
                return creatRandomPassword();
            }
            else
            {
                Console.WriteLine($"{randomStr}");
                return randomStr;
            }
        }
        public async Task<bool> ChangeRole(long userId, long UserTypeId)
        {
            try
            {
                var dbUser = _userRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
                if (UserTypeId == 2)
                {
                    UserTypeId = 1;
                }
                dbUser.Type = UserTypeId;
                await _userRepository.Update(dbUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<IServiceResult<object>> BirthDateList()
        {
            var result = _userRepository.GetQuery().Where(x => x.BirthDate.Value.Day == System.DateTime.Now.Day && x.BirthDate.Value.Month==System.DateTime.Now.Month).ToList();
            var Obj = new
            {
                Data = result
            };
            return new ServiceResult<object>().Ok(Obj);

        }

        
    }
}
