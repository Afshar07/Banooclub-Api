using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class WishList : BaseEntity
    {
        public long WishListId { get; set; }
        public long ObjectId { get; set; }
        public WishListType Type { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        [NotMapped]
        public ServicePack ServiceInfo { get; set; }
        [NotMapped]
        public Ads AdsInfo { get; set; }
    }
}
