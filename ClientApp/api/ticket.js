export default (context, resources) => ({
  async getAllTickets(params) {
    try {
      const response = await context.$axios.post(resources, null, {
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async createATicket(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAllTicketsByParentId(parentId) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          parentId,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
