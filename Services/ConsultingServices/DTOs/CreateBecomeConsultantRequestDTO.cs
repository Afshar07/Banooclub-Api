namespace BanooClub.Services.ConsultingServices.DTOs
{
    public class CreateBecomeConsultantRequestDTO
    {
    }

    public class CreateConsultCategoryDTO
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }

    public class ConsultCategoryDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
    public class UpdateConsultCategoryDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}
