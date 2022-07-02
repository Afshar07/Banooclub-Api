
using System;

namespace BanooClub.Models
{
    public class Log : BaseEntity
    {
        public long LogId { get; set; }
        public string IP { get; set; }
        public string BrowserOS { get; set; }
        public long UserId { get; set; }
        public long ViewUserId { get; set; }
        public string URL { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
