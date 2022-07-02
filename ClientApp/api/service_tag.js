export default (context, resources) => ({
  async createAServiceTag(payload){
    try {
      const response = await context.$axios.post(resources,payload);
      return response;
    } catch (error){
      console.log(error);
    }
  },
  async deleteAServiceTag(params){
    const response = await context.$axios.post(resources,null,{
      params
    })
    return response
  }
})
