using System;

namespace BanooClub.Models
{
    public class ShippingType : BaseEntity
    {
        public long ShippingTypeId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate{ get; set; }
    }
}
