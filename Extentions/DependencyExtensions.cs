﻿using BanooClub.Models;
using BanooClub.Services.AccountServices;
using BanooClub.Services.AccountSettingServices;
using BanooClub.Services.ActivityServices;
using BanooClub.Services.AdsCategoryServices;
using BanooClub.Services.AdsChatServices;
using BanooClub.Services.AdsCommentServices;
using BanooClub.Services.AdsPaymentServices;
using BanooClub.Services.AdsServices;
using BanooClub.Services.ChatNKServices;
using BanooClub.Services.ChatServices;
using BanooClub.Services.CityServices;
using BanooClub.Services.CNTRYServices;
using BanooClub.Services.Common;
using BanooClub.Services.CommonServices;
using BanooClub.Services.ConcultCommentServices;
using BanooClub.Services.ConsultScheduleServices;
using BanooClub.Services.CryptographyServices;
using BanooClub.Services.DateTimeServices;
using BanooClub.Services.FaqServices;
using BanooClub.Services.FollowerServices;
using BanooClub.Services.FollowingServices;
using BanooClub.Services.FollowRequestServices;
using BanooClub.Services.FormalServices;
using BanooClub.Services.FriendshipCommentServices;
using BanooClub.Services.FriendshipPostCommentServices;
using BanooClub.Services.FriendshipPostServices;
using BanooClub.Services.FriendshipServices;
using BanooClub.Services.GroupServices;
using BanooClub.Services.JWTServices;
using BanooClub.Services.LogServices;
using BanooClub.Services.MediaServices;
using BanooClub.Services.MessageRecipientServices;
using BanooClub.Services.MessageServices;
using BanooClub.Services.PackageServices;
using BanooClub.Services.PaymentServices;
using BanooClub.Services.PostCommentServices;
using BanooClub.Services.PostLikeServices;
using BanooClub.Services.PostNKServices;
using BanooClub.Services.PostReportServices;
using BanooClub.Services.PostServices;
using BanooClub.Services.ReportServices;
using BanooClub.Services.RequestConsultServices;
using BanooClub.Services.ReviewServices;
using BanooClub.Services.RoomateDocServices;
using BanooClub.Services.RoomateServices;
using BanooClub.Services.ServiceConsultServices;
using BanooClub.Services.ServiceTypeServices;
using BanooClub.Services.SmsServices;
using BanooClub.Services.SocialMediaServices;
using BanooClub.Services.SystemPaymentServices;
using BanooClub.Services.TicketServices;
using BanooClub.Services.UserCommentServices;
using BanooClub.Services.UserGroupServices;
using BanooClub.Services.UserServices;
using BanooClub.Services.UserSettingServices;
using BanooClub.Services.UserTypeServices;
using BanooClub.Services.ViewHistoryServices;
using BanooClub.Services.WishListServices;
using BanooClub.Settings;
using Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using BanooClub.Services.ServicePackServices;
using BanooClub.Services.ServiceCommentServices;
using BanooClub.Services.ServicePropertyServices;
using BanooClub.Services.ServiceCategoryServices;
using BanooClub.Services.ForumServices;
using BanooClub.Services.ForumCategoryServices;
using BanooClub.Services.ForumCommentServices;
using BanooClub.Services.RatingServices;
using BanooClub.Services.ViewServices;
using BanooClub.Services.TagServices;
using BanooClub.Services.WalletServices;
using BanooClub.Services.CommentLikeServices;
using BanooClub.Services.StateServices;

