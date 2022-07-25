export default (context, resources) => ({
  async GetMenu() {
    try {
      const response = await context.$axios.post(resources);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async GetConversation(params) {
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async DeliverMessage() {
    try {
      const response = await context.$axios.post(resources);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async ReadMessage(params) {
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async SendMessage(paylaod) {
    try {
      const response = await context.$axios.post(resources,paylaod);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
