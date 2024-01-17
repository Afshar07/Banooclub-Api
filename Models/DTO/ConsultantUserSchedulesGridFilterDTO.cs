using BanooClub.Models.Enums;
using System;

namespace BanooClub.Models.DTO
{
    public class ConsultantUserSchedulesGridFilterDTO
    {
        public string userFullname { get; set; }
        public string consultFullname { get; set; }
        public DateTime? createDate { get; set; }
        public ConsultantUserScheduleStatus? status { get; set; }
        public TimeSpan? reserveHour { get; set; }
        public bool? isPayed { get; set; }
        public int? pageNumber { get; set; }
        public int? take { get; set; }
    }
}
