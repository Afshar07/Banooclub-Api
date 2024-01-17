using System.Collections.Generic;

namespace BanooClub.Models
{
    public class MapOutputDTO
    {
        public float latitude { get; set; }
        public float longtitude { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public double distance { get; set; }
        public string number { get; set; }
        public string street { get; set; }
        public string postal_code { get; set; }
        public double confidence { get; set; }
        public string region { get; set; }
        public string region_code { get; set; }
        public string neighbourhood { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string map_url { get; set; }


    }

    public class dataDTO
    {
        public List<MapOutputDTO> data { get; set; }
    }
}
