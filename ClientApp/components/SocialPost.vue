<template>
  <div class="container-fluid px-0" style="border-radius: 10px;">
    <div
      class="modal modal-dialog fade"
      id="staticBackdrop"
      data-bs-keyboard="false"
      tabindex="-1"
      aria-labelledby="staticBackdropLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header position-relative">
            <h5
              class="modal-title small text-muted border-0"
              id="staticBackdropLabel"
            >
              گزارش تخلف
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="col-12">
                <div class="d-flex">
                  <div class="w-100">
                        <textarea
                          row="100"
                          class="form-control border rounded px-0 w-100"
                          placeholder="علت گزارش را بنویسید "
                          id="about"
                          v-model.trim="reportText"
                        ></textarea>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-outline-danger closingReport"
              data-bs-dismiss="modal"
            >
              انصراف
            </button>
            <button
              type="button"
              class="btn btn-outline-success"
              @click="reportPostSubmit(modalPostId, modalUserId)"
            >
              ثبت گزارش
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- Modal -->
    <!--    <div-->
    <!--      class="modal fade"-->
    <!--      id="Comments"-->
    <!--      tabindex="-1"-->
    <!--      aria-labelledby="exampleModalLabel"-->
    <!--      aria-hidden="true"-->
    <!--      style="z-index: 9999999999"-->
    <!--    >-->
    <!--      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">-->
    <!--        <div class="modal-content">-->
    <!--          <div class="modal-body" @scroll="handleScroll($event)">-->
    <!--            <ul class="we-comet px-0">-->
    <!--              <li-->
    <!--                class="position-relative my-3"-->
    <!--                v-for="(comItem, index) in SelectedPostComments"-->
    <!--                :key="index"-->
    <!--              >-->
    <!--                &lt;!&ndash;              <transition-group name="list">&ndash;&gt;-->
    <!--                <span-->
    <!--                  v-if="showDeleteInMyPost"-->
    <!--                  class="position-absolute CommentDeleteIcon"-->
    <!--                  @click="deleteComment(comItem.postCommentId)"-->
    <!--                >-->
    <!--                  <font-awesome-icon icon="trash" color="orange" size="lg" />-->
    <!--                </span>-->
    <!--                <div-->
    <!--                  class="comet-avatar"-->
    <!--                  @click="goToUserProfile(comItem.userInfo.userId)"-->
    <!--                >-->
    <!--                  <img-->
    <!--                    class="img-fluid myPicture"-->
    <!--                    :src="`https://banooclubapi.simagar.com/${comItem.userInfo.selfieFileData}`"-->
    <!--                    alt=""-->
    <!--                  />-->
    <!--                </div>-->
    <!--                <div class="we-comment">-->
    <!--                  <div class="coment-head">-->
    <!--                    <span>{{-->
    <!--                      comItem.createDate | moment("jYYYY/jM/jD HH:mm")-->
    <!--                    }}</span>-->
    <!--                    <font-awesome-icon-->
    <!--                      @click="showReply(comItem.postCommentId)"-->
    <!--                      icon="reply"-->
    <!--                      color="#088dcd"-->
    <!--                    ></font-awesome-icon>-->
    <!--                  </div>-->
    <!--                  <p>-->
    <!--                    {{ comItem.content }}-->
    <!--                  </p>-->

    <!--                  <div-->
    <!--                    class="col-12"-->
    <!--                    v-if="showReplyBox.includes(comItem.postCommentId)"-->
    <!--                  >-->
    <!--                    <div class="d-flex flex-row justify-content-between my-3">-->
    <!--                      <div class="w-75">-->
    <!--                        <input-->
    <!--                          type="text"-->
    <!--                          class="w-100 border"-->
    <!--                          v-model="replayText"-->
    <!--                        />-->
    <!--                      </div>-->
    <!--                      <div>-->
    <!--                        <button-->
    <!--                          class="btn btn-sm AddReplyBtn"-->
    <!--                          data-bs-dismiss="modal"-->
    <!--                          @click="saveReply(comItem.postCommentId)"-->
    <!--                        >-->
    <!--                          ثبت-->
    <!--                        </button>-->
    <!--                        <button-->
    <!--                          class="btn btn-sm btn-danger"-->
    <!--                          @click="cancelShowReply(comItem.postCommentId)"-->
    <!--                        >-->
    <!--                          لغو-->
    <!--                        </button>-->
    <!--                      </div>-->
    <!--                    </div>-->
    <!--                  </div>-->
    <!--                </div>-->
    <!--                &lt;!&ndash;              </transition-group>&ndash;&gt;-->
    <!--              </li>-->
    <!--            </ul>-->
    <!--            <div class="col-12 text-center moreComments" style="color: #088dcd">-->
    <!--              مشاهده کامنت های بیشتر-->
    <!--            </div>-->
    <!--          </div>-->
    <!--          <div class="modal-footer justify-content-center">-->
    <!--            <div class="row">-->
    <!--              <div class="col-md-12">-->
    <!--                <div class="d-flex align-items-center gap-3">-->
    <!--                  <img-->
    <!--                    class="img-fluid myPicture m-0"-->
    <!--                    :src="`https://banooclubapi.simagar.com/${$store.state.HeaderData.selfie}`"-->
    <!--                    alt=""-->
    <!--                  />-->
    <!--                  <input-->
    <!--                    v-model="CommentContent"-->
    <!--                    type="text"-->
    <!--                    class="form-control"-->
    <!--                  />-->
    <!--                  <button-->
    <!--                    class="btn btn-primary"-->
    <!--                    data-bs-dismiss="modal"-->
    <!--                    @click="addComment"-->
    <!--                  >-->
    <!--                    ثبت-->
    <!--                  </button>-->
    <!--                </div>-->
    <!--              </div>-->
    <!--            </div>-->
    <!--          </div>-->
    <!--        </div>-->
    <!--      </div>-->
    <!--    </div>-->

    <div
      class="row boxMainContent position-relative mx-auto mb-3 main-shadow my-4 p-3"
      v-for="(item,idx) in postData"
      :key="idx"
      style="box-shadow: 0 1px 3px 0 rgb(0 0 0 / 10%), 0 1px 2px 0 rgb(0 0 0 / 6%);"
    >
      <div class="col-12 p-0">
        <!-- <span
          v-if="showDeleteInMyPost"
          class="position-absolute deleteIcon"
          @click="$emit('checkData', item.postId)"
        >
          <font-awesome-icon icon="trash" color="#ff4d4d" size="lg" />
        </span> -->
        <div class="col-12 p-0">
          <div class="d-flex justify-content-between">
            <div class="d-flex flex-row mb-3 align-items-center ">
              <div class="justify-content-center align-items-center position-relative">
                <img
                  v-if="item.userInfo.selfieFileData"
                  class="IMG-FLUID myPicture"
                  :src="`https://banooclubapi.simagar.com/${item.userInfo.selfieFileData}`"
                  alt=""
                />
                <img
                  v-else
                  class="IMG-FLUID myPicture"
                  src="../assets/images/defaultUser.png"
                  alt=""
                />
              </div>
              <div class="px-3 tw-text-black">
                <div class="d-flex flex-column text-end justify-content-between tw-font-semibold tw-capitalize tw-text-black">
                  <div
                    class="authorName tw-text-black"
                    v-if="$auth.user.userInfo.userId !== item.userId"
                    style="font-size: 18px; font-weight: 600;"
                    @click="goToUserProfile(item)"
                  >
                    <template v-if="item.userId === $auth.user.userInfo.userId">
                      {{ $auth.user.baseData.name + " " + $auth.user.baseData.familyName }}
                    </template>
                    <template v-else>
                      {{ item.userInfo.name + " " + item.userInfo.familyName }}
                    </template>
                  </div>
                  <div
                    class="authorName tw-text-black"
                    v-else
                    v-html="userName"
                  ></div>
                  <div class="friendEmailStatus" style="color:#565f6c;font-weight: 600">
                    {{ item.createDate | moment("jYYYY/jM/jD HH:mm") }}
                  </div>
                </div>
              </div>
            </div>
            <div>
              <button class="tw-text-2xl hover:tw-bg-gray-200 tw-rounded-full tw-p-2 tw-transition tw--mr-1 tw-relative"
                      @click="showMore(item.postId)">
                <MoreIcon/>
              </button>
              <div :ref="`ShowMore${item.postId}`"
                   class="tw-z-10 d-none tw-absolute tw-left-0 tw-bg-white tw-w-56 tw-shadow-md tw-mx-auto tw-p-2 tw-rounded-md tw-text-gray-500 tw-text-base tw-border tw-border-gray-100">
                <ul class="tw-pl-0 mb-0">
                  <li>
                    <nuxt-link to="#"
                               class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                      <ShareIcon class="tw-ml-1"/>
                      اشتراک گذاری
                    </nuxt-link>
                  </li>
                  <li>
                    <nuxt-link to="#"
                               class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                      <EditIcon style="width: 19px; height: 19px;" class="tw-ml-1"/>
                      ویرایش پست
                    </nuxt-link>
                  </li>
                  <li>
                    <nuxt-link to="#"
                               class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                      <MessageIcon fill="black" class="tw-ml-1"/>
                      غیر فعال کردن نظرات

                    </nuxt-link>
                  </li>
                  <li>
                    <nuxt-link to="#"
                               class="tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                      <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-ml-1" fill="none"
                           viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                        <path stroke-linecap="round" stroke-linejoin="round"
                              d="M11.049 2.927c.3-.921 1.603-.921 1.902 0l1.519 4.674a1 1 0 00.95.69h4.915c.969 0 1.371 1.24.588 1.81l-3.976 2.888a1 1 0 00-.363 1.118l1.518 4.674c.3.922-.755 1.688-1.538 1.118l-3.976-2.888a1 1 0 00-1.176 0l-3.976 2.888c-.783.57-1.838-.197-1.538-1.118l1.518-4.674a1 1 0 00-.363-1.118l-3.976-2.888c-.784-.57-.38-1.81.588-1.81h4.914a1 1 0 00.951-.69l1.519-4.674z"/>
                      </svg>
                      افزودن به محبوب ها
                    </nuxt-link>
                  </li>
                  <li>
                    <hr class="-mx-2 my-2 dark:border-gray-800">
                  </li>
                  <li>
                    <nuxt-link to="#"
                               class="text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 tw-text-red-500 hover:tw-bg-red-100 hover:tw-text-red-500 tw-rounded-md">
                      <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-ml-1" fill="none"
                           viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
                        <path stroke-linecap="round" stroke-linejoin="round"
                              d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"/>
                      </svg>
                      حذف
                    </nuxt-link>
                  </li>
                </ul>

              </div>
            </div>
          </div>


        </div>
        <div style="text-align: center;min-width: 100%;width: 100%" dir="rtl"
             class="ql-editor col-12 overflow-hidden main-inset-shadow  p-0" v-html="item.description"></div>
        <div class="tw-flex mt-3  lg:tw-font-bold">
          <button @click="likePost(item.postId, item.userId)"
                  class="tw-flex tw-items-center tw-space-x-2 text-decoration-none text-dark">
            <div class="tw-p-2 tw-rounded-full  tw-text-black lg:tw-bg-gray-100 dark:tw-bg-gray-600 d-flex align-items-center">
              <LikeIcon class="mb-1" :fill_color="item.isLikedByMe ? '#ff2631' : '#000000'"/>
            </div>
            <div class="mx-2">
              لایک
            </div>
          </button>
                    <button href="#Comments" data-bs-toggle="modal" @click="SetPostComments(item)" class="tw-flex tw-items-center tw-space-x-2 text-decoration-none text-dark">
                      <div class="tw-p-2 tw-rounded-full  tw-text-black lg:tw-bg-gray-100 dark:tw-bg-gray-600">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" width="22" height="22" class="dark:text-gray-100">
                          <path fill-rule="evenodd" d="M18 5v8a2 2 0 01-2 2h-5l-5 4v-4H4a2 2 0 01-2-2V5a2 2 0 012-2h12a2 2 0 012 2zM7 8H5v2h2V8zm2 0h2v2H9V8zm6 0h-2v2h2V8z" clip-rule="evenodd" />
                        </svg>
                      </div>
                      <div class="mx-1"> ارسال نظر</div>
                    </button>

          <button data-bs-toggle="modal"
                  data-bs-target="#staticBackdrop"
                  @click="dataToModal(item.postId, item.userInfo.userId)" href="#"
                  class="tw-flex tw-items-center tw-flex-1 tw-justify-end text-decoration-none text-dark">
            <div class="tw-p-2 tw-rounded-full  tw-text-black lg:tw-bg-gray-100 dark:tw-bg-gray-600">
              <ForbiddenIcon style="width: 22px; height: 22px;"/>
              <!--              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" width="22" height="22" class="dark:text-gray-100">-->
              <!--                <path d="M15 8a3 3 0 10-2.977-2.63l-4.94 2.47a3 3 0 100 4.319l4.94 2.47a3 3 0 10.895-1.789l-4.94-2.47a3.027 3.027 0 000-.74l4.94-2.47C13.456 7.68 14.19 8 15 8z" />-->
              <!--              </svg>-->
            </div>
            <div class="mx-2"> گزارش تخلف</div>
          </button>
        </div>
        <div class="tw-flex tw-items-center py-3 tw-pt-2">
          <!--          <div class="tw-flex tw-items-center">-->
          <!--            <img class="IMG-FLUID like_user_icon" src="../assets/images/defaultUser.png" alt=""/>-->
          <!--            <img class="IMG-FLUID like_user_icon" src="../assets/images/defaultUser.png" alt=""/>-->
          <!--            <img class="IMG-FLUID like_user_icon" src="../assets/images/defaultUser.png" alt=""/>-->
          <!--          </div>-->
          <div class="dark:tw-text-gray-100" style="color:#676767;">
            پسندیده شده توسط
            <strong> ({{ item.likesCount }}) نفر </strong>
          </div>
        </div>
        <div v-if="item.comments.length>0"
             class="tw-border-t tw-py-4 tw-space-y-4 dark:tw-border-gray-600"
             style="border-top: 1px solid #e5e7eb;"
        >
          <div v-for="(comItem, index) in item.comments" :key="index" class="tw-flex">
            <div class="tw-w-10 tw-h-10 tw-rounded-full tw-relative tw-flex-shrink-0">
              <img class="IMG-FLUID tw-rounded-full custom_header_size" :src="BaseUrl + 'media/gallery/profile/' + comItem.userInfo.selfieFileData || '=../assets/images/defaultUser.png'" alt=""/>
            </div>
            <div>
              <div
                class="tw-text-gray-700 tw-py-2 tw-px-3 tw-rounded-md tw-bg-gray-100 tw-relative lg:tw-ml-5 tw-ml-2 lg:tw-mr-5">
                <p class="leading-6 mb-0">
                  {{ comItem.content }}
                </p>
                <div
                  class="tw-absolute tw-w-3 tw-h-3 tw-top-3 tw--right-1 tw-bg-gray-100 tw-transform tw-rotate-45"></div>
              </div>
            </div>
          </div>
        </div>
        <div class=" d-flex">
          <input v-model="CommentContent" @click="SetPostComments(item)"
                 style="border-radius: 50px; background-color: rgb(243 244 246); height: 40px !important;"
                 value="" type="text" class="form-control mx-1"
                 placeholder="نظر خود را وارد کنید...">
          <button style="font-weight: 600" :disabled="!CommentContent.length>0" class="mx-2" @click="addComment">
            ثبت
          </button>
        </div>


        <!--        <div class="col-12">-->
        <!--          <div-->
        <!--            class="d-flex flex-wrap align-items-center justify-content-around justify-content-md-start pt-3 iconBelowPicture border-top mt-2 pt-1 col-12"-->
        <!--          >-->
        <!--        <div class="p-2 position-relative">-->
        <!--          <font-awesome-icon icon="eye" v-tooltip="{-->
        <!--                  content:'دیدن',-->
        <!--                  delay:{show: 50, hide: 100}-->
        <!--                }"></font-awesome-icon>-->
        <!--          <span class="postViewState">1.2k</span>-->
        <!--        </div>-->
        <!--            <div-->
        <!--              class="position-relative mx-md-1"-->
        <!--              @click="likePost(item.postId, item.userId)"-->
        <!--            >-->
        <!--              <button-->
        <!--                type="button"-->
        <!--                class="btn custom-btn d-flex align-items-center justify-content-center text-decoration-none like-btn small"-->
        <!--              >-->
        <!--                لایک-->
        <!--                <span class="me-2">({{ item.likesCount }})</span>-->
        <!--                <font-awesome-icon-->
        <!--                  class="me-2"-->
        <!--                  icon="heart"-->
        <!--                  :color="item.isLikedByMe ? '#ff2631' : 'btn'"-->
        <!--                ></font-awesome-icon>-->
        <!--              </button>-->
        <!--            </div>-->
        <!--            <div class="position-relative mx-md-1">-->
        <!--              <div-->
        <!--                @click="SetPostComments(item)"-->
        <!--                href="#Comments"-->
        <!--                data-bs-toggle="modal"-->
        <!--                class=""-->
        <!--              >-->
        <!--                <button-->
        <!--                  type="button"-->
        <!--                  class="small btn custom-btn d-flex align-items-center justify-content-center text-decoration-none"-->
        <!--                >-->
        <!--                  ارسال نظر-->

        <!--                  <span class="me-2 small">({{ item.commentsCount }})</span>-->
        <!--                  <font-awesome-icon-->
        <!--                    icon="comments"-->
        <!--                    class="me-2"-->
        <!--                    :color="item.commentsCount > 0 ? '#0099ff' : ''"-->
        <!--                  ></font-awesome-icon>-->
        <!--                </button>-->
        <!--              </div>-->
        <!--            </div>-->

        <!--        <div class="p-2 mx-3 position-relative">-->
        <!--          <font-awesome-icon icon="heart-broken" color="red" v-tooltip="{-->
        <!--                  content:'نپسندیدن',-->
        <!--                  delay:{show: 50, hide: 100}-->
        <!--                }"></font-awesome-icon>-->
        <!--          <span class="postViewState text-danger">212</span>-->
        <!--        </div>-->

        <!--            <div-->
        <!--              v-if="!showDeleteInMyPost"-->
        <!--              class="position-relative mx-md-1"-->
        <!--              @click="dataToModal(item.postId, item.userInfo.userId)"-->
        <!--            >-->
        <!--              <button-->
        <!--                :disabled="item.isReportedByMe"-->
        <!--                type="button"-->
        <!--                class="btn custom-btn d-flex align-items-center justify-content-center text-decoration-none like-btn small"-->
        <!--                data-bs-toggle="modal"-->
        <!--                data-bs-target="#staticBackdrop"-->
        <!--              >-->
        <!--                گزارش تخلف-->
        <!--                <font-awesome-icon class="me-2" icon="ban" size="lg" />-->
        <!--              </button>-->
        <!--            </div>-->
        <!--          </div>-->
        <!--        </div>-->
      </div>

    </div>
  </div>
