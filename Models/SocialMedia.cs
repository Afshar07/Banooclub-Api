using BanooClub.Models.Enums;
using System;

namespace BanooClub.Models
{
    public class SocialMedia : BaseEntity
    {
        public long MediaId { get; set; }
        public long ObjectId { get; set; }
        public string PictureUrl { get; set; }
        public MediaTypes Type { get; set; }
        public int? Priority { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    public class OutPutSaveImage
    {
        public string ImageName { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class FileData
    {
        public string Base64 { get; set; }
        public int? Priority { get; set; }
        public int UploadType { get; set; }
    }
}
