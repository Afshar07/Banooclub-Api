using System;

namespace BanooClub.Models.Consulting
{
    public class ConsultantUserScheduleRating: BaseEntity
    {
        public long ConsultantUserScheduleRatingId { get; set; }
        public byte Rate { get; set; }
        public long ConsultantUserScheduleId { get; set; }
        public ConsultantUserSchedule ConsultantUserSchedule { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsConfirm { get; set; }
    }
}
