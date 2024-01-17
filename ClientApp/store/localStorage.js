// export default {
//   namespaced: true,
//   state: () => {
//     return {
//       user: {
//         userId: null,
//         name: '',
//         familyName: '',
//         mobile: null,
//
//       },
//       role: '',
//     }
//   },
//   getters: {
//     token: () => {
//       return localStorage.getItem('token')
//     },
//     user(state) {
//       return state.user;
//     },
//   },
//   mutations: {
//     setUser(state, {user}) {
//       state.user.userId = user.userId;
//       state.user.name = user.name;
//       state.user.familyName = user.familyName;
//       state.user.mobile = user.mobile;
//       state.role = user.role;
//     },
//     setAuthentication(state, payload) {
//       localStorage.setItem('token', payload.payload.token);
//       localStorage.setItem('role', payload.payload.user.role);
//     },
//     logout(state) {
//       state.token = '';
//       localStorage.removeItem('token');
//       state.role = '';
//       state.user.userId = null;
//       state.user.name = '';
//       state.user.familyName = '';
//       state.user.mobile = null;
//     }
//   },
//   actions: {
//     login(context, payload) {
//       context.commit('setAuthentication', payload);
//     },
//     logout(context, payload) {
//       context.commit('logout', payload);
//     },
//   }
// }
