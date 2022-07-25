export default (context, resources) => ({
  async createAService(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAllServices(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async getMyServices(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async GetwithView(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async getAService(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async getUserServices(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async getUserServicesByUserName(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  },
  async updateAService(payload){
    const response = await context.$axios.post(resources,payload)
    return response
  },
  async deleteAService(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  }
})
