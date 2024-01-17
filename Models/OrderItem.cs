using BanooClub.Models.Consulting;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class OrderItem : BaseEntity
    {
        public long OrderItemId { get; set; }
        public string Title { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long PlanId { get; set; }
        public int Count { get; set; }
        public long VendorUserId { get; set; }
        public long Price { get; set; }
        public long? ServiceId { get; set; }
        public ConsultTypeEnum? ConsultType { get; set; }
        public long? ConsultantId { get; set; }
        public Consultant Consultant { get; set; }

        [NotMapped]
        public ServicePack ServiceInfo { get; set; }
    }
}
