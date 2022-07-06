namespace BanooClub.Models
{
    public class UserComment : BaseEntity
    {
        public long UserCommentId { get; set; }
        public long TargetId { get; set; }
        public int Type { get; set; }
        public string Comment { get; set; }
        public long ParentId { get; set; }
        public long UserId { get; set; }
        
    }
}
