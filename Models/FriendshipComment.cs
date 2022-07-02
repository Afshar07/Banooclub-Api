namespace BanooClub.Models
{
    public class FriendshipComment : BaseEntity
    {
        public long FriendshipCommentId { get; set; }
        public long FriendshipId { get; set; }
        public int Type { get; set; }
        public string Comment { get; set; }
        public long ParentId { get; set; }
        public long UserId { get; set; }
    }
}
