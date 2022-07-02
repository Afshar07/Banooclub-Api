namespace BanooClub.Models
{
    public class Media : BaseEntity
    {
        public long MediaId { get; set; }
        public int Type { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public int IsDefault { get; set; }
        public int SRType { get; set; }
        public long SRId { get; set; }
    }
}
