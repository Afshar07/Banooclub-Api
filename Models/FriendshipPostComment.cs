namespace BanooClub.Models
{
    public class FriendshipPostComment : BaseEntity
    {
        public long FriendshipPostCommentId { get; set; }
        public long PostId { get; set; }
        public int Type { get; set; }
        public string Comment { get; set; }
        public long ParentId { get; set; }
        public long UserId { get; set; }
    }
}
