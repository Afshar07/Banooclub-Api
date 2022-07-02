using System;

namespace BanooClub.Models
{
    public class City : BaseEntity
    {
        public long CityId { get; set; }
        public string Name { get; set; }
        public long StateId { get; set; }
        public string StateCode { get; set; }
        public long CountryId { get; set; }
        public string CountryCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Flag { get; set; }
        public string WikiDataId { get; set; }
    }
}
