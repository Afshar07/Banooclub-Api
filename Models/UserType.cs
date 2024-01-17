namespace BanooClub.Models
{
    public class UserType : BaseEntity
    {
        public long UserTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
