export default (context, resources) => ({
  async createRate(params){
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getARate(params){
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  }


})
