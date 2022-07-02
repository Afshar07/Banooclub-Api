export default (context, resources) => ({
  async submitLogin(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      if (response.data.status === 3) {
        context.$toast.error("نام کاربری یا رمز عبور اشتباه است.");
      } else if (response.data.status === 4) {
        context.$toast.error(
          "کد ارسال شده منتقضی شده است،لطفا دوباره امتحان کنید."
        );
      } else if (response.data.status === 5) {
        context.$toast.error("کد وارد شده اشتباه است.");
      } else if (response.data.status === 6) {
        context.$toast.error("کاربری با این نام کاربری وجود ندارد.");
      } else if (response.data.status === 7) {
        // context.$toast.success('موفقیت.')
      } else if (response.data.status === 8) {
        context.$toast.error("قبلا کاربری با این نام کاربری ثبت نام کرده است.");
      } else if (response.data.status === 9) {
        context.$toast.error("پسورد شما ایمن نیست.");
      } else if (response.data.status === 10) {
        context.$toast.error("نام کاربری قبلا ثبت نام کرده است.");
      } else if (response.data.status === 11) {
        context.$toast.error("اکانت شما غیرفعال شده است.");
      }
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async sendOtpToMobile(phoneNumber) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          phoneNumber,
        },
      });
      if (response.data === 3) {
        this.$toast.error("نام کاربری یا رمز عبور اشتباه است.");
      } else if (response.data === 4) {
        this.$toast.error(
          "کد ارسال شده منقضی شده است،لطفا دوباره امتحان کنید."
        );
      } else if (response.data === 5) {
        this.$toast.error("کد وارد شده اشتباه است.");
      } else if (response.data === 6) {
        this.$toast.error("کاربری با این نام کاربری وجود ندارد.");
      } else if (response.data === 7) {
        // this.$toast.success('موفقیت.')
      } else if (response.data === 8) {
        this.$toast.error("قبلا کاربری با این شماره ثبت نام کرده است.");
      } else if (response.data === 9) {
        this.$toast.error("پسورد شما ایمن نیست.");
      } else if (response.data === 10) {
        this.$toast.error("نام کاربری قبلا ثبت نام کرده است.");
      } else if (response.data === 11) {
        this.$toast.error("اکانت شما غیرفعال شده است.");
      }
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async sendOtpToEmail(email) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          email,
        },
      });
      if (response.data === 3) {
        this.$toast.error("نام کاربری یا رمز عبور اشتباه است.");
      } else if (response.data === 4) {
        this.$toast.error(
          "کد ارسال شده منقضی شده است،لطفا دوباره امتحان کنید."
        );
      } else if (response.data === 5) {
        this.$toast.error("کد وارد شده اشتباه است.");
      } else if (response.data === 6) {
        this.$toast.error("کاربری با این نام کاربری وجود ندارد.");
      } else if (response.data === 7) {
        // this.$toast.success('موفقیت.')
      } else if (response.data === 8) {
        this.$toast.error("قبلا کاربری با این ایمیل ثبت نام کرده است.");
      } else if (response.data === 9) {
        this.$toast.error("پسورد شما ایمن نیست.");
      } else if (response.data === 10) {
        this.$toast.error("نام کاربری قبلا ثبت نام کرده است.");
      } else if (response.data === 11) {
        this.$toast.error("اکانت شما غیرفعال شده است.");
      }
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async validateUsername(userName) {
    try {
      const response = await context.$axios.post(resources, null, {
        params: {
          userName,
        },
      });
      return response;
    } catch (error) {
      console.log(error);
    }
  },
  async sendSignupRequest(payload) {
    try {
      const response = await context.$axios.post(resources, payload);
      return response;
    } catch (error) {
      console.log(error);
    }
  },
});
