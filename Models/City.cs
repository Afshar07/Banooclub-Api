using BanooClub.Models.Consulting;
using System;
using System.Collections.Generic;

namespace BanooClub.Models
{
    public class City : BaseEntity
    {
        public City()
        {
            Consultants = new List<Consultant>();
        }

        public long CityId { get; set; }
        public string Name { get; set; }
        public long StateId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public List<Consultant> Consultants { get; set; }
    }
}
