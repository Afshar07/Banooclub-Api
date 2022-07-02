namespace BanooClub.Models
{
    public class Country : BaseEntity
    {
        public long Id { get; set; }
        public string Iso { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public int? NumCode { get; set; }
        public int PhoneCode { get; set; }
    }
}
