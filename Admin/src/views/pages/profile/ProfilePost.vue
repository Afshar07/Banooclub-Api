<template>
  <div>
    <b-card
      v-for="(data) in posts"
      :key="data.postId"
    >
      <div
        class="d-flex justify-content-start align-items-center mb-1"
      >
        <!-- avatar -->
        <b-avatar
          :src="data.avatar"
          size="50"
          class="mr-1"
        />
        <!--/ avatar -->
        <div class="profile-user-info">
          <h6 class="mb-0">

          </h6>
          <small class="text-muted">  {{data.createDate.slice(11,16)}}
            -
            {{createJalaliDate(data.createDate.slice(0,10))}}</small>
        </div>
      </div>

      <div dir="rtl" class="ql-editor col-12 overflow-hidden main-inset-shadow p-3 rounded"
          v-html="data.content"
      ></div>
<!--      <b-card-text>-->
<!--        {{ data.postText }}-->
<!--      </b-card-text>-->

<!--      &lt;!&ndash; post img &ndash;&gt;-->
<!--      <b-img-->
<!--        v-if="data.postImg"-->
<!--        fluid-->
<!--        rounded-->
<!--        class="mb-25"-->
<!--        :src="data.postImg"-->
<!--      />-->
<!--      &lt;!&ndash;/ post img &ndash;&gt;-->
<!--      &lt;!&ndash; post video &ndash;&gt;-->
<!--      <b-embed-->
<!--        v-if="data.postVid"-->
<!--        type="iframe"-->
<!--        :src="data.postVid"-->
<!--        allowfullscreen-->
<!--        class="rounded mb-50"-->
<!--      />-->
<!--      &lt;!&ndash;/ post video &ndash;&gt;-->

      <!-- likes comments  share-->
      <b-row class="pb-50 mt-50">
        <b-col
          class="d-flex justify-content-between justify-content-sm-start mb-2"
        >
          <b-link class="d-flex align-items-center text-muted text-nowrap">
            <feather-icon
              icon="HeartIcon"
              class="mr-50"
              :class="data.youLiked ? 'profile-likes':'profile-icon'"
              size="18"
            />
            <span>{{ kFormatter(data.likesCount) }}</span>
          </b-link>
          <b-link class="text-body text-nowrap ml-2">
            <feather-icon
                icon="MessageSquareIcon"
                size="18"
                class="profile-icon mr-50"
            />
            <span class="text-muted mr-1">{{ kFormatter(data.commentsCount) }}</span>
          </b-link>
          <b-link class="text-body text-nowrap ml-2" :to="{ name: 'apps-post-edit', params: { id: data.postId } }">
            <feather-icon
                icon="EditIcon"
                size="18"
                class="profile-icon mr-50"
            />
<!--            <span class="text-muted mr-1">ویرایش</span>-->
          </b-link>
          <b-link class="text-body text-nowrap ml-2"  v-b-modal.modal-delete @click="showModal(data.postId)">
            <feather-icon
                icon="TrashIcon"
                size="18"
                class="profile-icon mr-50"
            />
          </b-link>
<!--          <div class="d-flex align-item-center">-->
<!--            <b-avatar-group-->
<!--              size="26"-->
<!--              class="ml-1"-->
<!--            >-->
<!--              <b-avatar-->
<!--                v-for="(avatarData,i) in data.likedUsers"-->
<!--                :key="i"-->
<!--                v-b-tooltip.hover.bottom="avatarData.username"-->
<!--                class="pull-up"-->
<!--                :src="avatarData.avatar"-->
<!--              />-->
<!--            </b-avatar-group>-->
<!--            <b-link class="text-muted text-nowrap mt-50 ml-50">-->
<!--              +{{ data.likedCount }} more-->
<!--            </b-link>-->
<!--          </div>-->
        </b-col>
<!--        <b-col-->
<!--          sm="4"-->
<!--          class="d-flex justify-content-between justify-content-sm-end align-items-center mb-2"-->
<!--        >-->
<!--          -->

<!--        </b-col>-->
      </b-row>
      <!--/ likes comments  share-->

      <!-- comments -->
