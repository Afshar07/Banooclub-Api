using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Group : BaseEntity
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public List<long> Members { get; set; }
        [NotMapped]
        public List<User> UserInfoes { get; set; }
        [NotMapped]
        public string FileData { get; set; }
    }
}