export default (context, resources) => ({
  async getAllTickets(pageNumber = 1, count = 50) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          pageNumber,
          count,
        },
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
