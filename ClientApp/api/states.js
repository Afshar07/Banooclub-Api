export default (context, resources) => ({
  async GetAllStates(params) {
    try {
      const response = await context.$axios.get(resources, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  }


});
