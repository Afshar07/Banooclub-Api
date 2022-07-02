export default (context, resources) => ({
  async getAllUsersForUser(userId, search, count = 10) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          userId,
          search,
          count,
        },
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
  async getAllUsers(pageNumber = 1, count = 10) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          pageNumber,
          count,
        },
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
});
