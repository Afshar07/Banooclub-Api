using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class BlogComment : BaseEntity
    {
        public long BlogCommentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public long BlogId { get; set; }
        public long UserId { get; set; }
        public long? ParentId { get; set; }
        public DateTime CreateDate { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public string BlogTitle { get; set; }
        [NotMapped]
        public string BlogSEOURL { get; set; }
    }
    public class BlogCommentDTO
    {
        public long BlogId { get; set; }
        public string Message { get; set; }
        public long? ParentId { get; set; }
    }
}
