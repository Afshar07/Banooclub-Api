export default (context, resources) => ({
  async createAPayment(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async ChangePaymentStatus(params) {
    try {
      const response = await context.$axios.post(resources, null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async GetMyPayments(params){
    try {
      const response = await context.$axios.post(resources, null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },

  async PayByWallet(params) {
    try {
      const response = await context.$axios.post(resources, null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async GetCredit() {
    try {
      const response = await context.$axios.post(resources);
      return response;
    } catch (error) {
      console.log(error);
    }
  },



})
