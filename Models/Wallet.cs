using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Wallet : BaseEntity
    {
        public long WalletId { get; set; }
        public long Credit { get; set; }
        public long UserId { get; set; }
        public DateTime UpdateDate { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
