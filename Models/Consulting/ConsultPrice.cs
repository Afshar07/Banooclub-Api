namespace BanooClub.Models.Consulting
{
    public class ConsultantPrice: BaseEntity
    {
        public long ConsultantId { get; set; }
        public Consultant Consultant { get; set; }
        public ConsultTypeEnum Type { get; set; }
        public long Price { get; set; }
    }
}
