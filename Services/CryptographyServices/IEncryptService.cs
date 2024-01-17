using BanooClub.Services.Common;

namespace BanooClub.Services.CryptographyServices
{
    public interface IEncryptService
    {
        IServiceResult<string> Encrypt(string text);
    }
}
