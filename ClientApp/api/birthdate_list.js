export default (context, resources) => ({
  async getBirthdateList() {
    try {
      const response = await context.$axios.post(resources);
      return response;
    } catch (error) {
      console.log(error);
    }
  }
});
