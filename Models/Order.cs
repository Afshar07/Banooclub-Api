﻿using BanooClub.Models.Consulting;
using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanooClub.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
            ConsultantVideoConferanceUrls = new List<ConsultantVideoConferanceUrl>();
            ConsultantUserSchedules = new List<ConsultantUserSchedule>();
        }

        public long OrderId { get; set; }
        public bool IsPayed { get; set; }
        public string Description { get; set; }
        public long SumPrice { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderStatus Status { get; set; }
        public long? ServiceId { get; set; }
        public long? AdsId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public List<ConsultantVideoConferanceUrl> ConsultantVideoConferanceUrls { get; set; }
        public List<ConsultantUserSchedule> ConsultantUserSchedules { get; set; }
        [NotMapped]
        public List<OrderItem> SubOrders { get; set; }
        [NotMapped]
        public ServicePack ServiceInfo { get; set; }
        [NotMapped]
        public Ads AdsInfo { get; set; }
        [NotMapped]
        public Payment PaymentInfo { get; set; }
        [NotMapped]
        public User UserInfo { get; set; }
    }
}
