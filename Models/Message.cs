using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Message : BaseEntity
    {
        public long MessageId { get; set; }
        public string Subject { get; set; }
        public long UserId { get; set; }
        public string MessageBody { get; set; }
        public DateTime CreateDate { get; set; }
        public long? ParentMessageId { get; set; }
        public bool IsForwarded { get; set; }
        [NotMapped]
        public long? RecipientUserId { get; set;}
        [NotMapped]
        public long? RecipientGroupId { get; set; }

    }

    public class MessageDTO
    {
        public long? GroupId { get; set; }
        public long? UserId { get; set; }
        public string Subject { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserPhoto { get; set; }
        public long UnReadCount { get; set; }
    }
}
