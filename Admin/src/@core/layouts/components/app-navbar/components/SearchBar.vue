<template>
  <li class="nav-item nav-search d-flex">

    <v-select
        v-model="SearchTypeId"
        :options="SearchTypes"
        label="label"
        :reduce="label => label.status"
        @input="showSearchBar = true"
        input-id="user-status"
    />
    <!-- Input -->
    <div
        class="search-input"
        :class="{'open': showSearchBar}"
    >
      <div class="search-input-icon">
        <feather-icon icon="SearchIcon"/>
      </div>

      <b-form-input
          v-if="showSearchBar"
          v-model="searchQuery"
          placeholder="جستجو کاربران"
          autofocus
          autocomplete="off"
          @keyup.up="increaseIndex(false)"
          @keyup.down="increaseIndex"
          @keyup.esc="showSearchBar = false; resetsearchQuery()"
          @keyup.enter="suggestionSelected"
      />

      <div
          class="search-input-close"
          @click="showSearchBar = false; resetsearchQuery()"
      >
        <feather-icon icon="XIcon"/>
      </div>

      <!-- Suggestions List -->
      <vue-perfect-scrollbar
          :settings="perfectScrollbarSettings"
          class="search-list search-list-main scroll-area overflow-hidden"
          :class="{'show': searchQuery}"
          tagname="ul"
      >
        <ul>
          <li
              v-if="SearchedUsers.length>0"
          v-for="(item,idx) in SearchedUsers"
          :key="idx"
              class="suggestions-groups-list p-2 UserItem"
          @click="RouteToUserPage(item)"
          >
            <div class=" d-flex align-items-center ">
              <img v-if="item.selfieFileData" class="rounded-circle" style="width: 50px;height: 50px" :src="`https://banooclubapi.oregonserver.com/${item.selfieFileData}`" alt="">

          <feather-icon v-else icon="UserIcon"></feather-icon>
              <div class="d-flex mx-4 align-items-start flex-column">
                <strong>{{item.name + ' ' + item.familyName}}</strong>
                <span>@{{item.userName }}</span>

              </div>
            </div>
          </li>
          <li v-else>
            <span class="text-secondary">موردی پیدا نشد</span>
          </li>
        </ul>

      </vue-perfect-scrollbar>
    </div>
  </li>
</template>

<script>
import {
  BFormInput, BLink, BImg, BAvatar,
} from 'bootstrap-vue'
import {ref, watch} from '@vue/composition-api'
import VuePerfectScrollbar from 'vue-perfect-scrollbar'
import useAutoSuggest from '@core/components/app-auto-suggest/useAutoSuggest'
import {title} from '@core/utils/filter'
import router from '@/router'
import store from '@/store'
import UsersAdminSearchRequest from "@/libs/Api/Users/UsersAdminSearchRequest";
import searchAndBookmarkData from '../search-and-bookmark-data'
import vSelect from "vue-select";

