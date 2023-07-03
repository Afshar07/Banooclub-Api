export default (context, resources) => ({

  async setTag() {
    const response = await context.$axios.post(resources);
    return response;
  },
  async setParams(params) {

    const response = await context.$axios.post(resources, null, {
      params
    });
    return response;
    // }

  },
  async setPartailUrl(params, partail) {
    const response = await context.$axios.post(resources + `/${partail.id}`, null, {
      params
    });
    return response;
  },
  async setParamsGet(params) {
    const response = await context.$axios.get(resources, {
      params
    });
    return response;

  },
  async setPayload(payload) {

    const response = await context.$axios.post(resources, payload);
    return response;
  },
  async setParamsBody(payload, params) {
    const response = await context.$axios.post(resources, payload, {
      params
    });
    return response;


  },
  async setConfig(payload, config) {

    const response = await context.$axios.post(resources, payload, config);
    return response;


  },
  async setFormData(payload, config) {
    const response = await context.$axios.post(resources, payload, config);
    return response;
  },


})
