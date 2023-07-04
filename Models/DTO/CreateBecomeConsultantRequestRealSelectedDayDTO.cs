using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;

namespace BanooClub.Models.DTO
{
    public class CreateBecomeConsultantRequestRealSelectedDayDTO
    {
        public MyDayOfWeek? dayOfWeek { get; set; }
        public List<TimeSpan> selectedTime { get; set; }
    }
}
