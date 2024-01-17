using System;

namespace BanooClub.Models.DTO
{
    public class ConsultantUserScheduleRatingGridFilterDTO
    {
        public string consultFullname { get; set; }
        public string userFullname { get; set; }
        public byte? rate { get; set; }
        public byte? minRate { get; set; }
        public byte? maxRate { get; set; }
        public DateTime? createDate { get; set; }
        public bool? isConfirm { get; set; }

        public int? pageNumber { get; set; }
        public int? take { get; set; }
    }
}
