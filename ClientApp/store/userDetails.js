// This namespaced vuex module is used to store users data (like avatar, banner, etc.)

export const state = () => ({
  userDetails: null,
});

export const mutations = {
  setUserDetails(state, payload) {
    state.userDetails = payload;

  },
};

export const actions = {
  setUserDetails(context, payload) {
    context.commit("setUserDetails", payload);
  },
};

export const getters = {
  getUserDetails(state) {
    return state.userDetails;
  },
};
