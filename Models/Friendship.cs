namespace BanooClub.Models
{
    public class Friendship : BaseEntity
    {
        public long FriendshipId { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
        public int Sport { get; set; }
        public int Smoke { get; set; }
        public int Alcohol { get; set; }
        public int Mariage { get; set; }
        public int Education { get; set; }
        public int Job { get; set; }
        public int Religion { get; set; }
        public int Nationality { get; set; }
        public string Langs { get; set; }
        public long UserId { get; set; }
        public long SystemPaymentId { get; set; }
    }
}
