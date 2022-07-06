export default (context, resources) => ({
  async getFollowingPosts(params) {
    try {
      const response = await context.$axios.post(resources, null,{
        params
      });
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
  async getMyPosts(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