</template>

<script>
import LikeIcon from "./Icons/LikeIcon";
import ForbiddenIcon from "./Icons/ForbiddenIcon";
import MoreIcon from "./Icons/MoreIcon";
import MessageIcon from "./Icons/MessageIcon";
import ShareIcon from "./Icons/ShareIcon";

export default {
  name: "SocialPost",
  components: {ShareIcon, MessageIcon, MoreIcon, ForbiddenIcon, LikeIcon},
  data() {
    return {
      show_more: false,
      showShare: false,
      commentListTemp: [],
      showMoreCommentsText: [],
      showMoreCommentsValue: [],
      showReplyBox: [],
      replayText: "",
      comments: [],
      newCommentText: "",
      reportText: "شش",
      modalPostId: null,
      modalUserId: null,
      postUserId: null,
      showAddCommentSectionIndex: [],
      SelectedPostComments: [],
      CommentContent: "",
      SelectedPostId: 0,
      Atbottom: false,
    };
  },
  mounted() {

  },
  methods: {
    showMore(id) {
      if (this.$refs[`ShowMore${id}`][0].classList.contains('d-block')) {
        this.$refs[`ShowMore${id}`][0].classList.remove('d-block')
        this.$refs[`ShowMore${id}`][0].classList.add('d-none')
      } else {
        this.$refs[`ShowMore${id}`][0].classList.add('d-block')
        this.$refs[`ShowMore${id}`][0].classList.remove('d-none')
      }

    },
    handleScroll: function ({
                              target: {scrollTop, clientHeight, scrollHeight},
                            }) {
      if (scrollTop + clientHeight >= scrollHeight) {
        this.Atbottom = true;
      } else {
        this.Atbottom = false;
      }

    },

    SetPostComments(item) {
      this.SelectedPostComments = item.comments;
      this.SelectedPostId = item.postId;
    },
    cancelShowReply(id) {
      this.showReplyBox.splice(
        this.showReplyBox.findIndex((e) => e.id === id),
        1
      );
    },
    showReply(id) {
      this.showReplyBox.push(id);
    },

    showMoreComments(postId, commentId) {
      this.$axios
        .post(`PostComment/GetChildrenByPostId`, null, {
          params: {
            postId: postId,
            commentId: commentId,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.showMoreCommentsText.push(commentId);
            for (let i = 0; i < response.data.length; i++) {
              this.$props.postData[
                this.$props.postData.findIndex((e) => e.postId === postId)
                ].comments.unshift(response.data[i]);
            }
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    addComment() {
      this.$axios
        .post(
          `PostComment/Create`,
          {
            content: this.CommentContent,
            parentId: 0,
            postId: this.SelectedPostId,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.CommentContent = "";
            this.$toast.success("نظر شما ثبت شد");
            this.getPostData();
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getPostData() {
      this.$nextTick(() => {
        this.$nuxt.$loading.start();
      });
      this.$axios
        .post(`Post/GetFollowingPosts`, null, {})
        .then((response) => {
          if (response.status === 200) {
            this.postData = response.data;
            this.$nuxt.$loading.finish();
            this.$nuxt.loading = false;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },

    saveReply(parentId) {
      this.$axios
        .post(
          `PostComment/Create`,
          {
            postId: this.SelectedPostId,
            parentId: parentId,
            content: this.replayText,
          },
          {}
        )
        .then((response) => {
          if (response.status === 200) {
            this.replayText = "";
            this.$toast.success("نظر شما ثبت شد");
            this.getPostData();
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    likePost(postId, userId) {
      this.$axios
        .post(`PostLike/Create`, null, {
          params: {
            postId: postId,
          },
        })
        .then((response) => {
          if (response.status === 200) {
            this.$emit("updateData");
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    deleteComment(id) {
      this.$axios
        .post(`PostComment/Delete`, {id: id}, {})
        .then((response) => {
          if (response.status === 200) {
            this.$emit("updateData");
            this.$props.postData.splice(
              this.$props.postData.comments.findIndex(
                (e) => e.postCommentId === id
              ),
              1
            );
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    dataToModal(postId, userId) {
      this.modalPostId = postId;
      this.modalUserId = userId;
    },
    reportPostSubmit(postId, userId) {
      if (this.reportText.length === 0) {
        this.$toast.error("لطفا علت گزارش را بنویسید");
      } else {
        this.$axios
          .post(
            `PostReport/Create`,
            {
              postId: postId,
              userId: userId,
              reason: this.reportText,
              createDate: new Date(Date.now())
            },
            {}
          )
          .then((response) => {
            if (response.status === 200) {
              this.$toast.success("ثبت گزارش با موفقیت انجام شد.");
              document.querySelector(".closingReport").click();
              this.$emit("updateData");
            }
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
    async goToUserProfile(user){
      try {
        this.$router.push({path: `/user/${user.userName}/posts`});
      }catch (e){
        console.log(e)
      }
    },
    showAddCommentSection(postId) {
      if (this.showAddCommentSectionIndex.includes(postId)) {
        this.showAddCommentSectionIndex.splice(
          this.showAddCommentSectionIndex.indexOf(postId),
          1
        );
        this.showingAddComment(postId);
      } else {
        this.showAddCommentSectionIndex.push(postId);
      }
      this.showingAddComment(postId);
    },
    showingAddComment(data) {
      return this.showAddCommentSectionIndex.includes(data);
    },
  },
  props: {
    postData: {
      type: Array,
      required: true,
    },
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
    // showDeleteInMyPost() {
    //   return this.$nuxt.$route.fullPath === "/social/accountsetting/myposts";
    // },
    userName() {
      return this.$auth.user.baseData.name + " " + this.$auth.user.baseData.familyName;
    },
  },
};
</script>

<style scoped>
.main-inset-shadow img {
  width: 100% !important;
}

.ql-editor {
  padding-right: 0;
}

button:disabled, .disabled {
  background-color: transparent !important;
  color: black !important;
  cursor: not-allowed !important;
}

.like_user_icon {
  width: 24px;
  height: 24px;

}

.custom-btn {
  background-color: #b1dcff;
}

button:disabled,
.disabled {
  background-color: #545454;
  color: #fff;
  cursor: not-allowed !important;
}

@media only screen and (max-width: 768px) {
  .btn {
    font-size: 0.7rem !important;
  }
}

@media only screen and (min-width: 769px) {
  .btn {
    font-size: 0.8rem !important;
  }
}

.instagram-button {
  background-color: #e1306c;
}

.MainContent img {
  width: 100%;
}

.Twitter {
  transition: 0.2s ease;
}

.Twitter:hover {
  color: #5da9dd;
  transition: 0.2s ease;
}

.Instagram {
  transition: 0.2s ease;
}

.Instagram:hover {
  color: #f44d55;
  transition: 0.2s ease;
}

.AddReplyBtn {
  background-color: #088dcd;
  color: white;
  transition: 0.2s ease;
}

.AddReviewBtn {
  border: solid 1px #088dcd;
  color: #088dcd;
  transition: 0.2s ease;
}

.AddReviewBtn:hover {
  background-color: #088dcd;
  border: solid 1px #088dcd;
  color: white;
  transition: 0.2s ease;
}

.showPostContent > p {
  overflow: hidden;
}

.showPostContent img {
  width: 100%;
}

.bounce-enter-active {
  animation: bounce-in 0.5s;
}

.bounce-leave-active {
  animation: bounce-in 0.5s reverse;
}

@keyframes bounce-in {
  0% {
    transform: scale(0);
  }
  50% {
    transform: scale(1);
  }
  100% {
    transform: scale(1);
  }
}

img {
  width: 10%;
  height: auto;
}

textarea {
  resize: none;
}

.btn-close {
  position: absolute;
  left: 0;
}

.boxMainContent {
  background: #ffffff;
  /* border: 1px solid #ede9e9; */
  border-radius: 10px !important;
  display: inline-block;
  width: 100%;
  padding: 0;
}

.deleteIcon {
  left: 1rem;
  top: 1rem;
  cursor: pointer;
  width: auto;
  z-index: 101;
}

.CommentDeleteIcon {
  left: 2.5rem;
  top: 1rem;
  cursor: pointer;
  width: auto;
  z-index: 101;
}

.list-enter-active {
  animation: list 0.3s ease-out;
}

.list-leave-active {
  animation: list 0.3s ease-in reverse;
}

@keyframes list {
  from {
    opacity: 0;
    transform: translateY(-25px) scale(1);
  }
  to {
    opacity: 1;
    transform: translateY(0) scale(1);
  }
}

.moreComments {
  font-size: 12px;
  text-transform: capitalize;
  color: #088dcd;
  margin: 2px auto;
  cursor: pointer;
  width: max-content;
}

.addNewComment {
  resize: none;
  /* background: #f3f3f3 none repeat scroll 0 0; */
  border-color: transparent;
  border-radius: 3px;
  color: #000000;
  font-size: 14px;
  height: 40px;
  line-height: 16px;
  width: 100%;
  overflow: auto;
  text-align: right;
  padding: 10px;
  margin-right: 15px;
}

li {
  list-style: none;
}

.we-comment {
  border: 1px solid #ede9e9;
  display: inline-block;
  margin: 5px 10px;
  padding: 10px;
  vertical-align: top;
  position: relative;
  width: 90%;
}

.comet-avatar {
  display: inline-block;
  max-width: 7.6%;
  vertical-align: top;
}

.peoples-mesg-box > ul > li.me p::before {
  border-left: 11px solid transparent;
  border-right: initial;
  left: auto;
  right: -11px;
}

.we-comment::before {
  border-bottom: 10px solid transparent;
  border-left: 11px solid #ede9e9;
  border-top: 10px solid transparent;
  right: -11px;
  top: 10px;
  position: absolute;
  content: "";
}

.we-comment::after,
.peoples-mesg-box > ul > li p:after {
  border-bottom: 8px solid transparent;
  border-left: 9px solid #fff;
  border-top: 8px solid transparent;
  right: -9px;
  top: 12px;
  position: absolute;
  content: "";
}

.we-comet > ul li {
  /*margin-right: 55px;*/
  margin-top: 20px;
  padding-right: 0;
  list-style: none;
}

.we-comet > li ul li {
  margin-bottom: 20px;
}

.we-comet li a.showmore {
  display: table;
  font-size: 12px;
  margin: 0 auto;
  text-transform: capitalize;
}

.we-comet > li ul li:last-child {
  margin-bottom: 0;
}

.coment-head {
  display: inline-block;
  width: 100%;
}

.coment-head > h5 {
  font-size: 15px;

  padding-bottom: 5px;
  text-transform: capitalize;
  display: inline-block;
  margin-bottom: 0;
  color: #2a2a2a;
}

.coment-head > h5 > a {
  text-decoration: none;
  color: #2a2a2a;
}

.we-comment > p {
  font-size: 14px;
  line-height: 26px;
  margin-bottom: 0;
  max-width: 100%;
  margin-top: 5px;
}

.we-reply {
  text-transform: capitalize;
  font-size: 13px;
  display: inline-block;
  margin-right: 10px;
}

.coment-head > span {
  color: #999;
  font-size: 12px;
  padding-right: 10px;
  display: inline-block;
}

li.post-comment .comet-avatar {
  display: inline-block;
  max-width: 6%;
  vertical-align: top;
  width: 6%;
}

.post-comt-box {
  display: inline-block;
  padding-right: 15px;
  position: relative;
  width: 93%;
}

img {
  max-width: 100%;
  height: auto;
}

.friendEmailStatus {
  font-size: 11px;
  color: #999;
}

.myPicture {
  /*margin-bottom: 20px;*/
  max-width: 38px;
  min-width: 38px;
  max-height: 38px;
  min-height: 38px;
  vertical-align: middle;
  border-radius: 50%;
  cursor: pointer;
}

.sharingIcon {
  cursor: pointer;
  background: black;
  color: white;
  width: 25px;
  height: 25px;
  border-radius: 100%;
  line-height: 1;
}

.sharingIcon:hover {
  color: red;
}

.icon-1 {
  top: -50px;
  left: -5px;
  cursor: pointer;
  background: black;
  color: white;
  padding: 6px;
  border-radius: 100%;
  line-height: 1;
}

.icon-1 :hover {
  transform: scale(1.15);
  color: red;
}

.icon-2 {
  top: -50px;
  left: -50px;
  cursor: pointer;
  background: black;
  color: white;
  padding: 6px;
  border-radius: 100%;
  line-height: 1;
}

.icon-2:hover {
  color: red;
}

.icon-3 {
  top: 50px;
  left: -48px;
  cursor: pointer;
  background: black;
  color: white;
  padding: 6px;
  border-radius: 100%;
  line-height: 1;
}

.icon-3:hover {
  color: red;
}

.icon-4 {
  top: 0;
  right: -68px;
  cursor: pointer;
  background: black;
  color: white;
  padding: 6px;
  border-radius: 100%;
  line-height: 1;
}

.icon-4:hover {
  color: red;
}

.icon-5 {
  top: 50px;
  left: 50px;
  cursor: pointer;
  background: black;
  color: white;
  padding: 6px;
  border-radius: 100%;
  line-height: 1;
}

.icon-5:hover {
  color: red;
}

.icon-6 {
  top: 60px;
  left: 3px;
  cursor: pointer;
  background: black;
  color: white;
  padding: 6px;
  border-radius: 100%;
  line-height: 1;
}

.icon-6:hover {
  color: red;
}

.icon-7 {
  top: -50px;
  left: 48px;
  cursor: pointer;
  background: black;
  color: white;
  padding: 6px;
  border-radius: 100%;
  line-height: 1;
}

.icon-7:hover {
  color: red;
}

.icon-8 {
  top: 0;
  left: -68px;
  cursor: pointer;
  background: black;
  color: white;
  padding: 6px;
  border-radius: 100%;
  line-height: 1;
}

.icon-8:hover {
  color: red;
}

.instagram {
  top: 10px;
  left: 10px;
}

.tooltip {
  display: block !important;
  z-index: 10000;
}

.tooltip .tooltip-inner {
  background: black;
  color: white;
  border-radius: 16px;
  padding: 5px 10px 4px;
}

.tooltip .tooltip-arrow {
  width: 0;
  height: 0;
  border-style: solid;
  position: absolute;
  margin: 5px;
  border-color: yellow;
  z-index: 1;
}

.tooltip[x-placement^="top"] {
  margin-bottom: 5px;
}

.tooltip[x-placement^="top"] .tooltip-arrow {
  border-width: 5px 5px 0 5px;
  border-left-color: transparent !important;
  border-right-color: transparent !important;
  border-bottom-color: transparent !important;
  bottom: -5px;
  left: calc(50% - 5px);
  margin-top: 0;
  margin-bottom: 0;
}

.tooltip[x-placement^="bottom"] {
  margin-top: 5px;
}

.tooltip[x-placement^="bottom"] .tooltip-arrow {
  border-width: 0 5px 5px 5px;
  border-left-color: transparent !important;
  border-right-color: transparent !important;
  border-top-color: transparent !important;
  top: -5px;
  left: calc(50% - 5px);
  margin-top: 0;
  margin-bottom: 0;
}

.tooltip[x-placement^="right"] {
  margin-left: 5px;
}

.tooltip[x-placement^="right"] .tooltip-arrow {
  border-width: 5px 5px 5px 0;
  border-left-color: transparent !important;
  border-top-color: transparent !important;
  border-bottom-color: transparent !important;
  left: -5px;
  top: calc(50% - 5px);
  margin-left: 0;
  margin-right: 0;
}

.tooltip[x-placement^="left"] {
  margin-right: 5px;
}

.tooltip[x-placement^="left"] .tooltip-arrow {
  border-width: 5px 0 5px 5px;
  border-top-color: transparent !important;
  border-right-color: transparent !important;
  border-bottom-color: transparent !important;
  right: -5px;
  top: calc(50% - 5px);
  margin-left: 0;
  margin-right: 0;
}

.tooltip.popover .popover-inner {
  background: #f9f9f9;
  color: black;
  padding: 24px;
  border-radius: 5px;
  /*box-shadow: 0 5px 30px rgba(black, .1);*/
}

.tooltip.popover .popover-arrow {
  border-color: #f9f9f9;
}

.tooltip[aria-hidden="true"] {
  visibility: hidden;
  opacity: 0;
  transition: opacity 0.15s, visibility 0.15s;
}

.tooltip[aria-hidden="false"] {
  visibility: visible;
  opacity: 1;
  transition: opacity 0.15s;
}

.authorName {
  color: black;
  cursor: pointer;
}

.iconBelowPicture {
  margin-top: 10px;
}

.postViewState {
  position: absolute;
  top: 0;
  left: 0;
  color: #545454;
  font-size: 14px;
}
</style>
