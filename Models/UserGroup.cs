﻿using System;

namespace BanooClub.Models
{
    public class UserGroup : BaseEntity
    {
        public long UserGroupId { get; set; }
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
