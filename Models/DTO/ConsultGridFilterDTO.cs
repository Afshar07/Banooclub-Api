using BanooClub.Models.Enums;
using System.Collections.Generic;

namespace BanooClub.Models.DTO
{
    public class ConsultGridFilterDTO
    {
        public ConsultGridFilterDTOSortType? sort { get; set; }
        public List<long> catIds { get; set; }
        public string searchInput { get; set; }
        public bool? isActive { get; set; }

        public int? pageNumber { get; set; }
        public int? take { get; set; }
    }
}
