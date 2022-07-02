using BanooClub.Models.Enums;
using System;

namespace BanooClub.Models
{
    public class Activity : BaseEntity
    {
        public long ActivityId { get; set; }
        public long UserId { get; set; }
        public ActivityTypes Type { get; set; }
        public long ObjectId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
    }
}
