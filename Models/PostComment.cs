using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class PostComment : BaseEntity
    {
        public long PostCommentId { get; set; }
        public long PostId { get; set; }
        public long ParentId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public long UserId { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
