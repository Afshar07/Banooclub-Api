namespace BanooClub.Models
{
    public class FriendshipPost : BaseEntity
    {
        public long FriendshipPostId { get; set; }
        public long FriendshipId { get; set; }
        public string Description { get; set; }
    }
}
