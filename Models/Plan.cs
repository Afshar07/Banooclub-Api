using System;

namespace BanooClub.Models
{
    public class Plan : BaseEntity
    {
        public long PlanId { get; set; }
        public DateTime CreateDate { get; set; }
        public long Price { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
