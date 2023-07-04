using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;

namespace BanooClub.Models.Consulting
{
    public class ConsultantUserSchedule : BaseEntity
    {
        public ConsultantUserSchedule()
        {
            ConsultantUserScheduleRatings = new List<ConsultantUserScheduleRating>();
        }

        public long ConsultantUserScheduleId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long ConsultantId { get; set; }
        public Consultant Consultant { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime TargetDate { get; set; }
        public TimeSpan ReserveTime { get; set; }
        public ConsultantUserScheduleStatus Status { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public bool IsPayed { get; set; }

        public List<ConsultantUserScheduleRating> ConsultantUserScheduleRatings { get; set; }
    }
}
