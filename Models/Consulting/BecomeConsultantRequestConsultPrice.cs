namespace BanooClub.Models.Consulting
{
    public class BecomeConsultantRequestConsultPrice: BaseEntity
    {
        public long BecomeConsultantRequestId { get; set; }
        public BecomeConsultantRequest BecomeConsultantRequest { get; set; }
        public ConsultTypeEnum Type { get; set; }
        public long Price { get; set; }
    }
}
