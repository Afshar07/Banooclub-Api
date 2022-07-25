export default (context, resources) => ({
  async GetAllGroups() {
    try {
      const response = await context.$axios.post(resources);
      return response;
    } catch (error) {
      console.log(error);
    }
  },

});
