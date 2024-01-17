using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Following : BaseEntity
    {
        public long FollowingId { get; set; }
        public long UserId { get; set; }
        public long FollowingUserId { get; set; }
        public DateTime CreateDate { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
