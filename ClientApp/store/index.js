export default {
  state: () => {
    return {
      unreadTicketCount: null,
      user: {
        userId: null,
        name: "",
        familyName: "",
        mobile: null,
        role: "",
      },
      EditAdId: 0,
      MyPageData: "",
      SearchedUser: 0,
      SearchedUserStatus: 0,
      role: "",
      token: null,

      loggedInfo: {
        status: 0,
        logCounts: 0,
        unreadTicketCount: 0,
        logCountsDistinct: 0,
        bio: "",
        userName: "",
        selfie: "",
        banner: "",
        lastViewers: [],
        recentActivity: [],
        FollowBtn: false,
        followerCount: 0,
        followingCount: 0,
        name: "",
        familyName: "",
        type: 0,
        lawyerCertificateDoc: null,
        lawyerCertificateStatus: 0,
        newspaperDoc: null,
        newspaperStatus: 0,
        videoIdentity: null,
        videoIdentityStatus: 0,
        videoIdentityUpdateDate: null,
        activeRoomate: false,
        Private: false,
        IsPrivateActivity: false,
      },
      HeaderData: {
        Private: false,
        status: 0,
        logCounts: 0,
        unreadTicketCount: 0,
        logCountsDistinct: 0,
        bio: "",
        userName: "",
        selfie: "",
        banner: "",
        lastViewers: [],
        recentActivity: [],
        FollowBtn: false,
        followerCount: 0,
        followingCount: 0,
        name: "",
        familyName: "",
        type: 0,
        lawyerCertificateDoc: null,
        lawyerCertificateStatus: 0,
        newspaperDoc: null,
        newspaperStatus: 0,
        videoIdentity: null,
        videoIdentityStatus: 0,
        videoIdentityUpdateDate: null,
        activeRoomate: false,
      },
      SocketUserId: 0,
    };
  },
  getters: {
    user(state) {
      return state.user;
    },
    SocketId(state) {
      return state.SocketUserId;
    },
  },
  mutations: {
    ChangeEditAdId(state, id) {
      state.EditAdId = id;
    },
    SetDefaultSocketId(state) {
      state.SocketUserId = 0;
    },
    sendMessage(state, resultId) {
      state.SocketUserId = resultId;
    },

    GetElement(eleee) {
      console.log(eleee);
    },

    SetSearchedUserStatus(state, status) {
      state.SearchedUserStatus = status;
    },
    SetUserMypageInfo(state, data) {
      state.MyPageData = data;
    },
    SetUserData(state, data) {
      if (data.status === 1 || data.status === 4) {
        state.HeaderData.status = data.status;
        state.HeaderData.logCounts = data.logCounts;
        state.HeaderData.logCountsDistinct = data.logCountsDistinct;
        state.HeaderData.unreadTicketCount = data.unreadTicketCount;
        state.HeaderData.bio = data.userInfo.userSetting.bio;
        state.HeaderData.userName = data.baseData.userName;
        state.HeaderData.selfie = data.baseData.selfie;
        state.HeaderData.banner = data.baseData.banner;
        state.HeaderData.followerCount = data.baseData.followersCount;
        state.HeaderData.followingCount = data.baseData.followingsCount;
        state.HeaderData.name = data.baseData.name;
        state.HeaderData.familyName = data.baseData.familyName;
        state.HeaderData.type = data.baseData.userType;
        state.HeaderData.lastViewers = data.last8Viewer;
        state.HeaderData.activeRoomate = data.baseData.activeRoomate;
        state.HeaderData.recentActivity = data.recentActivity;
        state.HeaderData.FollowBtn = data.followButton;
      }
      else if (data.status === 2) {
        state.HeaderData.FollowBtn = data.followButton;
        state.HeaderData.status = data.status;
        state.HeaderData.userName = data.baseData.userName;
        state.HeaderData.selfie = data.baseData.selfie;
        state.HeaderData.banner = data.baseData.banner;
        state.HeaderData.followerCount = data.baseData.followersCount;
        state.HeaderData.followingCount = data.baseData.followingsCount;
        state.HeaderData.name = data.baseData.name;
        state.HeaderData.familyName = data.baseData.familyName;
        state.HeaderData.type = data.baseData.userType;
        state.HeaderData.activeRoomate = data.baseData.activeRoomate;
      } else if (data.status === 3) {
        this.$toast.error("برای مشاهده پروفایل کاربر وارد اکانت خود شوید");
      }
      else if (!data && !data.status) {
        state.loggedInfo.status = 0;
        state.loggedInfo.logCounts = data.logCounts;
        state.loggedInfo.logCountsDistinct = data.logCountsDistinct;
        state.loggedInfo.unreadTicketCount = data.unreadTicketCount;
        state.loggedInfo.bio = data.userInfo.userSetting.bio;
        state.loggedInfo.userName = data.baseData.userName;
        state.loggedInfo.selfie = data.baseData.selfie;
        state.loggedInfo.banner = data.baseData.banner;
        state.loggedInfo.followerCount = data.baseData.followersCount;
        state.loggedInfo.followingCount = data.baseData.followingsCount;
        state.loggedInfo.name = data.baseData.name;
        state.loggedInfo.familyName = data.baseData.familyName;
        state.loggedInfo.lastViewers = data.last8Viewer;
        state.loggedInfo.recentActivity = data.recentActivity;
        state.loggedInfo.FollowBtn = data.followButton;
        state.loggedInfo.lawyerCertificateDoc =
          data.userInfo.userSetting.lawyerCertificateDoc;
        state.loggedInfo.lawyerCertificateStatus =
          data.userInfo.userSetting.lawyerCertificateStatus;
        state.loggedInfo.newspaperDoc = data.userInfo.userSetting.newspaperDoc;
        state.loggedInfo.newspaperStatus =
          data.userInfo.userSetting.newspaperStatus;
        state.loggedInfo.videoIdentity =
          data.userInfo.userSetting.videoIdentity;
        state.loggedInfo.videoIdentityStatus =
          data.userInfo.userSetting.videoIdentityStatus;
        state.loggedInfo.videoIdentityUpdateDate =
          data.userInfo.userSetting.videoIdentityUpdateDate;
        state.loggedInfo.Private = data.userInfo?.userSetting?.isPrivateActivity;
        state.loggedInfo.type = data.baseData.userType;
        state.loggedInfo.activeRoomate = data.baseData.activeRoomate;
        state.loggedInfo.IsPrivateActivity =
          data.userInfo.userSetting.isPrivateActivity;
        state.HeaderData.Private = data.userInfo.userSetting.isPrivateActivity;
        state.HeaderData.status = 0;
        state.HeaderData.activeRoomate = data.baseData.activeRoomate;
        state.HeaderData.logCounts = data.logCounts;
        state.HeaderData.logCountsDistinct = data.logCountsDistinct;
        state.HeaderData.unreadTicketCount = data.unreadTicketCount;
        state.HeaderData.bio = data.userInfo.userSetting.bio;
        state.HeaderData.userName = data.baseData.userName;
        state.HeaderData.selfie = data.baseData.selfie;
        state.HeaderData.banner = data.baseData.banner;
        state.HeaderData.followerCount = data.baseData.followersCount;
        state.HeaderData.followingCount = data.baseData.followingsCount;
        state.HeaderData.name = data.baseData.name;
        state.HeaderData.familyName = data.baseData.familyName;
        state.HeaderData.lastViewers = data.last8Viewer;
        state.HeaderData.recentActivity = data.recentActivity;
        state.HeaderData.FollowBtn = data.followButton;
        state.HeaderData.lawyerCertificateDoc =
          data.userInfo.userSetting.lawyerCertificateDoc;
        state.HeaderData.lawyerCertificateStatus =
          data.userInfo.userSetting.lawyerCertificateStatus;
        state.HeaderData.newspaperDoc = data.userInfo.userSetting.newspaperDoc;
        state.HeaderData.newspaperStatus =
          data.userInfo.userSetting.newspaperStatus;
        state.HeaderData.videoIdentity =
          data.userInfo.userSetting.videoIdentity;
        state.HeaderData.videoIdentityStatus =
          data.userInfo.userSetting.videoIdentityStatus;
        state.HeaderData.videoIdentityUpdateDate =
          data.userInfo.userSetting.videoIdentityUpdateDate;
        state.HeaderData.type = data.baseData.userType;
      }
    },

    SetSearchedUserId(state, userId) {
      state.SearchedUser = userId;
    },
    setUser(state, { user }) {
      state.user.userId = user.userId;
      state.user.name = user.name;
      state.user.familyName = user.familyName;
      state.user.mobile = user.mobile;
      state.role = user.role;
    },
    setAuthentication(state, payload) {
      localStorage.setItem("token", payload.payload.token);
      localStorage.setItem("role", payload.payload.user.role);
      state.token = payload.payload.token;
      state.user = payload.payload.user;
    },

    logout(state) {
      state.token = "";
      localStorage.setItem("token", "");
      localStorage.setItem("role", "");
      state.role = "";
      state.user.userId = null;
      state.user.name = "";
      state.user.familyName = "";
      state.user.mobile = null;
    },
    setIsPrivate(state, payload) {
      state.isPrivate = payload;
    },
    setIsPrivateRoomate(state, payload) {
      state.isPrivateRoomate = payload;
    },
    setIsPrivateAds(state, payload) {
      state.isPrivateAds = payload;
    },
    setIsPrivateSocial(state, payload) {
      state.isPrivateSocial = payload;
    },

    setUnreadTicketCount(state, payload) {
      state.unreadTicketCount = payload.payload;
    },
  },
  actions: {
    login(context, payload) {
      context.commit("setAuthentication", payload);
    },
    logout(context) {
      context.commit("logout");
    },
    setIsPrivate(context, payload) {
      context.commit("setIsPrivate", payload);
    },
    setIsPrivateRoomate(context, payload) {
      context.commit("setIsPrivateRoomate", payload);
    },
    setIsPrivateAds(context, payload) {
      context.commit("setIsPrivateAds", payload);
    },
    setIsPrivateSocial(context, payload) {
      context.commit("setIsPrivateSocial", payload);
    },
  },
};
