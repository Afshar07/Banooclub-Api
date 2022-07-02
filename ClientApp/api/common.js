export default (context, resources) => ({
  async getUserIndex(userId) {
    try {
      const response = await context.$axios.post(resources, null, {

        params: {
          userId,
        },
        headers:{

        }
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getUserByToken() {
    try {
      const response = await context.$axios.post(resources);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
