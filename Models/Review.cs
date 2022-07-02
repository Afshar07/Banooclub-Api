using System;

namespace BanooClub.Models
{
    public class Review : BaseEntity
    {
        public long ReviewId { get; set; }
        public long? UserId { get; set; }
        public string Ip { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
