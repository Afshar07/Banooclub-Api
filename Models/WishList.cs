using System;

namespace BanooClub.Models
{
    public class WishList : BaseEntity
    {
        public long WishListId { get; set; }
        public long ServiceId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
