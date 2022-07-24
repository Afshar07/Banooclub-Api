using System;

namespace BanooClub.Models
{
    public class ServicePlan : BaseEntity
    {
        public long ServicePlanId { get; set; }
        public long ServiceId { get; set; }
        public long PlanId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
