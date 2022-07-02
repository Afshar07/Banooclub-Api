export default (context, resources) => ({
  async createForumComment(payload){
    try {
      const response = await context.$axios.post(resources,payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  }

})
