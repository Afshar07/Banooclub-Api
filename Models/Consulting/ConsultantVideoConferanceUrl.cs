namespace BanooClub.Models.Consulting
{
    public class ConsultantVideoConferanceUrl: BaseEntity
    {
        public long ConsultantId { get; set; }
        public Consultant Consultant { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public string ConferanceUrl { get; set; }
    }
}
