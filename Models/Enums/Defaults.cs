using System.Collections.Generic;

namespace BanooClub.Models.Enums
{
    public static class Defaults
    {
        public const string Avatars = "/wwwroot/Media/Gallery/DefaultAvatars";
        public const string Banners = "/wwwroot/Media/Gallery/DefaultBanners";

        public static IList<string> GetDefaultAvatars() =>
            new List<string>
            {
                "defaultAvatar1.png",
                "defaultAvatar2.png",
                "defaultAvatar3.png",
                "defaultAvatar4.png",
                "defaultAvatar5.png",
                "defaultAvatar6.png",
                "defaultAvatar7.png",
                "defaultAvatar8.png",
                "defaultAvatar9.png",
                "defaultAvatar10.png",
                "defaultAvatar11.png",
                "defaultAvatar12.png",
                "defaultAvatar13.png",
                "defaultAvatar14.png",
                "defaultAvatar15.png",
                "defaultAvatar16.png"
            };

        public static IList<string> GetDefaultBanners() =>
            new List<string>
            {
                "defaultBanner1.jpg",
                "defaultBanner2.jpg",
                "defaultBanner3.jpg",
                "defaultBanner4.jpg",
                "defaultBanner5.jpg",
                "defaultBanner6.jpg",
                "defaultBanner7.jpg",
                "defaultBanner8.jpg",
                "defaultBanner9.jpg",
                "defaultBanner10.jpg",
                "defaultBanner11.jpg",
                "defaultBanner12.jpg",
                "defaultBanner13.jpg",
                "defaultBanner14.jpg",
                "defaultBanner15.jpg",
                "defaultBanner16.jpg",
                "defaultBanner17.jpg",
                "defaultBanner18.jpg",
                "defaultBanner19.jpg",
                "defaultBanner20.jpg",
                "defaultBanner21.jpg",
                "defaultBanner22.jpg",
            };
    }
}