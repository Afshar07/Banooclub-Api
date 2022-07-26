using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Ticket : BaseEntity
    {
        public long TicketId { get; set; }
        public long ParentId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int Type { get; set; }
        public long UserId { get; set; }
        public bool IsRead { get; set; }
        public long UserType { get; set; }
        public string Title { get; set; }
        public bool IsClosed { get; set; }
        [NotMapped]
        public string FileData { get; set; }
        [NotMapped]
        public bool UnRead { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
