using System;

namespace BanooClub.Models
{
    public class Shipping : BaseEntity
    {
        public long ShippingId { get; set; }
        public long ShipingType { get; set; }
        public long CityId { get; set; }
        public long StateId { get; set; }
        public long Price { get; set; }
        public long PriceLimit { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
