export default (context, resources) => ({
  async getMyFollowers() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getMyFollowings() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getMyFollowRequests() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async followRequestRespond(payload) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: payload,
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async deleteASingleFollower(followerUserId) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          followerUserId,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async deleteASingleFollowing(followingUserId) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          followingUserId,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async createAFollowRequest(followingUserId) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          followingUserId,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
