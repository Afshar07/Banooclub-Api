using System;

namespace BanooClub.Models
{
    public class PostLike : BaseEntity
    {
        public long PostLikeId { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
