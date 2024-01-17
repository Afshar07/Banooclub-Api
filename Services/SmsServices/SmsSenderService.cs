using BanooClub.Services.Common;
using BanooClub.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmsIrRestfulNetCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.SmsServices
{
    public class SmsSenderService : ISmsSenderService
    {
        private readonly ILogger<SmsSenderService> _logger;
        private readonly ISmsSetting _smsSetting;
        private readonly IWebHostEnvironment _environment;

        public SmsSenderService(ILogger<SmsSenderService> logger, ISmsSetting smsSetting, IWebHostEnvironment environment)
        {
            _logger = logger;
            _smsSetting = smsSetting;
            _environment = environment;
        }

        public Task<IServiceResult> Send(string phoneNumber, string text)
        {
            if (_environment.IsDevelopment())
            {
                _logger.LogInformation("Send sms -> PhoneNumber ->{PhoneNumber}->{Text}    Message->{Message}", phoneNumber, text, "IsDevelopment");
                return Task.FromResult(new ServiceResult().Ok());
            }

            var token = new Token().GetToken(_smsSetting.UserApiKey, _smsSetting.SecretKey);
            var messageSendObject = new MessageSendObject()
            {
                Messages = new List<string> { text }.ToArray(),
                MobileNumbers = new List<string> { phoneNumber }.ToArray(),
                LineNumber = _smsSetting.LineNumber,
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };

            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

            if (messageSendResponseObject.IsSuccessful)
            {
                _logger.LogInformation("Send sms -> PhoneNumber ->{PhoneNumber}->{Text}    Message->{Message}", phoneNumber, text, messageSendResponseObject.Message);
                return Task.FromResult(new ServiceResult().Ok());
            }

            _logger.LogError("Send sms -> PhoneNumber ->{PhoneNumber}->{Text}    Message->{Message}", phoneNumber, text, messageSendResponseObject.Message);
            return Task.FromResult(new ServiceResult().Failure(messageSendResponseObject.Message));
        }

        public Task<IServiceResult> UltraFastSend(UltraFastSend ultrafast)
        {
            var UserApiKey = "8948cd291e7f4d3062b6cd29";
            var SecretKey = "it66)%#teBC!@*&SimaGaRR";
            var token = new Token().GetToken($"{UserApiKey}", $"{SecretKey}");

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultrafast);

            if (ultraFastSendRespone.IsSuccessful)
            {

            }
            else
            {

            }
            return Task.FromResult(new ServiceResult().Ok());
        }

        public object KavenegarSms(string mobile, string message)
        {
            try
            {
                Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi("6D55375830333845794B746679626E584E6572376261543271343250332F5877786B37354E6E33364F43493D");
                var result = api.Send("10000550009900", mobile, message);
                return result;
            }
            catch (Kavenegar.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                return "Message : " + ex.Message;
            }
            catch (Kavenegar.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                return "Message : " + ex.Message;
            }
        }
        public object VerifyLookUp(string mobile, string token)
        {
            try
            {
                Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi("6D55375830333845794B746679626E584E6572376261543271343250332F5877786B37354E6E33364F43493D");
                var result = api.VerifyLookup(mobile, token, "BanooClubVerify");
                return result;
            }
            catch (Kavenegar.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                return "Message : " + ex.Message;
            }
            catch (Kavenegar.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                return "Message : " + ex.Message;
            }
        }
    }
}
