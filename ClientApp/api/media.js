export default (context, resources) => ({
  async uploadVideo(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getAUserVideos() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getUserMedias(params) {
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getUserMediasByUserName(params) {
    try {
      const response = await context.$axios.post(resources,null,{
        params
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getMyPhotos() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async getMyMediaGallery() {
    try {
      const response = await context.$axios.post(resources, null);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async uploadToPhotoGallery(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async uploadMediaGallery(payload) {
    const response = await context.$axios.post(resources, payload);
    return response;
  },
});
