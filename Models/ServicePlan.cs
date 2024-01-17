using BanooClub.Models.Enums;
using System;

namespace BanooClub.Models
{
    public class ServicePlan : BaseEntity
    {
        public long ServicePlanId { get; set; }
        public long ObjectId { get; set; }
        public long PlanId { get; set; }
        public ServicePlanType Type { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
