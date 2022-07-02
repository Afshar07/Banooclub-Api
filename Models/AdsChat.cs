using System;

namespace BanooClub.Models
{
    public class AdsChat : BaseEntity
    {
        public long AdsChatId { get; set; }
        public long AdsId { get; set; }
        public DateTime Date { get; set; }
        public long Priority { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
    }
}
