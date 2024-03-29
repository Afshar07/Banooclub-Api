﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Ads : BaseEntity
    {
        public long AdsId { get; set; }
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public long Price { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int Status { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Tag { get; set; }
        public int Condition { get; set; }
        public long CityId { get; set; }
        public long StateId { get; set; }
        public DateTime FireDate { get; set; }
        public bool Exchangeability { get; set; }
        public long PhoneNumber { get; set; }
        public bool IsShowNumber { get; set; }

        [NotMapped]
        public string City { get; set; }
        [NotMapped]
        public string State { get; set; }

        [NotMapped]
        public List<FileData> Photos { get; set; }
        [NotMapped]
        public string Reason { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public string AdsCategoryParents { get; set; }
        [NotMapped]
        public bool? IsFavourite { get; set; }
        [NotMapped]
        public List<int> PlanTypes { get; set; }
    }

}
