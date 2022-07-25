using BanooClub.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class UserSetting : BaseEntity
    {
        public long UserSettingId { get; set; }
        public long UserId { get; set; }
        public bool? IsPrivateActivity { get; set; }
        public bool? IsPrivateRoomate { get; set; }
        public bool? IsPrivateAds { get; set; }
        public bool? IsPrivateSocial { get; set; }
        public bool? IsPrivatePost { get; set; }
        public bool? IsPrivateGalleryFriend { get; set; }
        public string UserTag { get; set; }
        public string Flag { get; set; }
        public VideoIdentity VideoIdentityStatus { get; set; }
        public DateTime? VideoIdentityUpdateDate { get; set; }
        public string Bio { get; set; }
        public int? Gender { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? BirthDate { get; set; }
        public DocStatus LawyerCertificateStatus { get; set; }
        public DocStatus NewspaperStatus { get; set; }
        public bool ActiveRoomate { get; set; }
        [NotMapped]
        public string KartMelliDoc { get; set; }
        [NotMapped]
        public string PassportDoc { get; set; }
        [NotMapped]
        public string VideoIdentity { get; set; }
        [NotMapped]
        public string NewspaperDoc { get; set; }
        [NotMapped]
        public string LawyerCertificateDoc { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
