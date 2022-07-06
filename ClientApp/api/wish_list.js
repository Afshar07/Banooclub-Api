export default (context, resources) => ({
  async createWishList(payload){
      const response = await context.$axios.post(resources,payload);
      return response;
  },
  async deleteWishList(params){
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
  },

})
