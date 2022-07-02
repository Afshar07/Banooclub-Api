export default (context, resources) => ({
  async getFollowingPosts() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getPostsByUserId(userId) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          userId,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getMyPosts(count = 10, pageNumber = 1) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          count,
          pageNumber,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
