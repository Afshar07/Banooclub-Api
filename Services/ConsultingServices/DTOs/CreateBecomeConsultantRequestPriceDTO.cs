using BanooClub.Models.Consulting;

namespace BanooClub.Services.ConsultingServices.DTOs
{
    public class CreateBecomeConsultantRequestPriceDTO
    {
        public long? Price { get; set; }
        public ConsultTypeEnum? Type { get; set; }
    }
}
