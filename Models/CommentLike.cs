using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Models
{
    public class CommentLike:BaseEntity
    {
        public long CommentLikeId { get; set; }
        public CommentType Type { get; set; }
        public long ObjectId { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
