namespace BanooClub.Models.Consulting
{
    public class BecomeConsultantRequestConsultCategory : BaseEntity
    {
        public long RequestId { get; set; }
        public BecomeConsultantRequest Request { get; set; }
        public long CategoryId { get; set; }
        public ConsultCategory Category { get; set; }
    }
}
