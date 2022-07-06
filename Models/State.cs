namespace BanooClub.Models
{
    public class State : BaseEntity
    {
        public long StateId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
