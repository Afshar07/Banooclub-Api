export default (context, resources) => ({
  async createAServiceProperty(payload){
    try {
      const response = await context.$axios.post(resources,payload);
      return response;
    } catch (error){
      console.log(error);
    }
  },
  async updateAServiceProperty(payload){
    const response = await context.$axios.post(resources,payload)
    return response
  },
  async deleteAServiceProperty(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  }
})
