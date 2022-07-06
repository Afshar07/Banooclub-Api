namespace BanooClub.Models.Urls
{
    public class EntityUrls
    {
        public static string MediaBaseUrl = "wwwroot/Media/Gallery/";
        public static string BannerMediaUrl { get; set; } = MediaBaseUrl + "Banner";
        public static string ProfileMediaUrl { get; set; } = MediaBaseUrl + "Profile";
        public static string GalleryImagesMediaUrl { get; set; } = MediaBaseUrl + "GalleryImages";
        public static string GalleryVideosMediaUrl { get; set; } = MediaBaseUrl + "GalleryVideos";
        public static string AdsPhoto { get; set; } = MediaBaseUrl + "AdsPhotos";
        public static string RoomatePhoto { get; set; } = MediaBaseUrl + "RoomatePhotos";
        public static string KartMelliDoc { get; set; } = MediaBaseUrl + "KartMelliDocs";
        public static string PassportDoc { get; set; } = MediaBaseUrl + "PassportDocs";
        public static string TicketDoc { get; set; } = MediaBaseUrl + "Ticket";
        public static string VideoIdentity { get; set; } = MediaBaseUrl + "VideoIdentity";
        public static string Group { get; set; } = MediaBaseUrl + "Group";
        public static string LawyerCertificate { get; set; } = MediaBaseUrl + "LawyerCertificate";
        public static string Newspaper { get; set; } = MediaBaseUrl + "Newspaper";
        public static string Service { get; set; } = MediaBaseUrl + "Service";
        public static string ServiceCategory { get; set; } = MediaBaseUrl + "ServiceCategory";
        public static string PostMediaUrl { get; set; } = MediaBaseUrl + "Post";
    }
}
