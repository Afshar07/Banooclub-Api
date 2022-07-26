export default (context, resources) => ({
  async getAllPlans(params){
    const response = await context.$axios.post(resources, null,{
      params
    });
    return response;
  }
})
