using System;

namespace BanooClub.Models.Consulting
{
    public class ConsultantSchedule: BaseEntity
    {
        public long ConsultantScheduleId { get; set; }
        public long ConsultantId { get; set; }
        public Consultant Consultant { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EntTime { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
