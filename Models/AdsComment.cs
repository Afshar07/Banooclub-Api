namespace BanooClub.Models
{
    public class AdsComment : BaseEntity
    {
        public long AdsCommentId { get; set; }
        public long AdsId { get; set; }
        public int Type { get; set; }
        public string Comment { get; set; }
        public long ParentId { get; set; }
        public long UserId { get; set; }
    }
}
