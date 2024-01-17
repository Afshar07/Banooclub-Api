using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class FollowRequest : BaseEntity
    {
        public long FollowRequestId { get; set; }
        public long FollowerUserId { get; set; }
        public long FollowingUserId { get; set; }
        [NotMapped]
        public User FollowerInfo { get; set; }
    }
}
