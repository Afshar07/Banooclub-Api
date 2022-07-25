using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Blog : BaseEntity
    {
        public long BlogId { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public long BlogCategoryId { get; set; }
        public long UserId { get; set; }
        public string? SEOTitle { get; set; }
        public string? SEODescription { get; set; }
        public string? SEOURL { get; set; }
        [NotMapped]
        public string FileData { get; set; }
        [NotMapped]
        public long CommentsCount { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public long ViewsCount { get; set; }
        [NotMapped]
        public bool IsFavourite { get; set; }
        [NotMapped]
        public double? Rate { get; set; }
        [NotMapped]
        public List<Tag> Tags { get; set; }
        [NotMapped]
        public List<BlogComment> Comments { get; set; }
        [NotMapped]
        public long LikeCount { get; set; }
    }
}
