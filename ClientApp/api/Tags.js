export default (context, resources) => ({
  async AddTag(payload){
    const response = await context.$axios.post(resources,payload);
    return response
  },
  async DeleteTag(params){
    const response = await  context.$axios.post(resources,null,{
      params
    })
    return  response
  }






})
