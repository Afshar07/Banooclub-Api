namespace BanooClub.Models
{
    public class Formal : BaseEntity
    {
        public long FormalId { get; set; }
        public long UserId { get; set; }
        public string LicenceNumber { get; set; }
        public string About { get; set; }
        public int Type { get; set; }
        public int Experience { get; set; }
        public string CEO { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Tel3 { get; set; }
        public string Tel4 { get; set; }
        public string Website { get; set; }
        public int Status { get; set; }
    }
}
