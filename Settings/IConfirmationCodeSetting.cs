namespace BanooClub.Settings
{
    public interface IConfirmationCodeSetting
    {
        int CodeLength { get; set; }
        string Chars { get; set; }
        int LifeTime { get; set; }
    }
    public class ConfirmationCodeSetting : IConfirmationCodeSetting
    {
        public int CodeLength { get; set; }
        public string Chars { get; set; }
        public int LifeTime { get; set; }
    }
}