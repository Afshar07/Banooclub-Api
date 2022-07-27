namespace BanooClub.Models
{
    public class OrderItem : BaseEntity
    {
        public long OrderItemId { get; set; }
        public string Title { get; set; }
        public long OrderId { get; set; }
        public long PlanId { get; set; }
        public int Count { get; set; }
        public long VendorUserId { get; set; }
        public long Price { get; set; }
        public long? ServiceId { get; set; }
    }
}
