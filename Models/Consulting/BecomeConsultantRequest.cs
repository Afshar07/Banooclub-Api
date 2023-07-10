using System;
using System.Collections.Generic;

namespace BanooClub.Models.Consulting
{
    public class BecomeConsultantRequest : BaseEntity
    {
        private BecomeConsultantRequest()
        {
        }

        public BecomeConsultantRequest(long userId, string nationalCode, string medicalSystemNumber, long stateId, long cityId, string phoneNumber, string shabaNo, string bankName, string cartNo, string description, int? durationMinut)
        {
            UserId = userId;
            NationalCode = nationalCode;
            MedicalSystemNumber = medicalSystemNumber;
            StateId = stateId;
            CityId = cityId;
            PhoneNumber = phoneNumber;
            ShabaNo = shabaNo;
            BankName = bankName;
            CartNo = cartNo;
            CreateDate = DateTime.Now;
            Status = BecomeConsultantRequestStatus.Pending;
            Categories = new List<BecomeConsultantRequestConsultCategory>();
            Description = description;
            ConsultingDurationMinut = durationMinut.Value;
            BecomeConsultantRequestConsultPrices = new List<BecomeConsultantRequestConsultPrice>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string NationalCode { get; set; }
        public string MedicalSystemNumber { get; set; }
        public long StateId { get; set; }
        public State State { get; set; }
        public City City { get; set; }
        public long CityId { get; set; }
        public string PhoneNumber { get; set; }
        public string ShabaNo { get; set; }
        public string BankName { get; set; }
        public string CartNo { get; set; }
        public BecomeConsultantRequestStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int ConsultingDurationMinut { get; set; }
        public string Description { get; set; }

        public List<BecomeConsultantRequestConsultCategory> Categories { get; set; }
        public List<BecomeConsultantRequestConsultPrice> BecomeConsultantRequestConsultPrices { get; set; }
        public List<BecomeConsultantRequestSchedule> BecomeConsultantRequestSchedules { get; set; }
    }
}
