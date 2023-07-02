using System;

namespace BanooClub.Models.Consulting
{
    public class BecomeConsultantRequestSchedule: BaseEntity
    {
        public long BecomeConsultantRequestScheduleId { get; set; }
        public long BecomeConsultantRequestId { get; set; }
        public BecomeConsultantRequest BecomeConsultantRequest { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EntTime { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
