using System;

namespace BanooClub.Models
{
    public class City : BaseEntity
    {
        public long CityId { get; set; }
        public string Name { get; set; }
        public long StateId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
