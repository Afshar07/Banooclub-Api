using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Roomate : BaseEntity
    {
        public long RoomateId { get; set; }
        public int RoomType { get; set; }
        public int BedroomCount { get; set; }
        public int BedroomType { get; set; }
        public int BathroomCount { get; set; }
        public int BathroomType { get; set; }
        public int OwnerType { get; set; }
        public string Address { get; set; }
        public long? Longtitude { get; set; }
        public long? Latitude { get; set; }
        public int? HaveLobbyMan { get; set; }
        public int? HaveFurniture { get; set; }
        public int? HaveCCTV { get; set; }
        public int? HaveElevator { get; set; }
        public long? DailyRent { get; set; }
        public long? Mortgage { get; set; }
        public string ReceptionDate { get; set; }
        public int? WithElectricity { get; set; }
        public int? WithWater { get; set; }
        public int? WithGaz { get; set; }
        public int? WithInternet { get; set; }
        public int? WithGarbage { get; set; }
        public string RoomateGender { get; set; }
        public int? RoomateAgeRangeFrom { get; set; }
        public int? RoomateAgeRangeTo { get; set; }
        public int? RoomateSmoke { get; set; }
        public int? RoomateChild { get; set; }
        public int? ResidenceNumber { get; set; }
        public int? OwnerGender { get; set; }
        public int? OwnerAge { get; set; }
        public string OwnerLangs { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public long? PaymentId { get; set; }
        public int? HaveParking { get; set; }
        [NotMapped]
        public List<FileData> Photos { get; set; }
        [NotMapped]
        public bool? IsPrivate { get; set; }
    }
}
