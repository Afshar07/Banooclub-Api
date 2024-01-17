namespace BanooClub.Models
{
    public class Report : BaseEntity
    {
        public long ReportId { get; set; }
        public int ReasonType { get; set; }
        public int SRType { get; set; }
        public long SRId { get; set; }
    }
}
