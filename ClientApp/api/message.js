export default (context, resources) => ({
  async getMenu() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async deliverMessage() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async isMessageReaded(userId) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          userId,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async sendMessage(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getConversation(groupId, messageId, userId, count = 10) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          groupId,
          userId,
          messageId,
          count,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
