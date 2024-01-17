using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class ServiceComment : BaseEntity
    {
        public long ServiceCommentId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public long ServiceId { get; set; }
        public string Text { get; set; }
        public long ParentId { get; set; }
        public long BaseId { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public List<CommentLike> UserCommentLikes { get; set; }
        [NotMapped]
        public List<ServiceComment> Children { get; set; }
        [NotMapped]
        public long CommentsLikesCount { get; set; }
        [NotMapped]
        public bool IsLikedByMe { get; set; }
    }
}
