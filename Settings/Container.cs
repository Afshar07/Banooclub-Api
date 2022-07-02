using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BanooClub.Settings
{
    public static class Container
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICryptographySetting>(configuration.GetSection("CryptographySetting")
                .Get<CryptographySetting>());

            services.AddSingleton<IJwtSetting>(configuration.GetSection("JwtSetting")
                .Get<JwtSetting>());

            services.AddSingleton<ISmsSetting>(configuration.GetSection("SmsSetting")
                .Get<SmsSetting>());

            services.AddSingleton<IConfirmationCodeSetting>(configuration.GetSection("ConfirmationCodeSetting")
                .Get<ConfirmationCodeSetting>());
        }
    }
}
