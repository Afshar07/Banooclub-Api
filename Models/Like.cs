using BanooClub.Models.Enums;
using System;

namespace BanooClub.Models
{
    public class Like : BaseEntity
    {
        public long LikeId { get; set; }
        public long ObjectId { get; set; }
        public LikeType Type { get; set; }
        public LikeStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long UserId { get; set; }
    }
}
