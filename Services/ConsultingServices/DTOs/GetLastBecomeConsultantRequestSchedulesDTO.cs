using System;

namespace BanooClub.Services.ConsultingServices.DTOs
{
    public class GetLastBecomeConsultantRequestSchedulesDTO
    {
        public long id { get; set; }
        public bool isSelected { get; set; }
        public TimeSpan startHour { get; set; }
        public TimeSpan endHour { get; set; }

    }
}
