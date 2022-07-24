using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Forum : BaseEntity
    {
        public long ForumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public long ForumCategoryId { get; set; }
        public ForumStatus Status { get; set; }
        [NotMapped]
        public long ViewsCount { get; set; }
        [NotMapped]
        public long Vote { get; set; }
        [NotMapped]
        public double? Rate { get; set; }
        [NotMapped]
        public List<Tag> Tags { get; set; }
        [NotMapped]
        public List<ForumComment> Comments { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
