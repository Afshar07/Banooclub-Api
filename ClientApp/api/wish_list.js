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
  async GetWishlist(params){
    const response = await context.$axios.post(resources,null,{
      params
    });
    return response;
  },
  async GetMyWishList(){
    const response = await context.$axios.post(resources);
    return response;
  },

  async toggleWishList(params){
    const response = await context.$axios.post(resources,null,{
      params
    });
    return response;
  },
})
