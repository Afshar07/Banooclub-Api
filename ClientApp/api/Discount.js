export default (context, resources) => ({
  async CreateDiscount(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async UpdateDiscount(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async DeleteDiscount(params) {
    try {
      const response = await context.$axios.post(resources, null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },

});
