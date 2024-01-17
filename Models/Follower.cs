using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Follower : BaseEntity
    {
        public long FollowerId { get; set; }
        public long UserId { get; set; }
        public long FollowerUserId { get; set; }
        public DateTime CreateDate { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
