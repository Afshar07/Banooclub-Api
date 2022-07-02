using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class AccountSetting : BaseEntity
    {
        public long AccountSettingId { get; set; }
        public int? Gender { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string AboutMe { get; set; }
        public long UserId { get; set; }
        public int? GraduatedLevel { get; set; }
        public string GraduatedIn { get; set; }
        public DateTime? GraduatedFrom { get; set; }
        public DateTime? GraduatedTo { get; set; }
        public int? GraduatedCountry { get; set; }
        public int? GraduatedProvince { get; set; }
        public int? GraduatedCity { get; set; }
        public string Favorites { get; set; }
        public bool FollowMe { get; set; }
        public bool SendNotification { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsEnabled { get; set; }
        [NotMapped]
        public string ProfileImage { get; set; }
        [NotMapped]
        public string BannerImage { get; set; }

    }
}