<!--      <div-->
<!--        v-for="(comment,ind) in data.comments"-->
<!--        :key="ind"-->
<!--        class="d-flex align-items-start mb-1"-->
<!--      >-->
<!--        <b-avatar-->
<!--          :src="comment.avatar"-->
<!--          size="34"-->
<!--          class="mt-25 mr-75"-->
<!--        />-->
<!--        <div class="profile-user-info w-100">-->
<!--          <div class="d-flex align-items-center justify-content-between">-->
<!--            <h6 class="mb-0">-->
<!--              {{ comment.username }}-->
<!--            </h6>-->
<!--            <b-link class="text-body">-->
<!--              <feather-icon-->
<!--                icon="HeartIcon"-->
<!--                size="18"-->
<!--                class="mr-25"-->
<!--                :class="comment.youLiked ? 'profile-likes' :'profile-icon'"-->
<!--              />-->
<!--              <span class="text-muted align-middle">{{ comment.commentsLikes }}</span>-->
<!--            </b-link>-->
<!--          </div>-->
<!--          <small>{{ comment.comment }}</small>-->
<!--        </div>-->
<!--      </div>-->
      <!--/ comments -->

<!--      &lt;!&ndash; comment box &ndash;&gt;-->
<!--      <b-form-group>-->
<!--        <b-form-textarea-->
<!--          rows="3"-->
<!--          placeholder="Add Comment"-->
<!--        />-->
<!--      </b-form-group>-->
<!--      &lt;!&ndash;/ comment box &ndash;&gt;-->

<!--      <b-button-->
<!--        v-ripple.400="'rgba(255, 255, 255, 0.15)'"-->
<!--        size="sm"-->
<!--        variant="primary"-->
<!--      >-->
<!--        Post Comment-->
<!--      </b-button>-->
    </b-card>
    <b-modal
        id="modal-delete"
        centered
        ok-title="حذف"
        cancelTitle="انصراف"
        @ok="deletePost(deleteItem)"
    >
      <span>حذف شود؟</span>
    </b-modal>
  </div>
</template>

<script>
import {
  BAvatar, BCard, BCardText, BImg, BLink, BRow, BCol, BAvatarGroup, VBTooltip, BFormTextarea, BButton, BFormGroup, BEmbed,BModal,
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import { kFormatter } from '@core/utils/filter'
import ToastificationContent from "@core/components/toastification/ToastificationContent";
import Helper from "@/libs/Helper";
import PostDeleteRequest from "@/libs/Api/Post/PostDeleteRequest";

export default {
  components: {
    BAvatar,
    BCard,
    BCardText,
    BButton,
    BFormTextarea,
    BImg,
    BFormGroup,
    BRow,
    BLink,
    BCol,
    BAvatarGroup,
    BEmbed,
    BModal,
  },
  directives: {
    'b-tooltip': VBTooltip,
    Ripple,
  },
  props: {
    posts: {
      type: Array,
      default: () => [],
    },
  },
  data(){
    return{
      deleteItem:null,
    }
  },
  methods: {
    async deletePost(id){
      let _this = this;

      let postDeleteRequest = new PostDeleteRequest(_this)
      postDeleteRequest.setId(id);
      await postDeleteRequest.fetch(function (content){
        _this.$toast({
          component: ToastificationContent,
          position: 'bottom-center',
          props: {
            title: `عملیات موفق`,
            icon: 'CheckIcon',
            variant: 'success',
            text: `کوپن حذف شد.`,
          },
        })
        window.location.reload();
        // _this.getPosts(_this.defaultGetData.pageNumber,_this.defaultGetData.count)
      },function (error){
        console.log(error)
      })
    },
    showModal(param){
      let _this = this;
      _this.deleteItem = param;
    },
    createJalaliDate(param){
      return new Date(param).toLocaleDateString("fa-IR",);
    },
    makePrettyPrice(param){
      return Helper.prettyPrice(param,0)
    },
    kFormatter,
  },
}
</script>
<style>
img{
  max-width: 100%!important;
}
</style>