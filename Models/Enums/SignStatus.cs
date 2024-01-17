namespace BanooClub.Models.Enums
{
    public enum SignStatus
    {
        MobileAuth = 1,
        EmailAuth,
        WrongUserPass,
        CodeExpire,
        WrongCode,
        NoUserFound,
        Success,
        UserExists,
        PassFormatNotMatched,
        UserNameExists,
        UserNotActive
    }
}