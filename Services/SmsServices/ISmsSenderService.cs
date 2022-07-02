using BanooClub.Services.Common;
using SmsIrRestfulNetCore;
using System.Threading.Tasks;

namespace BanooClub.Services.SmsServices
{
    public interface ISmsSenderService
    {
        Task<IServiceResult> Send(string phoneNumber, string text);
        Task<IServiceResult> UltraFastSend(UltraFastSend ultrafast);
        public object KavenegarSms(string mobile, string message);

        object VerifyLookUp(string mobile, string token);
    }
}
