namespace BanooClub.Models.Consulting
{
    public class ConsultantConsultCategory: BaseEntity
    {
        public long ConsultantId { get; set; }
        public Consultant Consultant { get; set; }
        public ConsultCategory ConsultCategory { get; set; }
        public long ConsultCategoryId { get; set; }
    }
}
