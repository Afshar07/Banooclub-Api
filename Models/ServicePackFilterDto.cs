using BanooClub.Models.Enums;

namespace BanooClub.Models
{
    public class ServicePackFilterDto
    {
        public int PageNumber { get; set; }
        public byte Count { get; set; }
        public string SearchCommand { get; set; }
        public ServicePackStatus? Status { get; set; }
        public long CategoryId { get; set; }
        public long FromPrice { get; set; }
        public long ToPrice { get; set; }
        public long CityId { get; set; }
        public long StateId { get; set; }
        public bool HasDiscount { get; set; }
        public bool IsFunTime { get; set; }
    }
}
