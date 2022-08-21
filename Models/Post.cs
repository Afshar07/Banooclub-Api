using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Post : BaseEntity
    {
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Status { get; set; }
        [NotMapped]
        public bool IsLikedByMe { get; set; }
        [NotMapped]
        public bool IsReportedByMe { get; set; }
        [NotMapped]
        public bool IsActiveComment { get; set; }
        [NotMapped]
        public List<PostComment> Comments { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public int LikesCount { get; set; }
        [NotMapped]
        public int CommentsCount { get; set; }
        //[NotMapped]
        //public FileData FileData { get; set; }

        [NotMapped]
        public List<FileData> Medias { get; set; }
    }

    public class PostDTO
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public int Status { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [NotMapped]
        public List<PostComment> Comments { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
        [NotMapped]
        public bool IsLikedByMe { get; set; }
        [NotMapped]
        public bool IsReportedByMe { get; set; }
        //[NotMapped]
        //public FileData FileData { get; set; }
        [NotMapped]
        public List<FileData> Medias { get; set; }
    }
}
