namespace BanooClub.Models
{
    public class Package : BaseEntity
    {
        public long PackageId { get; set; }
        public int Type { get; set; }
        public int Price { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
    }
}
