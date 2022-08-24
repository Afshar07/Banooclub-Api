using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class ServicePack : BaseEntity
    {
        public long ServicePackId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public long ServiceCategoryId { get; set; }
        public string Description { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public long UserId { get; set; }
        public decimal Longitude { get; set; }
        public string WebAddress { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime FireDate { get; set; }
        public DateTime? StartDate { get; set; }
        public bool Specified { get; set; }
        public bool SpecifiedWithExpireDate { get; set; }
        public DateTime? SpecifiedExpireDateTime { get; set; }
        public long TotalPrice { get; set; }
        public DateTime? ExpireDate { get; set; }
        public ServicePackStatus Status { get; set; }
        public int Quantity { get; set; }
        public bool? IsFree { get; set; }
        public int Maintain { get; set; }
        [NotMapped]
        public Discount Discount { get; set; }
        [NotMapped]
        public long ViewsCount { get; set; }
        [NotMapped]
        public object WeekViews { get; set; }
        [NotMapped]
        public double? Rate { get; set; }
        [NotMapped]
        public List<FileData> Medias { get; set; }
        [NotMapped]
        public List<Tag> Tags { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public List<ServiceComment> Comments { get; set; }
        [NotMapped]
        public List<ServiceProperty> Properties { get; set; }
        [NotMapped]
        public bool IsFavourite { get; set; }
        [NotMapped]
        public string CatName { get; set; }
        [NotMapped]
        public List<int> PlanTypes { get; set; }
    }
}
