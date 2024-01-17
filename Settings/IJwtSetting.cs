namespace BanooClub.Settings
{
    public interface IJwtSetting
    {
        string SecretKey { get; set; }
        string Encryptkey { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int NotBeforeMinutes { get; set; }
        int ExpirationMinutes { get; set; }
    }
    public class JwtSetting : IJwtSetting
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }

}
