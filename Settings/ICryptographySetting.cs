namespace BanooClub.Settings
{
    public interface ICryptographySetting
    {
        string Key { get; set; }
    }
    public class CryptographySetting : ICryptographySetting
    {
        public string Key { get; set; }
    }

}
