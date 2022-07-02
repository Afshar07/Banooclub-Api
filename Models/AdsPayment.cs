namespace BanooClub.Models
{
    public class AdsPayment : BaseEntity
    {
        public long AdsPaymentId { get; set; }
        public long AdsId { get; set; }
        public long PackageId { get; set; }
        public long SystemPaymentId { get; set; }
    }
}
