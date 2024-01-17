namespace BanooClub.Models
{
    public class BlogCategory : BaseEntity
    {
        public long BlogCategoryId { get; set; }
        public string Name { get; set; }
        public string? SEOTitle { get; set; }
        public string? SEODescription { get; set; }
        public string? SEOURL { get; set; }
    }
}
