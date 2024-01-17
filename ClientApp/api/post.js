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

  async ChangePostCommentActivation(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getPostsByUserId(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getPost(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
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
  async getPostsByUserName(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async deleteAPost(params) {
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
