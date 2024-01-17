using System;

namespace BanooClub.Models
{
    public class ForumCategory : BaseEntity
    {
        public long ForumCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
