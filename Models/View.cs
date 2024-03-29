﻿using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;

namespace BanooClub.Models
{
    public class View : BaseEntity
    {
        public long ViewId { get; set; }
        public long ObjectId { get; set; }
        public ViewType Type { get; set; }
        public long Count { get; set; }
        public long? UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}