namespace BanooClub.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddDependency(this IServiceCollection services)
        {
            AddRepositories(services);
            AddServices(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IBanooClubEFRepository<User>, BanooClubEfRepository<User>>();
            services.AddScoped<IBanooClubEFRepository<Formal>, BanooClubEfRepository<Formal>>();
            services.AddScoped<IBanooClubEFRepository<Ads>, BanooClubEfRepository<Ads>>();
            services.AddScoped<IBanooClubEFRepository<AdsCategory>, BanooClubEfRepository<AdsCategory>>();
            services.AddScoped<IBanooClubEFRepository<AdsChat>, BanooClubEfRepository<AdsChat>>();
            services.AddScoped<IBanooClubEFRepository<AdsComment>, BanooClubEfRepository<AdsComment>>();
            services.AddScoped<IBanooClubEFRepository<AdsPayment>, BanooClubEfRepository<AdsPayment>>();
            services.AddScoped<IBanooClubEFRepository<Chat>, BanooClubEfRepository<Chat>>();
            services.AddScoped<IBanooClubEFRepository<ConsultComment>, BanooClubEfRepository<ConsultComment>>();
            services.AddScoped<IBanooClubEFRepository<ConsultSchedule>, BanooClubEfRepository<ConsultSchedule>>();
            services.AddScoped<IBanooClubEFRepository<Faq>, BanooClubEfRepository<Faq>>();
            services.AddScoped<IBanooClubEFRepository<Friendship>, BanooClubEfRepository<Friendship>>();
            services.AddScoped<IBanooClubEFRepository<FriendshipPostComment>, BanooClubEfRepository<FriendshipPostComment>>();
            services.AddScoped<IBanooClubEFRepository<FriendshipComment>, BanooClubEfRepository<FriendshipComment>>();
            services.AddScoped<IBanooClubEFRepository<FriendshipPost>, BanooClubEfRepository<FriendshipPost>>();
            services.AddScoped<IBanooClubEFRepository<Media>, BanooClubEfRepository<Media>>();
            services.AddScoped<IBanooClubEFRepository<Package>, BanooClubEfRepository<Package>>();
            services.AddScoped<IBanooClubEFRepository<Payment>, BanooClubEfRepository<Payment>>();
            services.AddScoped<IBanooClubEFRepository<Report>, BanooClubEfRepository<Report>>();
            services.AddScoped<IBanooClubEFRepository<RequestConsult>, BanooClubEfRepository<RequestConsult>>();
            services.AddScoped<IBanooClubEFRepository<Roomate>, BanooClubEfRepository<Roomate>>();
            services.AddScoped<IBanooClubEFRepository<ServiceConsult>, BanooClubEfRepository<ServiceConsult>>();
            services.AddScoped<IBanooClubEFRepository<ServiceType>, BanooClubEfRepository<ServiceType>>();
            services.AddScoped<IBanooClubEFRepository<SystemPayment>, BanooClubEfRepository<SystemPayment>>();
            services.AddScoped<IBanooClubEFRepository<UserComment>, BanooClubEfRepository<UserComment>>();
            services.AddTransient<IBanooClubEFRepository<UserType>, BanooClubEfRepository<UserType>>();
            services.AddScoped<IBanooClubEFRepository<Post>, BanooClubEfRepository<Post>>();
            services.AddScoped<IBanooClubEFRepository<PostComment>, BanooClubEfRepository<PostComment>>();
            services.AddScoped<IBanooClubEFRepository<Follower>, BanooClubEfRepository<Follower>>();
            services.AddScoped<IBanooClubEFRepository<Following>, BanooClubEfRepository<Following>>();
            services.AddScoped<IBanooClubEFRepository<PostLike>, BanooClubEfRepository<PostLike>>();
            services.AddScoped<IBanooClubEFRepository<Review>, BanooClubEfRepository<Review>>();
            services.AddScoped<IBanooClubEFRepository<Activity>, BanooClubEfRepository<Activity>>();
            services.AddTransient<IBanooClubEFRepository<SocialMedia>, BanooClubEfRepository<SocialMedia>>();
            services.AddScoped<IBanooClubEFRepository<AccountSetting>, BanooClubEfRepository<AccountSetting>>();
            services.AddScoped<IBanooClubEFRepository<FollowRequest>, BanooClubEfRepository<FollowRequest>>();
            services.AddScoped<IBanooClubEFRepository<WishList>, BanooClubEfRepository<WishList>>();
            services.AddScoped<IBanooClubEFRepository<ChatNK>, BanooClubEfRepository<ChatNK>>();
            services.AddScoped<IBanooClubEFRepository<PostNK>, BanooClubEfRepository<PostNK>>();
            services.AddScoped<IBanooClubEFRepository<PostReport>, BanooClubEfRepository<PostReport>>();
            services.AddScoped<IBanooClubEFRepository<UserSetting>, BanooClubEfRepository<UserSetting>>();
            services.AddScoped<IBanooClubEFRepository<Log>, BanooClubEfRepository<Log>>();
            services.AddScoped<IBanooClubEFRepository<Country>, BanooClubEfRepository<Country>>();
            services.AddScoped<IBanooClubEFRepository<ViewHistory>, BanooClubEfRepository<ViewHistory>>();
            services.AddScoped<IBanooClubEFRepository<RoomateDoc>, BanooClubEfRepository<RoomateDoc>>();
            services.AddScoped<IBanooClubEFRepository<Group>, BanooClubEfRepository<Group>>();
            services.AddScoped<IBanooClubEFRepository<UserGroup>, BanooClubEfRepository<UserGroup>>();
            services.AddScoped<IBanooClubEFRepository<Message>, BanooClubEfRepository<Message>>();
            services.AddScoped<IBanooClubEFRepository<MessageRecipient>, BanooClubEfRepository<MessageRecipient>>();
            services.AddScoped<IBanooClubEFRepository<City>, BanooClubEfRepository<City>>();
            services.AddScoped<IBanooClubEFRepository<CNTRY>, BanooClubEfRepository<CNTRY>>();
            services.AddTransient<IBanooClubEFRepository<Ticket>, BanooClubEfRepository<Ticket>>();
            services.AddTransient<IBanooClubEFRepository<State>, BanooClubEfRepository<State>>();

            services.AddScoped<IBanooClubEFRepository<ServicePack>,BanooClubEfRepository<ServicePack>>();
            services.AddScoped<IBanooClubEFRepository<ServiceComment>,BanooClubEfRepository<ServiceComment>>();
            services.AddScoped<IBanooClubEFRepository<ServiceProperty>,BanooClubEfRepository<ServiceProperty>>();
            services.AddScoped<IBanooClubEFRepository<ServiceCategory>,BanooClubEfRepository<ServiceCategory>>();
            services.AddScoped<IBanooClubEFRepository<Forum>,BanooClubEfRepository<Forum>>();
            services.AddScoped<IBanooClubEFRepository<ForumCategory>,BanooClubEfRepository<ForumCategory>>();
            services.AddScoped<IBanooClubEFRepository<ForumComment>,BanooClubEfRepository<ForumComment>>();
            services.AddScoped<IBanooClubEFRepository<Rating>,BanooClubEfRepository<Rating>>();
            services.AddScoped<IBanooClubEFRepository<View>,BanooClubEfRepository<View>>();
            services.AddScoped<IBanooClubEFRepository<Tag>,BanooClubEfRepository<Tag>>();
            services.AddScoped<IBanooClubEFRepository<Wallet>,BanooClubEfRepository<Wallet>>();
            services.AddScoped<IBanooClubEFRepository<CommentLike>, BanooClubEfRepository<CommentLike>>();
            

        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IFormalService, FormalService>();
            services.AddScoped<IAdsCategoryService, AdsCategoryService>();
            services.AddScoped<IAdsChatService, AdsChatService>();
            services.AddScoped<IAdsService, AdsService>();
            services.AddScoped<IAdsCommentService, AdsCommentService>();
            services.AddScoped<IAdsPaymentService, AdsPaymentService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IConsultCommentService, ConsultCommentService>();
            services.AddScoped<IConsultScheduleService, ConsultScheduleService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IFriendshipCommentService, FriendshipCommentService>();
            services.AddScoped<IFriendshipPostCommentService, FriendshipPostCommentService>();
            services.AddScoped<IFriendshipPostService, FriendShipPostService>();
            services.AddScoped<IFriendshipService, FriendshipService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IRequestConsultService, RequestConsultService>();
            services.AddScoped<IRoomateService, RoomateService>();
            services.AddScoped<IServiceConsultService, ServiceConsultService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            services.AddScoped<ISystemPaymentService, SystemPaymentService>();
            services.AddScoped<IUserCommentService, UserCommentService>();
            services.AddTransient<IUserTypeService, UserTypeService>();
            services.AddTransient<IAccountManagerService, AccountManagerService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostCommentService, PostCommentService>();
            services.AddTransient<IFollowerService, FollowerService>();
            services.AddTransient<IFollowingService, FollowingService>();
            services.AddTransient<ISocialMediaService, SocialMediaService>();
            services.AddTransient<IPostLikeService, PostLikeService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<IAccountSettingService, AccountSettingService>();
            services.AddTransient<IFollowRequestService, FollowRequestService>();
            services.AddTransient<IWishListService, WishListService>();
            services.AddTransient<IChatNKService, ChatNKService>();
            services.AddTransient<IPostNKService, PostNKService>();
            services.AddScoped<IUserSettingService, UserSettingService>();
            services.AddTransient<IPostReportService, PostReportService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IViewHistoryService, ViewHistoryService>();
            services.AddTransient<IRoomateDocService, RoomateDocService>();
            services.AddTransient<IUserGroupService, UserGroupService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<Hub<IMessageService>, MessageService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IMessageRecipientService, MessageRecipientService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICNTRYService, CNTRYService>();
            services.AddTransient<IStateService, StateService>();

            services.AddTransient<IServicePackService, ServicePackService>();
            services.AddTransient<IServiceCommentService, ServiceCommentService>();
            services.AddTransient<IServicePropertyService, ServicePropertyService>();
            services.AddTransient<IServiceCategoryService, ServiceCategoryService>();
            services.AddTransient<IForumService, ForumService>();
            services.AddTransient<IForumCategoryService, ForumCategoryService>();
            services.AddTransient<IForumCommentService, ForumCommentService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IViewService, ViewService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<ICommentLikeService, CommentLikeService>();


            services.AddTransient(typeof(IGenerateJwtService), typeof(GenerateJwtService));
            services.AddTransient(typeof(IConfirmationCodeSetting), typeof(ConfirmationCodeSetting));
            services.AddSingleton(typeof(IDateTime), typeof(Services.DateTimeServices.DateTime));
            services.AddTransient(typeof(IEncryptService), typeof(EncryptService));
            services.AddTransient(typeof(IDecryptService), typeof(DecryptService));
            services.AddTransient(typeof(ISmsSenderService), typeof(SmsSenderService));
        }
    }
}
