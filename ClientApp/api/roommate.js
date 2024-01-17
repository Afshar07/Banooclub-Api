export default (context, resources) => ({
  async getRoommateByUserId(userId) {
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
  async getAllRoommates() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getMyRoommate() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
