export default (context, resources) => ({
  async createComment(payload){
    try {
      const response = await context.$axios.post(resources,payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async createCommentLike(params){
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
