using System;
using System.Collections.Generic;

namespace BanooClub.Models.Consulting
{
    public class Consultant : BaseEntity
    {
        private Consultant()
        {
            ConsultantPrices = new List<ConsultantPrice>();
            Categories = new List<ConsultantConsultCategory>();
            ConsultantSchedules = new List<ConsultantSchedule>();
            ConsultantVideoConferanceUrls = new List<ConsultantVideoConferanceUrl>();
            ConsultantUserSchedules = new List<ConsultantUserSchedule>();
        }
        public Consultant(long userId, string nationalCode, string medicalSystemNumber, long stateId, long cityId, string phoneNumber, string shabaNo, string bankName, string cartNo, string description, int consultingDurationMinut)
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
            Description = description;
            ConsultingDurationMinut = consultingDurationMinut;
            CreateDate = DateTime.Now;
        }


        public void Update(string nationalCode, string medicalSystemNumber, long stateId, long cityId, string phoneNumber, string shabaNo, string bankName, string cartNo, string description, int consultingDurationMinut)
        {
            NationalCode = nationalCode;
            MedicalSystemNumber = medicalSystemNumber;
            StateId = stateId;
            CityId = cityId;
            PhoneNumber = phoneNumber;
            ShabaNo = shabaNo;
            BankName = bankName;
            CartNo = cartNo;
            Description = description;
            ConsultingDurationMinut = consultingDurationMinut;
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
        public DateTime CreateDate { get; set; }
        public int ConsultingDurationMinut { get; set; }
        public string Description { get; set; }

        public List<ConsultantConsultCategory> Categories { get; set; }
        public List<ConsultantPrice> ConsultantPrices { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<ConsultantSchedule> ConsultantSchedules { get; set; }
        public List<ConsultantVideoConferanceUrl> ConsultantVideoConferanceUrls { get; set; }
        public List<ConsultantUserSchedule> ConsultantUserSchedules { get; set; }
    }
}
