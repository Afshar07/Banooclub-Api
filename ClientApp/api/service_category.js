export default (context, resources) => ({
  async getAllServicesCategory(params){
    const response = await context.$axios.post(resources,null,{
      params
    });
    return response
  },
  async getAServiceCategory(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  }
})
