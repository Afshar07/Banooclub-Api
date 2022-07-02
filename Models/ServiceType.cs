namespace BanooClub.Models
{
    public class ServiceType : BaseEntity
    {
        public long TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public long ParentId { get; set; }
    }
}
