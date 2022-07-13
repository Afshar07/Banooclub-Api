import createAuthRepository from "../api/auth.js";
import createMediaRepository from "../api/media.js";
import createRoommateRepository from "../api/roommate.js";
import createPostRepository from "../api/post.js";
import createSocialRepository from "../api/social.js";
import createMessageRepository from "../api/message.js";
import createUserRepository from "../api/user.js";
import createCommonRepository from "../api/common.js";
import createAdsRepository from "../api/ads.js";
import createTicketRepository from "../api/ticket.js";
import createServicePackRepository from  "../api/service_pack";
import createServiceProperty from "../api/service_property";
import createServiceCategory from "../api/service_category"
import createForumPackRepository from "../api/forum";
import createServiceCommentRepository from "../api/service_comment";
import createServiceTagsRepository from "../api/service_tag"
import createForumCommentRepository from "../api/forum_comment";
import createRateRepository from "../api/rating"
import createWishListRepository from "../api/wish_list";
import createBirthdateRepository from "../api/birthdate_list";
import CreateTagsRepository from "../api/Tags";
import  CreateBlogRepository from '../api/Blog'


export default (context, inject) => {
  const repositories = {
    //   Authentication repositories start
    submitLogin: createAuthRepository(context, "Account/SignIn"),
    sendOtpToMobile: createAuthRepository(
      context,
      "Account/SendConfirmationCodeToMobile"
    ),
    sendOtpToEmail: createAuthRepository(
      context,
      "Account/SendConfirmationCodeToEmail"
    ),
    validateUsername: createAuthRepository(context, "Account/CheckUserName"),
    sendSignupRequest: createAuthRepository(context, "Account/SignUp"),
    forgetPassword: createAuthRepository(context, "Account/ForgotPassword"),

    //  Authentication repositories end

    // Media repositories start
    uploadVideo: createMediaRepository(context, "Users/UploadVideoGallery"),
    getAUserVideos: createMediaRepository(context, "Users/GetMyVideos"),
    getMyPhotos: createMediaRepository(context, "Users/GetMyPhotos"),
    uploadToPhotoGallery: createMediaRepository(
      context,
      "Users/UploadPhotoGallery"
    ),

    // Media repositories end

    // Roommate repositories start
    getRoommateByUserId: createRoommateRepository(
      context,
      "Roomate/GetByUserId"
    ),
    getAllRoommates: createRoommateRepository(context, "Roomate/GetAll"),
    getMyRoommate: createRoommateRepository(context, "Roomate/GetMyRoomate"),

    // Roommate repositories end

    // Post repositories start
    getFollowingPosts: createPostRepository(context, "Post/GetAll"),
    getPostsByUserId: createPostRepository(context, "Post/GetByUserId"),
    getMyPosts: createPostRepository(context, "Post/GetMyPost"),

    // Birthdate repositories start
    getBirthdateList: createBirthdateRepository(context, "Account/BirthDateList"),


    // Post repositories end

    // Social repositories start (Followers/Following, etc)
    getMyFollowers: createSocialRepository(context, "Follower/GetMyFollowers"),
    getMyFollowings: createSocialRepository(
      context,
      "Following/GetMyFollowings"
    ),
    getMyFollowRequests: createSocialRepository(
      context,
      "FollowRequest/GetByUserId"
    ),
    followRequestRespond: createSocialRepository(
      context,
      "FollowRequest/QuestRespond"
    ),
    deleteASingleFollower: createSocialRepository(
      context,
      "Follower/DeleteByFollowerUserId"
    ),
    deleteASingleFollowing: createSocialRepository(
      context,
      "Following/DeleteByFollowingUserId"
    ),
    createAFollowRequest: createSocialRepository(
      context,
      "FollowRequest/Create"
    ),

    // Social repositories end

    // Message repositories start
    getMenu: createMessageRepository(context, "Message/GetMenu"),
    deliverMessage: createMessageRepository(context, "Message/DeliverMessage"),
    isMessageReaded: createMessageRepository(context, "Message/ReadMessage"),
    sendMessage: createMessageRepository(context, "Message/SendMessage"),
    getConversation: createMessageRepository(
      context,
      "Message/GetConversation"
    ),

    // Message repositories end

    // User repositories start
    getAllUsersForUser: createUserRepository(
      context,
      "Users/GetAllUserforUser"
    ),
    getAUserById: createUserRepository(context, "Users/Get"),
    searchUserByName: createUserRepository(context, "Users/SearchByName"),
    getAllUsers: createUserRepository(context, "Users/GetAll"),
    updateUserDetails: createUserRepository(context, "Users/Update"),
    deleteMedia: createUserRepository(context, "Users/DeleteMedia"),

    // User repositories end

    // Common repositories start
    getUserIndex: createCommonRepository(context, "Common/GetUserIndex"),
    getUserByToken: createCommonRepository(context, "Common/GetIndexData"),

    // Common repositories end

    // Ads repository start
    getAllAdsCategory: createAdsRepository(context, "AdsCategory/GetAll"),
    GetAllStates: createAdsRepository(context, "State/GetStates"),
    GetAllCities: createAdsRepository(context, "City/GetCitiesByStateId"  ),
    createAnAd: createAdsRepository(context, "Ads/Create"),
    UpdateAd:createAdsRepository(context,'Ads/Update'),
    getAdsByUserId: createAdsRepository(context, "Ads/GetByUserId"),
    getAnAd: createAdsRepository(context, "Ads/Get"),
    changeAnAdStatus: createAdsRepository(context, "Ads/ChangeStatus"),
    getAllRejectedAds: createAdsRepository(context, "Ads/GetRejected"),
    getAllPendingAds: createAdsRepository(context, "Ads/GetNotConfirmed"),
    GetAllAds:createAdsRepository(context,'Ads/GetAdsByFilter'),
    CreateLike:createForumPackRepository(context,'Like/Create'),
    // Ads repository end

    // Ticket repository start
    getAllTickets: createTicketRepository(context, "Ticket/GetAllForCustomer"),
    createATicket: createTicketRepository(context, "Ticket/CreateTicket"),
    getAllTicketsByParentId: createTicketRepository(
      context,
      "Ticket/GetAllByParentId"
    ),

    // Ticket repository end

    getAllServices:createServicePackRepository(context,"ServicePack/GetAll"),
    getMyServices:createServicePackRepository(context,"ServicePack/GetMyServices"),
    createAService: createServicePackRepository(context, "ServicePack/Create"),
    getAService: createServicePackRepository(context,"ServicePack/Get"),
    updateAService: createServicePackRepository(context,"ServicePack/Update"),
    deleteAService: createServicePackRepository(context,"ServicePack/Delete"),

    // Service repository end


    getAllServicesCategory: createServiceCategory(context,"ServiceCategory/GetAll"),
    getAServiceCategory: createServiceCategory(context,"ServiceCategory/Get"),
    // Service category repository end


    createAServiceProperty:createServiceProperty(context,"ServiceProperty/Create"),
    updateAServiceProperty:createServiceProperty(context,"ServiceProperty/Update"),
    deleteAServiceProperty:createServiceProperty(context,"ServiceProperty/Delete"),
    // Service Property repository end

    createComment:createServiceCommentRepository(context,"ServiceComment/Create"),
    // Service Property repository end

    getAllForumCategory: createForumPackRepository(context,"ForumCategory/GetAll"),
    createForum:createForumPackRepository(context,'Forum/Create'),
    getAllForums:createForumPackRepository(context,'Forum/GetAll'),
    getMyForums:createForumPackRepository(context,"Forum/GetMyForums"),
    getAForum:createForumPackRepository(context,'Forum/Get'),
    DeleteForum:createForumPackRepository(context,'Forum/Delete'),
    UpdateForum:createForumPackRepository(context,'Forum/Update'),
    // Forum repository end

    // Tags Repository
    AddTag:CreateTagsRepository(context,'Tag/Create'),
    DeleteTag:CreateTagsRepository(context,'Tag/Delete'),
    createForumComment:createForumCommentRepository(context,"ForumComment/Create"),
    // Forum Property repository end

    createWishList:createWishListRepository(context,"WishList/Create"),
    deleteWishList:createWishListRepository(context,"WishList/Delete"),
    GetWishlist:createWishListRepository(context,'WishList/GetByUserId'),
    // Wish list repository end


    createRate:createRateRepository(context,"Rating/Create"),
    getARate:createRateRepository(context,"Rating/Get"),
    // Forum Property repository end

    deleteAServiceTag:createServiceTagsRepository(context,"Tag/Delete"),
    createAServiceTag:createServiceTagsRepository(context,"Tag/Create"),
    // Service Tags repository end

    //Blog Repo
    GetAllBlog:CreateBlogRepository(context,'Blog/GetAll')


  };
  inject("repositories", repositories);

};
