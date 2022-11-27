import axios from "axios";
import {getUserData} from "@/auth/utils";
import useJwt from "@/auth/jwt/useJwt";
import {initialAbility} from "@/libs/acl/config";
import ToastificationContent from "@core/components/toastification/ToastificationContent";

class WebServiceRequest {
    static _RESULT_NOT_FOUND = 34;

    constructor (context) {
        this.context = context
        this.data = {
            meta: {
                // reference: 'webapp'
            },
            request: {}
        };
      // let userToken = context.$cookies.get('token');
      // let userToken = context.$auth.strategy.token.get();
      let userToken = getUserData().data.token;
        this.userToken = userToken;
        axios.defaults.headers.common['Authorization'] = userToken;
        axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
    }

    // region base setters

    setPage (s) {
        this.setRequestParam('page', s + '')
    }

    setPerPage (i) {
        this.setRequestParam('per_page', i + '')
    }

    setClientToken (i) {
        this.setMetaParam('client_token', i + '')
    }


    // endregion

    getData () {
        return this.data
    }

    setTag (tag) {
        this.tag = tag
    }

    getTag () {
        return this.tag
    }

    getUrl () {
        return `https://subapi.banoclub.com/api/${this.getTag()}`
      // return `https://bababilit-dev.ir/api/v1/${this.getTag()}`
      //   return `http://192.168.100.97/api/v1/${this.getTag()}`
    }

    setMetaParam (key, value) {
        this.data.meta[key] = value
    }

    setRequestParam (key, value) {
        this.data[key] = value
    }

    setRequestParamDataObj(value){
        this.data = value
    }

    setFormData(data) {
      this.formData = data;
    }
    getFormData() {
      return this.formData;
    }

    // static asyncData ({ params, error }) {
    //     return axios.post('https://jsonplaceholder.typicode.com/posts/asd')
    //         .then((res) => {
    //             return { title: res.data.title }
    //         })
    //         .catch((e) => {
    //             error({ statusCode: 404, message: 'Post not found' })
    //         })
    // }

    async send (_successCallback, _errorCallback) {
        const _this = this

        // add client token to every request
        // this.setClientToken(ClientTokenHelper.getClientToken())

        // add token to every request
        //this.setToken(UserHelper.getToken())

        try {
            await axios.post(this.getUrl(), this.getData()).then(function (response) {
                const _data = response.data
                if (_data) {
                    _successCallback(_data.body, _data.message)
                } else {
                    // token invalid mode



                    _errorCallback(_data.message)
                }
            })
        } catch (e) {
            console.log('error in WebServiceRequest send function',e)
       }
    }

    async fetch (successCallback, errorCallback) {
        const _this = this

        // add client token to every request
        // this.setClientToken(ClientTokenHelper.getClientToken())

        // add token to every request
        //this.setToken(UserHelper.getToken())

        try {
            const _data = await fetch(_this.getUrl(), {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.
                 // mode: 'no-cors', // no-cors, *cors, same-origin
                // cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
                // credentials: 'same-origin', // include, *same-origin, omit
                headers: {
                    'Content-Type': 'application/json' ,
                    // 'content-type': 'multipart/form-data;boundary=<calculated when request is sent>',
                    'Access-Control-Allow-Origin': '*',
                    // 'Access-Control-Allow-Origin': 'origin',
                    'Authorization':  _this.userToken,
                    'platform':'Web'
                },
                // redirect: 'follow', // manual, *follow, error
                // referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
                body: JSON.stringify(_this.getData()) // body data type must match "Content-Type" header
            }).then( async  function (response) {
              if (response.status === 401) {

                  localStorage.removeItem(useJwt.jwtConfig.storageTokenKeyName)
                  localStorage.removeItem(useJwt.jwtConfig.storageRefreshTokenKeyName)

                  // Remove userData from localStorage
                  localStorage.removeItem('userData')

                  // Reset ability
                  _this.context.$ability.update(initialAbility)

                  // Redirect to login page
                  _this.context.$router.push({ name: 'auth-login' })
              }
              if (response.status === 500){
                  _this.context.$toast({
                      component: ToastificationContent,
                      position: 'bottom-center',
                      props: {
                          title: `توجه`,
                          icon: '',
                          variant: 'danger',
                          text: `مشکلی در سرور رخ داده است لطفا بعدا تلاش کنید.`,
                      },
                  })
                  // _this.context.$router.back();
              }
              if (response.status === 400){
                 return _this.context.$toast({
                     component: ToastificationContent,
                     position: 'bottom-center',
                     props: {
                         title: `توجه`,
                         icon: '',
                         variant: 'danger',
                         text: `اطلاعات وارد شده صحیح نمیباشند.`,
                     },
                 })
              }
                const isJson = response.headers.get('content-type')?.includes('application/json');
                const data = isJson ? await response.json() : "success";
                return data;
            })
            if (_data) {
                successCallback(_data)
            } else {
                errorCallback(_data)
            }
        } catch (e) {
          console.log(e)
        }
    }
    async sendFormData (successCallback, errorCallback) {
      const _this = this

      // add client token to every request
      // this.setClientToken(ClientTokenHelper.getClientToken())

      // add token to every request
      //this.setToken(UserHelper.getToken())


      try {
        await axios.post(this.getUrl(), this.getFormData(), {
          headers: {
            'Content-Type': 'multipart/form-data',
            // 'content-type': 'multipart/form-data;boundary=<calculated when request is sent>',
            // 'Access-Control-Allow-Origin': '*',
            // 'Access-Control-Allow-Origin': 'origin',
            'Authorization': _this.userToken,
            'platform': 'Web'
          },
        }).then(function (response) {
          const _data = response
          if (_data) {
            successCallback(_data)
          } else {
            // token invalid mode
            errorCallback(_data)
          }
        })
      } catch (e) {
        console.log('error in WebServiceRequest send function', e)
      }
    }
      // if(_this.context != undefined){
      //   Helper.closeLoadingOverlay(_this.context);
      //   // _this.context.$toastMessage(_this.context , 'متاسفانه خطایی در سرور رخ داده است.' ,'error');
      // }



}

WebServiceRequest._ERROR_USER_BLOCKED = 35
WebServiceRequest._ERROR_YOU_CANT_CHAT_WITH_YOUR_SHOP = 34
WebServiceRequest._ERROR_SERVER_ERROR = 500
WebServiceRequest._RESULT_USER_NOT_ALLOWED = 501

export default WebServiceRequest
