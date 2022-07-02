using System;

namespace BanooClub.Models
{
    public class ViewHistory : BaseEntity
    {
        public long ViewHistoryId { get; set; }
        public long UserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long FirstPostId { get; set; }
        public long LastPostId { get; set; }
    }
}
