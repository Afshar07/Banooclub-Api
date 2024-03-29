﻿using BanooClub.Models.Consulting;
using BanooClub.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace BanooClub.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Consultants = new List<Consultant>();
            Orders = new List<Order>();
            ConsultantUserSchedules = new List<ConsultantUserSchedule>();
            Activities = new List<Activity>();
        }

        public long UserId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public long Type { get; set; }
        public int Status { get; set; }
        public long? FormalId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public long? ServiceCategoryId { get; set; }
        public string UserCode { get; set; }
        public string IntroducerCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public RelationStatus? RelationState { get; set; }
        public string Avatar { get; set; }
        public string DefaultBanner { get; set; }
        public bool UseDefaultAvatar { get; set; }
        public bool UseDefaultBanner { get; set; }

        [NotMapped]
        public int FollowersCount { get; set; }
        [NotMapped]
        public int FollowingsCount { get; set; }
        [NotMapped]
        public string BannerFileData { get; set; }
        [NotMapped]
        public string SelfieFileData { get; set; }
        [NotMapped]
        public string CurrentPassword { get; set; }
        [NotMapped]
        public UserSetting UserSetting { get; set; }
        [NotMapped]
        public  bool? IsFollowing { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        [NotMapped]
        public string StateName { get; set; }

        public List<Consultant> Consultants { get; set; }
        public List<Order> Orders { get; set; }
        public List<ConsultantUserSchedule> ConsultantUserSchedules { get; set; }
        public List<Activity> Activities { get; set; }
    }
    public class UserSignDTO
    {
        public string Mobile { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string VerifyCode { get; set; }
        public string EncryptedMail { get; set; }
        public long Type { get; set; }
        public string UserName { get; set; }
        public long UserRole { get; set; }
        public long? ServiceCategoryId { get; set; }
        public string IntroducerCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public RelationStatus? RelationState { get; set; }
        public string? Avatar { get; set; }
        public string? Banner { get; set; }
    }
    public class UserAndUserSetting
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public long Type { get; set; }
        public int Status { get; set; }
        public long FormalId { get; set; }
        public string Bio { get; set; }
        public string BirthDate { get; set; }
        public int Gender { get; set; }
        public string UserTag { get; set; }
        public string Flag { get; set; }
        public bool IsPrivate { get; set; }

    }
    public class UserBaseData
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Banner { get; set; }
        public string Selfie { get; set; }
        public string UserName { get; set; }
    }
}
