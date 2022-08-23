using BanooClub.Models.Enums;
using System;

namespace BanooClub.Models
{
    public class Discount : BaseEntity
    {
        public long DiscountId { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DiscountType Type { get; set; }
        public long Value { get; set; }
        public DateTime CreateDate { get; set; }
        public long ServicePackId { get; set; }
        public bool Refraction { get; set; }
        public DateTime? StartDate { get; set; }
    }

    public class DiscountDto
    {
        public DateTime? ExpireDate { get; set; }
        public DiscountType Type { get; set; }
        public long Value { get; set; }
        public bool Refraction { get; set; }
        public long ServicePackId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
