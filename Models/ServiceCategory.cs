using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class ServiceCategory : BaseEntity
    {
        public long ServiceCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        [NotMapped]
        public string FileData { get; set; }
    }
}
