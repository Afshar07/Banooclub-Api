using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class AdsCategory : BaseEntity
    {
        public long AdsCategoryId { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        [NotMapped]
        public string ParentName { get; set; }
        [NotMapped]
        public string FileData { get; set; }
    }
}