export default {
  components: {
    BFormInput,
    vSelect,
    BLink,
    BImg,
    BAvatar,
    VuePerfectScrollbar,
  },
  data() {
    return {
      searchQuery: '',
      SearchTypes: [
        {
          label: 'جستجو بر اساس',
          status: 0
        },
        {
          label: 'نام کاربری',
          status: 1
        },
        {
          label: 'شماره تلفن',
          status: 2
        },
        {
          label: 'شناسه کاربری',
          status: 3
        },
      ],
      SearchTypeId: 0,
      SearchedUsers:[]
    }
  },

  watch:{
    searchQuery:function (val){
      this.SearchUsers()
    }
  },

  methods: {
    RouteToUserPage(item){
      this.$router.push(`/apps/users/Detail/${item.userName}`)
      this.showSearchBar = false
      this.searchQuery = ''
    },
    async SearchUsers() {
      let _this = this
      let usersAdminSearchRequest = new UsersAdminSearchRequest()
      let data={
        search:this.searchQuery,
        searchType:this.SearchTypeId
      }
      usersAdminSearchRequest.setParams(data)
      await usersAdminSearchRequest.fetch((content)=>{
        _this.SearchedUsers = content

      },(e)=>{
        console.log(e)
      })
    }
  },
  setup() {
    const showSearchBar = ref(false)

    const perfectScrollbarSettings = {
      maxScrollbarLength: 60,
    }

    const suggestionSelected = (grpName, suggestion) => {
      // If parameter is not provided => Use current selected
      if (!suggestion) {
        // If currentSelected value is -1 => No value/item is selected (Prevent Errors)
        /* eslint-disable no-use-before-define, no-param-reassign */
        if (currentSelected.value !== -1) {
          /* eslint-disable no-use-before-define, no-param-reassign */
          const [grpIndex, itemIndex] = currentSelected.value.split('.')
          grpName = Object.keys(filteredData.value)[grpIndex]
          suggestion = filteredData.value[grpName][itemIndex]
          /* eslint-enable */
        }
      }
      if (grpName === 'pages') router.push(suggestion.route).catch(() => {
      })
      // eslint-disable-next-line no-use-before-define
      resetsearchQuery()
      showSearchBar.value = false
    }

    const {
      searchQuery,
      resetsearchQuery,
      filteredData,
    } = useAutoSuggest({data: searchAndBookmarkData, searchLimit: 4})

    watch(searchQuery, val => {
      store.commit('app/TOGGLE_OVERLAY', Boolean(val))
    })

    const currentSelected = ref(-1)
    watch(filteredData, val => {
      if (!Object.values(val).some(obj => obj.length)) {
        currentSelected.value = -1
      } else {
        // Auto Select first item if it's not item-404
        let grpIndex = null

        // eslint-disable-next-line no-restricted-syntax
        for (const [index, grpSuggestions] of Object.values(val).entries()) {
          if (grpSuggestions.length) {
            grpIndex = index
            break
          }
        }

        if (grpIndex !== null) currentSelected.value = `${grpIndex}.0`
      }
    })

    const increaseIndex = (val = true) => {
      /* eslint-disable no-lonely-if, no-plusplus */

      // If there's no matching items
      if (!Object.values(filteredData.value).some(grpItems => grpItems.length)) return

      const [grpIndex, itemIndex] = currentSelected.value.split('.')

      const grpArr = Object.entries(filteredData.value)
      const activeGrpTotalItems = grpArr[grpIndex][1].length

      if (val) {
        // If active item is not of last item in grp
        if (activeGrpTotalItems - 1 > itemIndex) {
          currentSelected.value = `${grpIndex}.${Number(itemIndex) + 1}`

          // If active item grp is not last in grp list
        } else if (grpIndex < grpArr.length - 1) {
          for (let i = Number(grpIndex) + 1; i < grpArr.length; i++) {
            // If navigating group have items => Then move in that group
            if (grpArr[i][1].length > 0) {
              currentSelected.value = `${Number(i)}.0`
              break
            }
          }
        }
      } else {
        // If active item is not of first item in grp
        if (Number(itemIndex)) {
          currentSelected.value = `${grpIndex}.${Number(itemIndex) - 1}`

          // If active item grp  is not first in grp list
        } else if (Number(grpIndex)) {
          for (let i = Number(grpIndex) - 1; i >= 0; i--) {
            // If navigating group have items => Then move in that group
            if (grpArr[i][1].length > 0) {
              currentSelected.value = `${i}.${grpArr[i][1].length - 1}`
              break
            }
          }
        }
      }
      /* eslint-enable no-lonely-if, no-plusplus */
    }

    return {
      showSearchBar,
      perfectScrollbarSettings,
      searchAndBookmarkData,
      title,
      suggestionSelected,
      currentSelected,
      increaseIndex,

      // AutoSuggest
      searchQuery,
      resetsearchQuery,
      filteredData,
    }
  },
}
</script>

<style lang="scss" scoped>
@import '~@core/scss/base/bootstrap-extended/include';
@import '~@core/scss/base/components/variables-dark';

ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

p {
  margin: 0;
}

/* .app-auto-suggest {
  position: relative;
}

.auto-suggest-suggestions-list {
  box-shadow: 0 4px 24px 0 rgba(34, 41, 47, 0.1);
  border-radius: 6px;
  position: absolute;
  top: calc(100% + 1rem);
  background-color: white;
  width: 100%;
} */
.UserItem:hover{
  cursor: pointer;
  background-color: #cccccc;
  transition: .2s ease;
}
.suggestion-group-title {
  font-weight: 500;
  padding: .75rem 1rem .25rem;
}

.suggestion-group-suggestion {
  padding: .75rem 1rem
}

.suggestion-current-selected {
  background-color: $body-bg;

  .dark-layout & {
    background-color: $theme-dark-body-bg;
  }
}
</style>
