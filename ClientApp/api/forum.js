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
  async DeleteForum(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async UpdateForum(payload){
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
  },
  async CreateLike(payload){
    const response = await context.$axios.post(resources,payload)
    return response
  },
  async ReportForum(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async GetTopCommenters(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  }
})
