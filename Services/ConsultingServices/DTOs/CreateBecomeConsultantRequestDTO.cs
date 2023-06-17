using BanooClub.Models.Consulting;
using BanooClub.Models;
using System.Collections.Generic;

namespace BanooClub.Services.ConsultingServices.DTOs
{
    public class CreateBecomeConsultantRequestDTO
    {
        public long Id { get; set; }
        public string NatiaonCode { get; set; }
        public string MedicalSystemNumber { get; set; }
        public List<long> Categories { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public string PhoneNumber { get; set; }
        public string ShabaNo { get; set; }
        public string BankName { get; set; }
        public string CartNo { get; set; }
    }

    public class BecomeConsultantRequestDTO
    {
        public long Id { get; set; }
        public string NatiaonCode { get; set; }
        public string MedicalSystemNumber { get; set; }
        public List<long> Categories { get; set; }
        public long StateId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public long CityId { get; set; }
        public string PhoneNumber { get; set; }
        public string ShabaNo { get; set; }
        public string BankName { get; set; }
        public string CartNo { get; set; }
        public string UserFullName { get; set; }
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
