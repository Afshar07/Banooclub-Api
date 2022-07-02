using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class PostReport : BaseEntity
    {
        public long PostReportId { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public bool IsConfirmed { get; set; }
        public string Reason { get; set; }
        public long ReporterUserId { get; set; }
        public DateTime CreateDate { get; set; }
        [NotMapped]
        public string Content { get; set; }
        [NotMapped]
        public User ReporterUserInfo { get; set; }
        [NotMapped]
        public User ReportedUserInfo { get; set; }

    }
}
