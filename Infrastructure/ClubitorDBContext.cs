using BanooClub.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
    public class BanooClubDBContext : DbContext
    {
        public BanooClubDBContext()
        {
        }

        public DbSet<Ads> Adses { get; set; }
        public DbSet<AdsCategory> AdsCategories { get; set; }
        public DbSet<AdsChat> AdsChats { get; set; }
        public DbSet<AdsComment> AdsComments { get; set; }
        public DbSet<AdsPayment> AdsPayments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ConsultComment> ConsultComments { get; set; }
        public DbSet<ConsultSchedule> ConsultSchedules { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Formal> Formals { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendshipComment> FriendshipComments { get; set; }
        public DbSet<FriendshipPostComment> FriendshipPostComments { get; set; }
        public DbSet<FriendshipPost> FrienshipPosts { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<RequestConsult> RequestConsults { get; set; }
        public DbSet<Roomate> Roomates { get; set; }
        public DbSet<ServiceConsult> ServiceConsults { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<SystemPayment> SystemPayments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserType> Types { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<AccountSetting> AccountSettings { get; set; }
        public DbSet<Object> Objects { get; set; }
        public DbSet<PostDTO> PostDTOs { get; set; }
        public DbSet<FollowRequest> FollowRequests { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<PostNK> PostNKs { get; set; }
        public DbSet<ChatNK> ChatNKs { get; set; }
        public DbSet<PostReport> PostReports { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ViewHistory> ViewHistories { get; set; }
        public DbSet<RoomateDoc> RoomateDocs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageRecipient> MessageRecipients { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CNTRY> CNTRIES { get; set; }

        public DbSet<ServicePack> ServicePacks { get; set; }
        public DbSet<ServiceComment> ServiceComments { get; set; }
        public DbSet<ServiceProperty> ServiceProperties { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<ForumCategory> ForumCategories { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Wallet> Wallets { get; set; }


        public BanooClubDBContext(DbContextOptions<BanooClubDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Ads>().ToTable("Ads", "Service").HasKey(z => z.AdsId);
            modelbuilder.Entity<AdsCategory>().ToTable("AdsCategories", "Service").HasKey(z => z.AdsCategoryId);
            modelbuilder.Entity<AdsChat>().ToTable("AdsChats", "Service").HasKey(z => z.AdsChatId);
            modelbuilder.Entity<AdsComment>().ToTable("AdsComments", "Service").HasKey(z => z.AdsCommentId);
            modelbuilder.Entity<AdsPayment>().ToTable("AdsPayments", "Service").HasKey(z => z.AdsPaymentId);
            modelbuilder.Entity<Chat>().ToTable("Chats", "Service").HasKey(z => z.ChatId);
            modelbuilder.Entity<ConsultComment>().ToTable("ConsultComments", "Service").HasKey(z => z.ConsultCommentId);
            modelbuilder.Entity<ConsultSchedule>().ToTable("ConsultSchedules", "Service").HasKey(z => z.ConsultScheduleId);
            modelbuilder.Entity<Faq>().ToTable("Faqs", "Service").HasKey(z => z.FaqId);
            modelbuilder.Entity<Formal>().ToTable("Formals", "User").HasKey(z => z.FormalId);
            modelbuilder.Entity<Friendship>().ToTable("Friendships", "Service").HasKey(z => z.FriendshipId);
            modelbuilder.Entity<FriendshipComment>().ToTable("FriendshipComments", "Service").HasKey(z => z.FriendshipCommentId);
            modelbuilder.Entity<FriendshipPostComment>().ToTable("FriendshipPostComments", "Service").HasKey(z => z.FriendshipPostCommentId);
            modelbuilder.Entity<FriendshipPost>().ToTable("FriendshipPosts", "Service").HasKey(z => z.FriendshipPostId);
            modelbuilder.Entity<Media>().ToTable("Medias", "Service").HasKey(z => z.MediaId);
            modelbuilder.Entity<Package>().ToTable("Packages", "System").HasKey(z => z.PackageId);
            modelbuilder.Entity<Payment>().ToTable("Payments", "Payment").HasKey(z => z.PaymentId);
            modelbuilder.Entity<Report>().ToTable("Reports", "Service").HasKey(z => z.ReportId);
            modelbuilder.Entity<RequestConsult>().ToTable("Consults", "Request").HasKey(z => z.RequestConsultId);
            modelbuilder.Entity<Roomate>().ToTable("Roomates", "Service").HasKey(z => z.RoomateId);
            modelbuilder.Entity<ServiceConsult>().ToTable("Consults", "Service").HasKey(z => z.ConsultId);
            modelbuilder.Entity<ServiceType>().ToTable("Types", "Service").HasKey(z => z.TypeId);
            modelbuilder.Entity<SystemPayment>().ToTable("Payments", "System").HasKey(z => z.SystemPaymentId);
            modelbuilder.Entity<User>().ToTable("Users", "User").HasKey(z => z.UserId);
            modelbuilder.Entity<UserComment>().ToTable("UserComments", "Service").HasKey(z => z.UserCommentId);
            modelbuilder.Entity<UserType>().ToTable("Types", "User").HasKey(z => z.UserTypeId);
            modelbuilder.Entity<Post>().ToTable("Posts", "Social").HasKey(z => z.PostId);
            modelbuilder.Entity<PostComment>().ToTable("PostComments", "Social").HasKey(z => z.PostCommentId);
            modelbuilder.Entity<Follower>().ToTable("Followers", "Social").HasKey(z => z.FollowerId);
            modelbuilder.Entity<Following>().ToTable("Followings", "Social").HasKey(z => z.FollowingId);
            modelbuilder.Entity<PostLike>().ToTable("PostLikes", "Social").HasKey(z => z.PostLikeId);
            modelbuilder.Entity<Review>().ToTable("Reviews", "Social").HasKey(z => z.ReviewId);
            modelbuilder.Entity<Activity>().ToTable("Activities", "Social").HasKey(z => z.ActivityId);
            modelbuilder.Entity<SocialMedia>().ToTable("Medias", "Social").HasKey(z => z.MediaId);
            modelbuilder.Entity<AccountSetting>().ToTable("AccountSettings", "Social").HasKey(z => z.AccountSettingId);
            modelbuilder.Entity<FollowRequest>().ToTable("FollowRequests", "Social").HasKey(z => z.FollowRequestId);
            modelbuilder.Entity<WishList>().ToTable("WishLists", "Common").HasKey(z => z.WishListId);
            modelbuilder.Entity<PostNK>().ToTable("PostNKs", "Common").HasKey(z => z.PostNkId);
            modelbuilder.Entity<ChatNK>().ToTable("ChatNKs", "Common").HasKey(z => z.ChatNKId);
            modelbuilder.Entity<PostReport>().ToTable("PostReports", "Common").HasKey(z => z.PostReportId);
            modelbuilder.Entity<UserSetting>().ToTable("UserSettings", "User").HasKey(z => z.UserSettingId);
            modelbuilder.Entity<Log>().ToTable("Logs", "Common").HasKey(z => z.LogId);
            modelbuilder.Entity<Country>().ToTable("Countries", "dbo").HasKey(z => z.Id);
            modelbuilder.Entity<ViewHistory>().ToTable("ViewHistories", "Social").HasKey(z => z.ViewHistoryId);
            modelbuilder.Entity<RoomateDoc>().ToTable("RoomateDocs", "Social").HasKey(z => z.RoomateDocId);
            modelbuilder.Entity<Group>().ToTable("Groups", "Social").HasKey(z => z.GroupId);
            modelbuilder.Entity<Ticket>().ToTable("Tickets", "Common").HasKey(z => z.TicketId);
            modelbuilder.Entity<UserGroup>().ToTable("UserGroups", "Social").HasKey(z => z.UserGroupId);
            modelbuilder.Entity<Message>().ToTable("Messages", "Social").HasKey(z => z.MessageId);
            modelbuilder.Entity<MessageRecipient>().ToTable("MessageRecipients", "Social").HasKey(z => z.MessageRecipientId);
            modelbuilder.Entity<City>().ToTable("Cities", "dbo").HasKey(z => z.CityId);
            modelbuilder.Entity<CNTRY>().ToTable("CNTRIES", "dbo").HasKey(z => z.CountryId);
            modelbuilder.Entity<Object>().HasNoKey();
            modelbuilder.Entity<PostDTO>().HasNoKey();

            modelbuilder.Entity<ServicePack>().ToTable("ServicePacks", "Service").HasKey(z => z.ServicePackId);
            modelbuilder.Entity<ServiceComment>().ToTable("ServiceComments", "Service").HasKey(z => z.ServiceCommentId);
            modelbuilder.Entity<ServiceProperty>().ToTable("ServiceProperties", "Service").HasKey(z => z.ServicePropertyId);
            modelbuilder.Entity<ServiceCategory>().ToTable("ServiceCategories", "Service").HasKey(z => z.ServiceCategoryId);
            modelbuilder.Entity<Forum>().ToTable("Forums", "Social").HasKey(z => z.ForumId);
            modelbuilder.Entity<ForumCategory>().ToTable("ForumCategories", "Social").HasKey(z => z.ForumCategoryId);
            modelbuilder.Entity<ForumComment>().ToTable("ForumComments", "Social").HasKey(z => z.ForumCommentId);
            modelbuilder.Entity<Rating>().ToTable("Ratings", "Common").HasKey(z => z.RatingId);
            modelbuilder.Entity<View>().ToTable("Views", "Common").HasKey(z => z.ViewId);
            modelbuilder.Entity<Tag>().ToTable("Tags", "Common").HasKey(z => z.TagId);
            modelbuilder.Entity<Wallet>().ToTable("Wallets", "Common").HasKey(z => z.WalletId);
            modelbuilder.Entity<CommentLike>().ToTable("CommentLike", "Common").HasKey(z => z.CommentLikeId);


            modelbuilder.Entity<Post>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<PostComment>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<PostLike>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Follower>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Following>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Review>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Activity>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Activity>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            //modelbuilder.Entity<Ads>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<WishList>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Log>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<PostReport>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<UserGroup>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Group>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Message>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<Ticket>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<CommentLike>().Property(b => b.CreateDate).HasDefaultValueSql("getdate()");
            modelbuilder.Entity<SocialMedia>().Property(b => b.UpdateDate).HasDefaultValueSql("getdate()");
            
        }
    }
}


