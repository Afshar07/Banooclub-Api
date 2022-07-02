export default (context, resources) => ({
  async getAllAdsCategory(pageNumber = 1, count = 50) {
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
  async getAllCountries() {
    try {
      const response = await context.$axios.get(resources);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getCitiesByCountry(countryId) {
    try {
      const response = await context.$axios.get(resources, {
        params: {
          countryId,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async createAnAd(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAdsByUserId(userId, count = 10, pageNumber = 1) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          userId,
          count,
          pageNumber,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAnAd(id) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          id,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async changeAnAdStatus(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAllRejectedAds(pageNumber, count, search) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          pageNumber,
          count,
          search,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAllPendingAds(pageNumber, count, search) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          pageNumber,
          count,
          search,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
