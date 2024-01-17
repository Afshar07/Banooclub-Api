namespace BanooClub.Models
{
    public class SystemPayment : BaseEntity
    {
        public long SystemPaymentId { get; set; }
        public long Amount { get; set; }
        public int Status { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
    }
}
