<template>
  <div class="container-fluid px-0">
    <div class="row boxMainContent">
      <div class="col-12">
        <h5 class="editAccountSetting text-black d-inline-block">
          <i class="far fa-heart text-primary"></i>
          علایق من
        </h5>
      </div>
      <div class="col-12">
        <p class="editAccountSettingText">در این بخش تنظمات مربوط به پانل کاربری در اختیار کاربر قرار داده خواهد شد.در
          این بخش کاربر قادر خواهد بود امکان دریافت یک نسخه از ایمیل های دریافتی به ایمیل ثبت شده در پروفایل خود را فعال
          یا غیرفعال نماید</p>
      </div>
      <form class="row">
        <div class="col-12">
          <label for="source" class="EditInterestLabel">افزودن منابع:</label>
        </div>
        <div class="col-12">
          <input class="EditInterestInput" id="source" type="text" placeholder="عکاسی ، سفر ، دوچرخه"
                 v-model.trim="newSourceItem"/>
          <button class="btn btn-sm EditInterestButton text-white" type="button" @click="addNewSource">اضافه کردن
          </button>
        </div>
      </form>
      <div class="row">
        <div class="col-12 sourceItems" v-for="(item,index) in sources" :key="index">
          <div class="d-flex flex-row">
            <div class="ms-2">
              <span class="remove" @click="removeSourceIndex(index)">
                <i class="fas fa-times"></i>
              </span>
            </div>
            <div class="sourceItem">{{ item }}</div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-12 mt-3">
          <button type="button" class="btn btn-sm ButtonAccountSetting">لغو</button>
          <button type="button" class="btn btn-sm ButtonAccountSetting" @click="submitEditInterest">به روز رسانی
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditInterest",
  layout: 'PoshtebamPlusLayout',
  data() {
    return {
      sources: ['دوچرخه', 'سفر', 'عکاسی', 'خرید', 'غذا', 'هتل', 'دوچرخه'],
      newSourceItem: ''
    }
  },
  methods: {
    removeSourceIndex(IndexItem) {
      this.sources.splice(IndexItem, 1);
    },
    addNewSource() {
      if (this.newSourceItem.length > 0) {
        this.sources.push(this.newSourceItem);
        this.newSourceItem = null;
      }

    },
    submitEditInterest() {
      console.log(' submit EditInterest');
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.$nuxt.$loading.finish();
    })
  }
}
</script>

<style scoped>
.boxMainContent {
  background: #fdfdfd none repeat scroll 0 0;
  border: 1px solid #ede9e9;
  border-radius: 3px;
  display: inline-block;
  width: 100%;
  margin-bottom: 20px;
  padding: 25px;
}

.editAccountSetting {
  font-size: 18px;
  text-transform: capitalize;
  margin-bottom: 20px;
  padding-bottom: 20px;
  border-bottom: 3px solid #088dcd;
}

.editAccountSettingText {
  font-size: 14px;
  line-height: 26px;
  color: #8c8b8b;
}

.EditInterestLabel {
  margin-bottom: 0.5rem;
  color: #545454;
}

.EditInterestInput {
  border: 1px solid #eee;
  font-size: 14px;
  padding: 10px 20px;
  width: 75%;
}

.EditInterestButton {
  padding: 11px 30px;
  background: #088dcd;
  border: medium none;
  font-size: 13px;

}

.sourceItems {
  margin-top: 10px;
  margin-bottom: 0;
}

.sourceItems:first-child {
  margin-top: 35px;
}

.sourceItem {
  background: #ebebeb none repeat scroll 0 0;
  display: inline-block;
  font-size: 14px;
  padding: 4px 15px;
  text-transform: capitalize;
  border-radius: 3px;
  line-height: 1.5;
  color: #545454;
}

.remove {
  cursor: pointer;
}

.ButtonAccountSetting {
  font-size: 16px;
  background-color: #4f93ce;
  color: white;
  margin-top: 10px;
  padding: 7px 30px;
  cursor: pointer;
  -webkit-transition: background-color .28s ease, color .28s ease, -webkit-box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, -webkit-box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, box-shadow .28s ease;
  transition: background-color .28s ease, color .28s ease, box-shadow .28s ease, -webkit-box-shadow .28s ease;
  overflow: hidden;
  -webkit-box-shadow: 0 2px 2px 0 rgba(0, 0, 0, .14), 0 3px 1px -2px rgba(0, 0, 0, .2), 0 1px 5px 0 rgba(0, 0, 0, .12);
  box-shadow: 0 2px 2px 0 rgba(0, 0, 0, .14), 0 3px 1px -2px rgba(0, 0, 0, .2), 0 1px 5px 0 rgba(0, 0, 0, .12);
}

.ButtonAccountSetting:hover {
  background-color: #088dcd;
  -webkit-box-shadow: 0 6px 10px 0 rgba(0, 0, 0, .14), 0 1px 18px 0 rgba(0, 0, 0, .12), 0 3px 5px -1px rgba(0, 0, 0, .2);
  box-shadow: 0 6px 10px 0 rgba(0, 0, 0, .14), 0 1px 18px 0 rgba(0, 0, 0, .12), 0 3px 5px -1px rgba(0, 0, 0, .2)
}

.ButtonAccountSetting:active::before,
.ButtonAccountSetting:focus::before {
  -webkit-transition: opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  transition: opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  transition: transform 1.12s ease, opacity .28s ease 364ms;
  transition: transform 1.12s ease, opacity .28s ease 364ms, -webkit-transform 1.12s ease;
  -webkit-transform: translate(-50%, -50%) scale(1);
  transform: translate(-50%, -50%) scale(1);
  opacity: 0
}

.ButtonAccountSetting:focus {
  outline-style: none;
}


@media (max-width: 576px) {
  .boxMainContent {
    background: #fdfdfd none repeat scroll 0 0;
    border: 1px solid #ede9e9;
    border-radius: 3px;
    display: inline-block;
    width: 100%;
    margin-bottom: 20px;
    padding: 30px 15px;
  }
}
@media (max-width: 1220px) {
  .EditInterestButton {
    margin: 10px 2px;
    padding: 6px 30px;
  }

  .EditInterestInput {
    width: 100%;
  }
}
</style>
