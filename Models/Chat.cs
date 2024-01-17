using System;

namespace BanooClub.Models
{
    public class Chat : BaseEntity
    {
        public long ChatId { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public long Priority { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
    }
}
