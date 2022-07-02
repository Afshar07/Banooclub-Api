export default (context, resources) => ({
  async getAllForumCategory(params){
    const response = await context.$axios.post(resources,null,{
      params,
    });
    return response
  },
  async createForum(payload){
    const response = await context.$axios.post(resources,payload)
    return response
  },
  async getAllForums(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response

  },
  async getMyForums(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async getAForum(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  }
})
