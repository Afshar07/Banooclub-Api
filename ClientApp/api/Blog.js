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
  },
async   GetBlog(params){
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    }catch (e) {
      console.log(e)
    }
  },
  async   GetMostPopular(params){
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    }catch (e) {
      console.log(e)
    }
  },
  async   CreateBlogComment(payload){
    try {
      const response = await context.$axios.post(resources,payload);
      return response;
    }catch (e) {
      console.log(e)
    }
  },
  async   BlogCommentGetById(params){
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    }catch (e) {
      console.log(e)
    }
  },
  async   CreateBlogLike(payload){
    try {
      const response = await context.$axios.post(resources,payload);
      return response;
    }catch (e) {
      console.log(e)
    }
  },
});
