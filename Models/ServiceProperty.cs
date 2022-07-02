using System;

namespace BanooClub.Models
{
    public class ServiceProperty : BaseEntity
    {
        public long ServicePropertyId { get; set; }
        public long ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ServiceCategoryId { get; set; }
        public long Price { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
