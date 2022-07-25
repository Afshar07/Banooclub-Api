export default (context, resources) => ({
  async getAllUsersForUser(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAUserById(id) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          id,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async GetUserByUserName(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async searchUserByName(name) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          name,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAllUsers(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async updateUserDetails(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async deleteMedia(mediaName) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          mediaName,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async GetMyWishList() {
    try {
      const response = await context.$axios.post(resources);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
