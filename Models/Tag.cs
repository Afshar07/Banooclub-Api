using BanooClub.Models.Enums;
using System;

namespace BanooClub.Models
{
    public class Tag : BaseEntity
    {
        public long TagId { get; set; }
        public TagType Type { get; set; }
        public long ObjectId { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
