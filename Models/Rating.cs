using BanooClub.Models.Enums;
using System;

namespace BanooClub.Models
{
    public class Rating : BaseEntity
    {
        public long RatingId { get; set; }
        public long ObjectId { get; set; }
        public RatingType Type { get; set; }
        public DateTime CreateDate { get; set; }
        public int Rate { get; set; }
        public long UserId { get; set; }
    }
    public class RateOveral
    {
        public int FiveStar { get; set; }
        public int FourStar { get; set; }
        public int ThreeStar { get; set; }
        public int TwoStar { get; set; }
        public int OneStar { get; set; }

        public int RateCount { get; set; }
        public double Average { get; set; }

    }
}
