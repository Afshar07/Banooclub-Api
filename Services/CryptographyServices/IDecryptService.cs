using BanooClub.Services.Common;

namespace BanooClub.Services.CryptographyServices
{
    public interface IDecryptService
    {
        IServiceResult<string> Decrypt(string text);
        IServiceResult<string> DecryptMail(string text);
    }
}
