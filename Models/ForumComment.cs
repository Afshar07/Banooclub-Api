using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class ForumComment : BaseEntity
    {
        public long ForumCommentId { get; set; }
        public long ForumId { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public long ParentId { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public long Vote { get; set; }
    }
}
