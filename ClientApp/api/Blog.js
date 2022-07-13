export default (context, resources) => ({
  async GetAllBlog(params) {
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  }
});
