using BanooClub.Models.DTO;
using System.Collections.Generic;

namespace BanooClub.Services.ConsultingServices.DTOs
{
    public class ConsultantScheualesDTO
    {
        public long? ConsultantId { get; set; }
        public List<CreateBecomeConsultantRequestRealSelectedDayDTO> SelectedStartedTimes { get; set; }
    }
}
