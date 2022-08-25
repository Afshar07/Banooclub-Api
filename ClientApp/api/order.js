export default (context, resources) => ({
  async createAOrder(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAOrder(params) {
    try {
      const response = await context.$axios.post(resources, null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async OrderGetByUserId(params) {
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },


})
