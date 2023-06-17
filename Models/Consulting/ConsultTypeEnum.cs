using System.Collections.Generic;

namespace BanooClub.Models.Consulting
{
    //public enum ConsultTypeEnum
    //{
    //    PhoneConsultancy = 1,
    //    ImmediatePhoneConsultancy = 2,
    //    VedioConsultancy = 3,
    //}

    //public enum BecomeConsultantRequestStatus
    //{
    //    Pending = 0,
    //    Accepted = 1,
    //    Rejected = 2,
    //}
    //public class BecomeConsultantRequest : BaseEntity
    //{
    //    public long Id { get; set; }
    //    public long UserId { get; set; }
    //    public string NatiaonCode { get; set; }
    //    public string MedicalSystemNumber { get; set; }
    //    public ICollection<BecomeConsultantRequestConsultCategory> Categories { get; set; }
    //    public long StateId { get; set; }
    //    public State State { get; set; }
    //    public City City { get; set; }
    //    public long CityId { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string ShabaNo { get; set; }
    //    public string BankName { get; set; }
    //    public string CartNo { get; set; }
    //    public BecomeConsultantRequestStatus Status { get; set; }
    //}

    //public class Consultant : BaseEntity
    //{
    //    public long Id { get; set; }
    //    public long UserId { get; set; }
    //    public string NatiaonCode { get; set; }
    //    public string MedicalSystemNumber { get; set; }
    //    public ICollection<ConsultCategory> Categories { get; set; }
    //    public long StateId { get; set; }
    //    public State State { get; set; }
    //    public City City { get; set; }
    //    public long CityId { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string ShabaNo { get; set; }
    //    public string BankName { get; set; }
    //    public string CartNo { get; set; }
    //}

    //public class BecomeConsultantRequestConsultCategory : BaseEntity
    //{
    //    public BecomeConsultantRequest Request { get; set; }
    //    public long RequestId { get; set; }
    //    public long CategoryId { get; set; }
    //    public ConsultCategory Category { get; set; }
    //}


    public class ConsultCategory : BaseEntity
    {
        private ConsultCategory()
        {

        }
        public ConsultCategory(string title, long? parentId = null)
        {
            Title = title;
            ParentId = parentId;
        }
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
    }
}
