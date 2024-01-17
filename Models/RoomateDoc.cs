using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class RoomateDoc : BaseEntity
    {
        public long RoomateDocId { get; set; }
        public long UserId { get; set; }
        public string KartMelliImage { get; set; }
        public string PassportImage { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
        public string StateDesc { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
