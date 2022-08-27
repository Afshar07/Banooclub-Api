using BanooClub.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Payment : BaseEntity
    {
        public long PaymentId { get; set; }
        public long Amount { get; set; }
        public int Status { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public long OrderId { get; set; }
        public string TransId { get; set; }
        public string RefId { get; set; }
        public bool? WalletCharge { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public Order OrderInfo { get; set; }
    }
    public class PurchasedPlansByUserDto
    {
        public long ServiceId { get; set; }
        public long PlanId { get; set; }
        public int BuyCount { get; set; }
        public string Title { get; set; }
    }
    public class IncomeDto
    {
        public long Price { get; set; }
    }
}
