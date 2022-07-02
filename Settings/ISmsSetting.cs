namespace BanooClub.Settings
{
    public interface ISmsSetting
    {
        string UserApiKey { get; set; }
        string SecretKey { get; set; }
        string LineNumber { get; set; }
    }
    public class SmsSetting : ISmsSetting
    {
        public string UserApiKey { get; set; }
        public string SecretKey { get; set; }
        public string LineNumber { get; set; }
    }

}