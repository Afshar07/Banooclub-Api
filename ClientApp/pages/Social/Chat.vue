<template>
  <div class="container-fluid bg-white">
    <base-modal v-if="is_show_follow_modal" @close_modal="hideFollowModal">
      <template v-slot:title>
        <h5 class="text-right px-3">دنبال کنندگان من</h5>
      </template>
      <template v-slot:content>
        <div class="d-flex flex-column align-items-center bg-white custom-follower-radius">
<!--          <div class="w-100 mb-3 pt-3 pb-1 rounded-top">-->
<!--            -->
<!--          </div>-->
          <div class="overflow-auto col-12">
            <div class="SingleParentItem border-bottom p-3 col-6"
                 v-for="item in MyFollowers" :key="item.followerId" @click="ChangeActiveParent(item)"
                 :class="GetActiveParentClass(item)">
              <div class="d-flex" v-if="item.userInfo">
                <img
                  style="width: 40px; height: 40px"
                  :src="handleUserProfile(item.userInfo.selfieFileData)"
                  class="rounded-circle"
                  alt=""
                />
                <div class="d-flex align-items-center px-5">
                  <div class="d-flex flex-column justify-content-center">
                    <div class="d-flex justify-content-between align-items-center">
                      <span>{{item.userInfo.name + " " + item.userInfo.familyName}}</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <small v-if="MyFollowers.length === 0" class="text-secondary">کسی شما را دنبال نمیکند</small>
        </div>
      </template>
    </base-modal>

    <!-- Modal -->
    <group-modal id="AddGroup"></group-modal>
    <ul
      id="right-click-menu"
      tabindex="-1"
      ref="right"
      v-if="viewMenu"
      @blur="closeMenu()"
      :style="{ top: top, left: left }"
    >
      <li @click="a">Reply</li>
      <li @click="a">Forward</li>
    </ul>

    <div class="row">
      <div class="">
        <div class="row p-lg-5 pb-lg-0 ps-lg-0 pt-lg-0 p-0 position-relative main-container">
          <div v-if="isUserSelected" class="d-md-none position-fixed w-100 h-100 top-0 start-0 mx-auto bg-white p-0 m-0 custom-z-index">
            <main-chat
              ref="mobileChat"
              :BaseImgUrl="BaseImgUrl"
              :Messages="Messages"
              :SelectedParentId="SelectedParentId"
              :Uploaded="Uploaded"
              :SelectedUserData="SelectedUserData"
              @CancelUpload="CancelUpload"
              @handleScroll="handleScroll"
              @openMenu="openMenu"
              @SendMessage="setContent"
              @UploadFile="UploadFile"
              @closeChat="closeChat"
            ></main-chat>
          </div>

          <div class="col-lg-3 p-0 col-12 mb-5 bg-white" style="min-height: max-content; border-left: 1px solid #eaeaea;">
            <div class="d-flex flex-column align-items-center custom-discussions-radius">
<!--              <div class="w-100 mb-3 pt-3 px-2 d-flex justify-content-center align-items-center">-->
<!--                <p class="d-flex align-items-center ms-2">-->
<!--                  {{ $auth.user.name }}-->
<!--                  {{ $auth.user.familyName }}-->
<!--                </p>-->
<!--                <img-->
<!--                  width="20%"-->
<!--                  height="auto"-->
<!--                  class="rounded-circle"-->
<!--                  :src="userAvatar"-->
<!--                  alt=""-->
<!--                />-->
<!--              </div>-->

              <div class="w-100 px-3 d-flex flex-row justify-content-between align-items-center" style="border-bottom: 1px solid #e3e3e3;height: 77px;">
                <h2 class="text-right " style="font-family: iranSans !important; font-weight: 600;padding-right: 25px">مکالمات من</h2>
                <button @click="showFollowModal" type="button" class="btn">
                  <EditIcon style="width: 20px; height: 20px;"/>
                </button>
              </div>
              <div class="col-12 row my-3 mx-auto">
                <div class="col-12 row d-flex flex-lg-row flex-md-column flex-sm-column flex-column justify-content-between mx-auto" style="padding-right: 25px;padding-left: 0">
                  <div class="ChatSearchBox col-12 w-100 d-flex border py-2 border-1 rounded-3">
                    <input
                      v-model="SearchConversation"
                      type="search"
                      placeholder="جستجو..."
                      class="Search col-11"
                    />
                    <button class="SearchBtn ps-3 col-1 d-flex justify-content-center align-items-center">
                      <i class="fas fa-search"></i>
                    </button>
                  </div>
                </div>
              </div>

              <div class="overflow-auto col-12 border-0" style="max-height: 300px">
                <div
                  class="SingleParentItem px-4 py-1 w-100"
                  v-for="item in Parents"
                  :key="item.userId"
                  @click="ChangeActiveChat(item)"
                  :class="GetActiveChatClass(item)"
                >
                  <div class="row">
                    <div class="col-3">
                      <img
                        :src="handleUserProfileWithoutMedia(item.userPhoto)"
                        style="width: 40px; height: 40px;"
                        class="rounded-circle"
                        alt=""
                      />
                    </div>
                    <div class="col-9 d-flex flex-column px-0">
                        <div class="d-flex flex-row justify-content-between align-items-center">
                          <span class="chat_name">{{ item.userName }}</span>
                          <small class="text-muted" style="color: #666666">{{item.createDate | moment("HH:mm") }}</small>
                        </div>
                        <small class="text-muted small chat_text">{{ item.subject }} ...</small>
<!--                        <div class="d-flex justify-content-between align-items-center">-->
<!--                          <div v-if="item.unReadCount !== 0" class="UnreadCountWrapper d-flex align-items-center align-self-end justify-content-center">-->
<!--                            <small class="text-white">-->
<!--                              {{item.unReadCount }}-->
<!--                            </small>-->
<!--                          </div>-->
<!--                        </div>-->
                    </div>
                  </div>
                </div>
              </div>

              <small v-if="ParentChats.length === 0" class="text-secondary"
                >مکالمه ای برای نمایش وجود ندارد</small
              >
            </div>
          </div>
          <!-- Here -->
          <div class="col-lg-9 col-12 p-0 d-none d-md-block">
            <div class="bg-white custom-chatbox-radius">
              <div class="row col-12 position-relative me-0 custom-chatbox-radius p-0">
                <div class="chat-bg position-absolute top-0 end-0 w-100 h-100 custom-chatbox-radius"></div>
<!--                <header class="bg-white col-12 custom-z-index align-items-center" >-->
                  <div class="d-flex align-items-center" v-if="isUserSelected"
                       style="border-bottom: 1px solid #e3e3e3;height: 77px;">
<!--                    <img-->
<!--                      v-if="SelectedUserData.selfieFileData"-->
<!--                      :src="-->
<!--                        BaseUrl +-->
<!--                        'media/gallery/profile/' +-->
<!--                        SelectedUserData.selfieFileData-->
<!--                      "-->
<!--                      alt="User Avatar"-->
<!--                      width="30px "-->
<!--                      height="30px"-->
<!--                      class="rounded"-->
<!--                    />-->
                    <p class="my-auto me-3 text-capitalize">
                      {{ SelectedUserData.fullName }}
                    </p>
                  </div>
<!--                </header>-->
                <div
                  class="col-md-12"
                  ref="conversations"
                  @scroll="handleScroll($event)"
                  style="
                    overflow-y: auto;
                    /*z-index: 99999 !important;*/
                    scrollbar-width: none;
                     min-height: 100vh;
                  "
                  :style="{ height: height + 'px!important' }"
                >
                  <!-- <div v-if="Messages" class="col-md-12">
                    <div class="d-flex justify-content-center">
                      <div
                        class="d-flex justify-content-center align-items-center text-muted p-3 rounded-circle"
                      >
                        <i class="fas fa-plus"></i>
                      </div>
                    </div>
                  </div> -->

                  <div
                    class="my-2 p-2"
                    :class="{ SingleMessage: item.MessageId === 0 }"
                    v-for="item in Messages"
                    :key="item.MessageId"
                  >
                    <div
                      class="row d-flex align-items-center"
                      :class="{
                        'd-flex flex-row-reverse':
                          item.CreatorUserId !== $auth.user.userId,
                      }"
                    >
                      <!-- @click.right.prevent="openMenu($event, item)" -->
                      <div
                        class="col-md-1 d-flex align-items-md-center flex-column"
                        :class="
                          item.CreatorUserId === $auth.user.userId
                            ? 'align-items-start'
                            : 'align-items-end'
                        "
                      >
                        <img
                          v-if="item.CreatorUserId === $auth.user.userId"
                          :src="BaseUrl + $store.state.HeaderData.selfie"
                          style="width: 50px; height: 50px;"
                          class="rounded-circle"
                          alt=""
                        />
                        <img
                          v-else
                          :src="
                            BaseUrl +
                            'media/gallery/profile/' +
                            SelectedUserData.selfieFileData
                          "
                          style="width: 50px; height: 50px;"
                          class="rounded-circle"
                          alt=""
                        />

                      </div>
                      <div
                        class="chat-width"
                        :class="{
                          'd-flex justify-content-end':
                            item.CreatorUserId != $auth.user.userId,
                        }"
                      >
                        <div
                          class="MessageBox chat-shadow chat-width pt-3 pe-3 ps-2"
                          :class="{
                            'MessageBoxOther chat-your-message':
                              item.CreatorUserId !== $auth.user.userId,
                          }"
                        >
                          <div v-if="item.IsForwarded" class="Forwarded">
                            <small>
                              <i class="text-muted">Forwarded Message</i>
                            </small>
                          </div>

                          <div
                            v-if="
                              item.ParentMessageId && item.ParentMessageId != 0
                            "
                            class="Replay my-3"
                          ></div>
                          <div
                            class="w-100 wrapping"
                            :class="
                              item.CreatorUserId !== $auth.user.userId
                                ? 'chat-your-message'
                                : 'chat-my-message'
                            "
                          >
                            <p class="wrapping mb-1">
                              {{ item.MessageBody }}
                            </p>
                          </div>

                          <!-- <p
                            class="wrapping"
                            :class="{
                              'text-secondary d-flex flex-row-reverse':
                                item.CreatorUserId !== $auth.user.userId,
                            }"
                          ></p> -->
                          <!--                        <img :src="item.img" class="img-fluid rounded" alt="">-->
                          <div class="d-flex align-items-center" v-if="item.CreatorUserId === $auth.user.userId">
                            <i v-if=" item.sent && !item.IsRead && !item.IsDelivered " class="fas fa-check" style="font-size: 8px"></i>
                            <i v-if="item.IsDelivered == true" class="fas fa-check-double" :class="  item.IsRead ? 'readed-color' : 'chat-my-message' "
                               style="font-size: 8px"></i>
                            <i v-if="!item.sent" class="far fa-circle" style="font-size: 8px"></i>
                            <small class="px-2">
                              {{item.CreateDate | moment("HH:mm") }}
                            </small>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div
                  v-if="SelectedParentId"
                  class="col-md-12 SendMessage custom-chat-footer-radius bg-white custom-z-index"
                >
                  <div class="row">
                    <!--                  <div class="col-md-1 d-flex align-items-start pt-lg-1 pt-0 justify-content-lg-center">-->

                    <!--                      <div class="d-flex justify-content-center align-items-center SendBtn" style="cursor: pointer" >-->
                    <!--                        <i class="fas fa-paper-plane text-white"></i>-->
                    <!--                      </div>-->

                    <!--                  </div>-->
                    <div class="col-lg-12 col-12 py-4" dir="auto" style="border-top: 1px solid #e3e3e3;">
                      <div class=" d-flex align-items-center justify-content-between col-12">
                        <button
                          type="button"
                          class="send-button-color btn border rounded-3 ms-2 chat-actions-height"
                          @click="SendMessage"
                        >
<!--                          <i-->
<!--                            class="fas fa-paper-plane ms-1"-->
<!--                            style="font-size: 16px"-->
<!--                          ></i>-->
                          ارسال
                        </button>
                        <input
                          class="col-9 border p-2 chat-actions-height border-0 overflow-scroll"
                          placeholder="پیام شما"
                          type="text"
                          name="content"
                          v-model="content"
                          @keydown.enter="SendMessage"
                          dir="auto"
                        />
                        <button
                          class="btn me-1 col-1 chat-actions-height"
                          @click="ToggleEmojis"
                        >
                          <i
                            class="far fa-smile-beam"
                            style="font-size: 20px"
                          ></i>
                        </button>
                      </div>
                    </div>

                    <div class="col-md-12" v-if="Uploaded">
                      <div class="row pe-4">
                        <div class="col-md-4">
                          <span
                            class="badge bg-danger text-white"
                            style="cursor: pointer"
                            @click="CancelUpload()"
                          >
                            <i class="fas fa-minus"></i
                            ></span>
                          <img
                            :src="BaseImgUrl"
                            class="img-fluid rounded"
                            alt=""
                          />
                        </div>
                        <div class="col-md-8">
                          <p>{{ content }}</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="d-flex justify-content-end" ref="Emoji">
                  <client-only>
                    <Picker
                      :data="emojiIndex"
                      class="Emoji-Container"
                      v-show="SelectingEmoji"
                      @select="showEmoji"
                    />
                  </client-only>
                </div>
              </div>
            </div>
          </div>

          <!--          <div class="col-lg-9 col-12 p-0 d-none d-md-block">-->
<!--            <div class="d-flex flex-column">-->
<!--              <div v-if="isUserSelected" class="w-100 px-3 d-flex flex-row" style="height: 77px;border-bottom: 1px solid #e3e3e3">-->
<!--                <h4 class="my-auto me-3 text-capitalize chat_name">-->
<!--                  {{ SelectedUserData.fullName }}-->
<!--                </h4>-->
<!--              </div>-->
<!--              <div-->
<!--                  class="my-2 p-2"-->
<!--                  :class="{ SingleMessage: item.MessageId === 0 }"-->
<!--                  v-for="item in Messages"-->
<!--                  :key="item.MessageId"zz-->
<!--                >-->
<!--                  <div-->
<!--                    class="row d-flex align-items-center"-->
<!--                    :class="{-->
<!--                        'd-flex flex-row-reverse':-->
<!--                          item.CreatorUserId !== $auth.user.userId,-->
<!--                      }"-->
<!--                  >-->
<!--                    &lt;!&ndash; @click.right.prevent="openMenu($event, item)" &ndash;&gt;-->
<!--                    <div-->
<!--                      class="col-md-1 d-flex align-items-md-start flex-column"-->
<!--                      :class="-->
<!--                          item.CreatorUserId === $auth.user.userId-->
<!--                            ? 'align-items-start'-->
<!--                            : 'align-items-end'-->
<!--                        "-->
<!--                    >-->
<!--                      <img-->
<!--                        v-if="item.CreatorUserId === $auth.user.userId"-->
<!--                        :src="BaseUrl + $store.state.HeaderData.selfie"-->
<!--                        style="width: 50px; height: 50px;"-->
<!--                        class="rounded-circle"-->
<!--                        alt=""-->
<!--                      />-->
<!--                      <img-->
<!--                        v-else-->
<!--                        style="width: 50px; height: 50px;"-->

<!--                        :src="-->
<!--                            BaseUrl +-->
<!--                            'media/gallery/profile/' +-->
<!--                            SelectedUserData.selfieFileData-->
<!--                          "-->
<!--                        class="rounded-circle"-->
<!--                        alt=""-->
<!--                      />-->
<!--                    </div>-->
<!--                    <div-->
<!--                      class="chat-width"-->
<!--                      :class="{-->
<!--                          'd-flex justify-content-end':-->
<!--                            item.CreatorUserId != $auth.user.userId,-->
<!--                        }"-->
<!--                    >-->
<!--                      <div-->
<!--                        class="MessageBox chat-shadow chat-width pt-3 pe-3 ps-2"-->
<!--                        :class="{-->
<!--                            'MessageBoxOther chat-your-message':-->
<!--                              item.CreatorUserId !== $auth.user.userId,-->
<!--                          }"-->
<!--                      >-->
<!--                        <div v-if="item.IsForwarded" class="Forwarded">-->
<!--                          <small>-->
<!--                            <i class="text-muted">Forwarded Message</i></small-->
<!--                          >-->
<!--                        </div>-->

<!--                        <div-->
<!--                          v-if="-->
<!--                              item.ParentMessageId && item.ParentMessageId != 0-->
<!--                            "-->
<!--                          class="Replay my-3"-->
<!--                        ></div>-->
<!--                        <div-->
<!--                          class="w-100 wrapping"-->
<!--                          :class="-->
<!--                              item.CreatorUserId !== $auth.user.userId-->
<!--                                ? 'chat-your-message'-->
<!--                                : 'chat-my-message'-->
<!--                            "-->
<!--                        >-->
<!--                          <p class="wrapping">-->
<!--                            {{ item.MessageBody }}-->
<!--                          </p>-->
<!--                        </div>-->

<!--                        &lt;!&ndash; <p-->
<!--                          class="wrapping"-->
<!--                          :class="{-->
<!--                            'text-secondary d-flex flex-row-reverse':-->
<!--                              item.CreatorUserId !== $auth.user.userId,-->
<!--                          }"-->
<!--                        ></p> &ndash;&gt;-->
<!--                        &lt;!&ndash;                        <img :src="item.img" class="img-fluid rounded" alt="">&ndash;&gt;-->
<!--                        <div v-if="item.CreatorUserId === $auth.user.userId" class="d-flex justify-content-between align-items-center">-->
<!--                          <i v-if="item.sent && !item.IsRead && !item.IsDelivered" class="fas fa-check" style="font-size: 8px"></i>-->
<!--                          <i v-if="item.IsDelivered == true" class="fas fa-check-double" :class=" item.IsRead ? 'readed-color' : 'chat-my-message' " style="font-size: 8px"></i>-->
<!--                          <i v-if="!item.sent" class="far fa-circle" style="font-size: 8px"></i>-->
<!--                          <small class="mt-2">-->
<!--                            {{item.CreateDate | moment("HH:mm")}}-->
<!--                          </small>-->
<!--                        </div>-->
<!--                      </div>-->
<!--                    </div>-->
<!--                  </div>-->
<!--                </div>-->
<!--              <div class="d-flex">-->
<!--                <div v-if="SelectedParentId" class="col-md-12 py-3 SendMessage custom-chat-footer-radius bg-white custom-z-index">-->
<!--                  <div class="row">-->
<!--&lt;!&ndash;                    <div class="col-md-1 d-flex align-items-start pt-lg-1 pt-0 justify-content-lg-center">&ndash;&gt;-->

<!--&lt;!&ndash;                      <div class="d-flex justify-content-center align-items-center SendBtn" style="cursor: pointer" >&ndash;&gt;-->
<!--&lt;!&ndash;                        <i class="fas fa-paper-plane text-white"></i>&ndash;&gt;-->
<!--&lt;!&ndash;                      </div>&ndash;&gt;-->

<!--&lt;!&ndash;                    </div>&ndash;&gt;-->
<!--                    <div class="col-lg-12 col-12" dir="auto">-->
<!--                      <div class="my-auto d-flex align-items-center justify-content-between col-12">-->
<!--                        <button-->
<!--                          type="button"-->
<!--                          class="send-button-color btn border rounded-3 ms-2 chat-actions-height"-->
<!--                          @click="SendMessage"-->
<!--                        >-->
<!--                          <i-->
<!--                            class="fas fa-paper-plane ms-1"-->
<!--                            style="font-size: 16px"-->
<!--                          ></i>-->
<!--                          ارسال پیام-->
<!--                        </button>-->
<!--                        <input-->
<!--                          class="col-9 border rounded-2 p-2 chat-actions-height"-->
<!--                          type="text"-->
<!--                          name="content"-->
<!--                          v-model="content"-->
<!--                          @keydown.enter="SendMessage"-->
<!--                          dir="auto"-->
<!--                        />-->
<!--                        <button-->
<!--                          class="btn me-1 col-1 chat-actions-height"-->
<!--                          @click="ToggleEmojis"-->
<!--                        >-->
<!--                          <i-->
<!--                            class="far fa-smile-beam"-->
<!--                            style="font-size: 20px"-->
<!--                          ></i>-->
<!--                        </button>-->
<!--                      </div>-->
<!--                    </div>-->
<!--                    <div class="col-md-12" v-if="Uploaded">-->
<!--                      <div class="row pe-4">-->
<!--                        <div class="col-md-4">-->
<!--                          <span-->
<!--                            class="badge bg-danger text-white"-->
<!--                            style="cursor: pointer"-->
<!--                            @click="CancelUpload()"-->
<!--                          >-->
<!--                            <i class="fas fa-minus"></i-->
<!--                            ></span>-->
<!--                          <img-->
<!--                            :src="BaseImgUrl"-->
<!--                            class="img-fluid rounded"-->
<!--                            alt=""-->
<!--                          />-->
<!--                        </div>-->
<!--                        <div class="col-md-8">-->
<!--                          <p>{{ content }}</p>-->
<!--                        </div>-->
<!--                      </div>-->
<!--                    </div>-->
<!--                  </div>-->
<!--                </div>-->

<!--              </div>-->
<!--            </div>-->
<!--            <div class="bg-white custom-chatbox-radius">-->
<!--              <div class="row col-12 position-relative me-0 custom-chatbox-radius">-->
<!--                <div class="chat-bg position-absolute top-0 end-0 w-100 h-100 custom-chatbox-radius px-0"></div>-->
<!--&lt;!&ndash;                <header class="bg-white col-12 custom-z-index py-3" v-if="isUserSelected">&ndash;&gt;-->
<!--&lt;!&ndash;                  <div class="d-flex align-items-center">&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                    <img&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                      v-if="SelectedUserData.selfieFileData"&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                      :src="&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                        BaseUrl +&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                        'media/gallery/profile/' +&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                        SelectedUserData.selfieFileData&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                      "&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                      alt="User Avatar"&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                      width="30px "&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                      height="30px"&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                      class="rounded"&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;&lt;!&ndash;                    />&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;                    <h4 class="my-auto me-3 text-capitalize chat_name">&ndash;&gt;-->
<!--&lt;!&ndash;                      {{ SelectedUserData.fullName }}&ndash;&gt;-->
<!--&lt;!&ndash;                    </h4>&ndash;&gt;-->
<!--&lt;!&ndash;                  </div>&ndash;&gt;-->
<!--&lt;!&ndash;                </header>&ndash;&gt;-->
<!--                <div class="col-md-12" ref="conversations" @scroll="handleScroll($event)" style="overflow-y: auto; z-index: 99999 !important;  scrollbar-width: none;  min-height: 77vh;   " :style="{ height: height + 'px!important' }">-->
<!--                  &lt;!&ndash; <div v-if="Messages" class="col-md-12">-->
<!--                    <div class="d-flex justify-content-center">-->
<!--                      <div-->
<!--                        class="d-flex justify-content-center align-items-center text-muted p-3 rounded-circle"-->
<!--                      >-->
<!--                        <i class="fas fa-plus"></i>-->
<!--                      </div>-->
<!--                    </div>-->
<!--                  </div> &ndash;&gt;-->

<!--&lt;!&ndash;                  <div&ndash;&gt;-->
<!--&lt;!&ndash;                    class="my-2 p-2"&ndash;&gt;-->
<!--&lt;!&ndash;                    :class="{ SingleMessage: item.MessageId === 0 }"&ndash;&gt;-->
<!--&lt;!&ndash;                    v-for="item in Messages"&ndash;&gt;-->
<!--&lt;!&ndash;                    :key="item.MessageId"&ndash;&gt;-->
<!--&lt;!&ndash;                  >&ndash;&gt;-->
<!--&lt;!&ndash;                    <div&ndash;&gt;-->
<!--&lt;!&ndash;                      class="row d-flex align-items-center"&ndash;&gt;-->
<!--&lt;!&ndash;                      :class="{&ndash;&gt;-->
<!--&lt;!&ndash;                        'd-flex flex-row-reverse':&ndash;&gt;-->
<!--&lt;!&ndash;                          item.CreatorUserId !== $auth.user.userId,&ndash;&gt;-->
<!--&lt;!&ndash;                      }"&ndash;&gt;-->
<!--&lt;!&ndash;                    >&ndash;&gt;-->
<!--&lt;!&ndash;                      &lt;!&ndash; @click.right.prevent="openMenu($event, item)" &ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;                      <div&ndash;&gt;-->
<!--&lt;!&ndash;                        class="col-md-1 d-flex align-items-md-start flex-column"&ndash;&gt;-->
<!--&lt;!&ndash;                        :class="&ndash;&gt;-->
<!--&lt;!&ndash;                          item.CreatorUserId === $auth.user.userId&ndash;&gt;-->
<!--&lt;!&ndash;                            ? 'align-items-start'&ndash;&gt;-->
<!--&lt;!&ndash;                            : 'align-items-end'&ndash;&gt;-->
<!--&lt;!&ndash;                        "&ndash;&gt;-->
<!--&lt;!&ndash;                      >&ndash;&gt;-->
<!--&lt;!&ndash;                        <img&ndash;&gt;-->
<!--&lt;!&ndash;                          v-if="item.CreatorUserId === $auth.user.userId"&ndash;&gt;-->
<!--&lt;!&ndash;                          :src="BaseUrl + $store.state.HeaderData.selfie"&ndash;&gt;-->
<!--&lt;!&ndash;                          style="width: 50px; height: 50px;"&ndash;&gt;-->
<!--&lt;!&ndash;                          class="rounded-circle"&ndash;&gt;-->
<!--&lt;!&ndash;                          alt=""&ndash;&gt;-->
<!--&lt;!&ndash;                        />&ndash;&gt;-->
<!--&lt;!&ndash;                        <img&ndash;&gt;-->
<!--&lt;!&ndash;                          v-else&ndash;&gt;-->
<!--&lt;!&ndash;                          style="width: 50px; height: 50px;"&ndash;&gt;-->

<!--&lt;!&ndash;                          :src="&ndash;&gt;-->
<!--&lt;!&ndash;                            BaseUrl +&ndash;&gt;-->
<!--&lt;!&ndash;                            'media/gallery/profile/' +&ndash;&gt;-->
<!--&lt;!&ndash;                            SelectedUserData.selfieFileData&ndash;&gt;-->
<!--&lt;!&ndash;                          "&ndash;&gt;-->
<!--&lt;!&ndash;                          class="rounded-circle"&ndash;&gt;-->
<!--&lt;!&ndash;                          alt=""&ndash;&gt;-->
<!--&lt;!&ndash;                        />&ndash;&gt;-->
<!--&lt;!&ndash;                      </div>&ndash;&gt;-->
<!--&lt;!&ndash;                      <div&ndash;&gt;-->
<!--&lt;!&ndash;                        class="chat-width"&ndash;&gt;-->
<!--&lt;!&ndash;                        :class="{&ndash;&gt;-->
<!--&lt;!&ndash;                          'd-flex justify-content-end':&ndash;&gt;-->
<!--&lt;!&ndash;                            item.CreatorUserId != $auth.user.userId,&ndash;&gt;-->
<!--&lt;!&ndash;                        }"&ndash;&gt;-->
<!--&lt;!&ndash;                      >&ndash;&gt;-->
<!--&lt;!&ndash;                        <div&ndash;&gt;-->
<!--&lt;!&ndash;                          class="MessageBox chat-shadow chat-width pt-3 pe-3 ps-2"&ndash;&gt;-->
<!--&lt;!&ndash;                          :class="{&ndash;&gt;-->
<!--&lt;!&ndash;                            'MessageBoxOther chat-your-message':&ndash;&gt;-->
<!--&lt;!&ndash;                              item.CreatorUserId !== $auth.user.userId,&ndash;&gt;-->
<!--&lt;!&ndash;                          }"&ndash;&gt;-->
<!--&lt;!&ndash;                        >&ndash;&gt;-->
<!--&lt;!&ndash;                          <div v-if="item.IsForwarded" class="Forwarded">&ndash;&gt;-->
<!--&lt;!&ndash;                            <small>&ndash;&gt;-->
<!--&lt;!&ndash;                              <i class="text-muted">Forwarded Message</i></small&ndash;&gt;-->
<!--&lt;!&ndash;                            >&ndash;&gt;-->
<!--&lt;!&ndash;                          </div>&ndash;&gt;-->

<!--&lt;!&ndash;                          <div&ndash;&gt;-->
<!--&lt;!&ndash;                            v-if="&ndash;&gt;-->
<!--&lt;!&ndash;                              item.ParentMessageId && item.ParentMessageId != 0&ndash;&gt;-->
<!--&lt;!&ndash;                            "&ndash;&gt;-->
<!--&lt;!&ndash;                            class="Replay my-3"&ndash;&gt;-->
<!--&lt;!&ndash;                          ></div>&ndash;&gt;-->
<!--&lt;!&ndash;                          <div&ndash;&gt;-->
<!--&lt;!&ndash;                            class="w-100 wrapping"&ndash;&gt;-->
<!--&lt;!&ndash;                            :class="&ndash;&gt;-->
<!--&lt;!&ndash;                              item.CreatorUserId !== $auth.user.userId&ndash;&gt;-->
<!--&lt;!&ndash;                                ? 'chat-your-message'&ndash;&gt;-->
<!--&lt;!&ndash;                                : 'chat-my-message'&ndash;&gt;-->
<!--&lt;!&ndash;                            "&ndash;&gt;-->
<!--&lt;!&ndash;                          >&ndash;&gt;-->
<!--&lt;!&ndash;                            <p class="wrapping">&ndash;&gt;-->
<!--&lt;!&ndash;                              {{ item.MessageBody }}&ndash;&gt;-->
<!--&lt;!&ndash;                            </p>&ndash;&gt;-->
<!--&lt;!&ndash;                          </div>&ndash;&gt;-->

<!--&lt;!&ndash;                          &lt;!&ndash; <p&ndash;&gt;-->
<!--&lt;!&ndash;                            class="wrapping"&ndash;&gt;-->
<!--&lt;!&ndash;                            :class="{&ndash;&gt;-->
<!--&lt;!&ndash;                              'text-secondary d-flex flex-row-reverse':&ndash;&gt;-->
<!--&lt;!&ndash;                                item.CreatorUserId !== $auth.user.userId,&ndash;&gt;-->
<!--&lt;!&ndash;                            }"&ndash;&gt;-->
<!--&lt;!&ndash;                          ></p> &ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;                          &lt;!&ndash;                        <img :src="item.img" class="img-fluid rounded" alt="">&ndash;&gt;&ndash;&gt;-->
<!--&lt;!&ndash;                          <div v-if="item.CreatorUserId === $auth.user.userId" class="d-flex justify-content-between align-items-center">&ndash;&gt;-->
<!--&lt;!&ndash;                            <i v-if="item.sent && !item.IsRead && !item.IsDelivered" class="fas fa-check" style="font-size: 8px"></i>&ndash;&gt;-->
<!--&lt;!&ndash;                            <i v-if="item.IsDelivered == true" class="fas fa-check-double" :class=" item.IsRead ? 'readed-color' : 'chat-my-message' " style="font-size: 8px"></i>&ndash;&gt;-->
<!--&lt;!&ndash;                            <i v-if="!item.sent" class="far fa-circle" style="font-size: 8px"></i>&ndash;&gt;-->
<!--&lt;!&ndash;                            <small class="mt-2">&ndash;&gt;-->
<!--&lt;!&ndash;                              {{item.CreateDate | moment("HH:mm")}}&ndash;&gt;-->
<!--&lt;!&ndash;                            </small>&ndash;&gt;-->
<!--&lt;!&ndash;                          </div>&ndash;&gt;-->
<!--&lt;!&ndash;                        </div>&ndash;&gt;-->
<!--&lt;!&ndash;                      </div>&ndash;&gt;-->
<!--&lt;!&ndash;                    </div>&ndash;&gt;-->
<!--&lt;!&ndash;                  </div>&ndash;&gt;-->
<!--                </div>-->

<!--                <div v-if="SelectedParentId" class="col-md-12 py-3 SendMessage custom-chat-footer-radius bg-white custom-z-index">-->
<!--                  <div class="row">-->
<!--                                      <div class="col-md-1 d-flex align-items-start pt-lg-1 pt-0 justify-content-lg-center">-->

<!--                                          <div class="d-flex justify-content-center align-items-center SendBtn" style="cursor: pointer" >-->
<!--                                            <i class="fas fa-paper-plane text-white"></i>-->
<!--                                          </div>-->

<!--                                      </div>-->
<!--                    <div class="col-lg-12 col-12" dir="auto">-->
<!--                      <div class="my-auto d-flex align-items-center justify-content-between col-12">-->
<!--                        <button-->
<!--                          type="button"-->
<!--                          class="send-button-color btn border rounded-3 ms-2 chat-actions-height"-->
<!--                          @click="SendMessage"-->
<!--                        >-->
<!--                          <i-->
<!--                            class="fas fa-paper-plane ms-1"-->
<!--                            style="font-size: 16px"-->
<!--                          ></i>-->
<!--                          ارسال پیام-->
<!--                        </button>-->
<!--                        <input-->
<!--                          class="col-9 border rounded-2 p-2 chat-actions-height"-->
<!--                          type="text"-->
<!--                          name="content"-->
<!--                          v-model="content"-->
<!--                          @keydown.enter="SendMessage"-->
<!--                          dir="auto"-->
<!--                        />-->
<!--                        <button-->
<!--                          class="btn me-1 col-1 chat-actions-height"-->
<!--                          @click="ToggleEmojis"-->
<!--                        >-->
<!--                          <i-->
<!--                            class="far fa-smile-beam"-->
<!--                            style="font-size: 20px"-->
<!--                          ></i>-->
<!--                        </button>-->
<!--                      </div>-->
<!--                    </div>-->
<!--                    <div class="col-md-12" v-if="Uploaded">-->
<!--                      <div class="row pe-4">-->
<!--                        <div class="col-md-4">-->
<!--                          <span-->
<!--                            class="badge bg-danger text-white"-->
<!--                            style="cursor: pointer"-->
<!--                            @click="CancelUpload()"-->
<!--                          >-->
<!--                            <i class="fas fa-minus"></i-->
<!--                          ></span>-->
<!--                          <img-->
<!--                            :src="BaseImgUrl"-->
<!--                            class="img-fluid rounded"-->
<!--                            alt=""-->
<!--                          />-->
<!--                        </div>-->
<!--                        <div class="col-md-8">-->
<!--                          <p>{{ content }}</p>-->
<!--                        </div>-->
<!--                      </div>-->
<!--                    </div>-->
<!--                  </div>-->
<!--                </div>-->
<!--                <div class="d-flex justify-content-end" ref="Emoji">-->
<!--                  <client-only>-->
<!--                    <Picker-->
<!--                      :data="emojiIndex"-->
<!--                      class="Emoji-Container"-->
<!--                      v-show="SelectingEmoji"-->
<!--                      @select="showEmoji"-->
<!--                    />-->
<!--                  </client-only>-->
<!--                </div>-->
<!--              </div>-->
<!--            </div>-->
<!--          </div>-->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import GroupModal from "../../components/Chat/GroupModal.vue";
import MainChat from "../../components/Chat/MainChat.vue";
import data from "emoji-mart-vue-fast/data/all.json";
import "emoji-mart-vue-fast/css/emoji-mart.css";
import EditIcon from "../../components/Icons/EditIcon";
import { Picker, EmojiIndex } from "emoji-mart-vue-fast";
let emojiIndex = new EmojiIndex(data);
import { mapGetters } from "vuex";
import BaseModal from "../../components/utilities/BaseModal";
export default {
  name: "Chat",
  layout: "PoshtebamPlusLayout",
  components: {BaseModal, GroupModal, MainChat, Picker, EditIcon },
  fetchOnServer() {
    return false;
  },
  async fetch() {
    const response = await this.$repositories.getMyFollowers.getMyFollowers();
    this.MyFollowers = response.data;
    await this.GetParentConversations();
  },
  data() {
    return {
      is_show_follow_modal:false,
      emojiIndex: emojiIndex,
      isUserSelected: false,
      SearchConversation: "",
      viewMenu: false,
      SelectingEmoji: false,
      SelectedParentId: null,
      content: "",
      BaseImgUrl: "",
      image: "",
      Uploaded: false,
      SelectedItem: "",
      Messages: [],
      ParentChats: [],
      SelectedUserData: {
        userId: 0,
        fullName: "",
        selfieFileData: "",
      },
      id: 0,
      top: 0,
      left: 0,
      hasScrolledToTop: false,
      height: 650,
      MyFollowers: [],
      SelectedChatId: 0,
      timer: null,
      Atbottom: true,
    };
  },

  computed: {
    userAvatar() {
      return this.loggedInfoData?.selfie
        ? this.BaseUrl + this.loggedInfoData.selfie
        : "/defaultUser.png";
    },
    Parents() {
      const data = this.ParentChats;
      if (this.SearchConversation === "") {
        return data;
      } else {
        return data.filter((el) => {
          return el.userName.match(this.SearchConversation);
        });
      }
    },
    ...mapGetters(["SocketId"]),

    BaseUrl() {
      return process.env.pic;
    },
  },

  watch: {
    SocketId: {
      immediate: true,
      async handler(val) {
        if (val != this.$auth.user.userId) {
          await this.GetParentConversations();
          if (this.SelectedParentId) {
            await this.GetConversations(0, 0);
          }
          this.$store.commit("SetDefaultSocketId");
        }
      },
    },

    // Atbottom: function (val, oldVal) {
    //   if (val) {
    //     this.GetConversations(0, 0);
    //   }
    // },

    hasScrolledToTop: function (val, oldVal) {
      if (val) {
        if (this.Messages.length > 0) {
          const fIdx = this.Messages[0].MessageId;
          this.GetConversations(0, fIdx);
        }
      }
    },
  },

  methods: {
    showFollowModal(){
      this.is_show_follow_modal = true;
    },
    hideFollowModal(){
      this.is_show_follow_modal = false;
    },
    async setContent(content) {
      this.content = content;
      await this.SendMessage();
      this.$refs.mobileChat.scrollToBottom();
    },
    closeChat() {
      this.isUserSelected = false;
      this.SelectedParentId = null;
    },
    handleUserProfileWithoutMedia(userPhoto) {
      if (userPhoto) {
        return `${process.env.pic}media/gallery/profile/${userPhoto}`;
      } else {
        return "/defaultUser.png";
      }
    },
    handleUserProfile(userPhoto) {
      if (userPhoto) {
        return `${process.env.pic}${userPhoto}`;
      } else {
        return "/defaultUser.png";
      }
    },
    //Upload Pic
    CancelUpload() {
      this.image = "";
      this.BaseImgUrl = "";
      this.Uploaded = false;
    },
    a() {},
    UploadFile(file) {
      let f;
      if (file) {
        f = file;
      } else {
        f = this.$refs.ChatFile.files[0];
      }
      const that = this;

      that.Uploaded = true;

      this.BaseImgUrl = URL.createObjectURL(f);
      const reader = new FileReader();

      reader.onload = (function (theFile) {
        return function () {
          const binaryData = reader.result;
          that.image = window.btoa(binaryData);
        };
      })(f);
      reader.readAsBinaryString(f);
    },
    OpenFile() {
      this.$refs.ChatFile.click();
    },
    //Right Click Menu
    closeMenu() {
      this.viewMenu = false;
      this.SelectedItem = "";
    },
    setMenu(top, left) {
      let largestHeight =
        window.innerHeight - this.$refs.right.offsetHeight - 25;
      let largestWidth = window.innerWidth - this.$refs.right.offsetWidth - 25;

      if (top > largestHeight) top = largestHeight;

      if (left > largestWidth) left = largestWidth;

      this.top = top + "px";
      this.left = left + "px";
    },
    openMenu(e, item) {
      this.SelectedItem = item;
      this.viewMenu = true;
      this.$nextTick(
        function () {
          this.$refs.right.focus();

          this.setMenu(e.y, e.x);
        }.bind(this)
      );
      e.preventDefault();
    },

    //Emoji
    ToggleEmojis() {
      this.SelectingEmoji = !this.SelectingEmoji;
    },
    showEmoji(emoji) {
      this.content += emoji.native;
    },
    //For New Chat
    ChangeActiveParent(item) {
      if (item.followerUserId) {
        this.SelectedParentId = item.followerUserId;
        this.SelectedChatId = item.followerUserId;
      } else {
        this.SelectedParentId = item.userId;
        this.SelectedChatId = item.userId;
      }
      this.Messages = [];
      this.SelectedUserData.userId = item.userInfo.userId;
      this.SelectedUserData.fullName = item.userInfo.name.concat(
        " ",
        item.userInfo.familyName
      );
      // this.SelectedUserData.selfieFileData =
      // item.userInfo.selfieFileData.replace("media/gallery/Profile/", "");
      this.GetConversations(0, 0);
    },
    GetActiveParentClass(item) {
      if (item) {
        if (item.followerUserId === this.SelectedParentId) {
          return "SingleParentItemActive";
        } else {
          return "";
        }
      }
    },
    //For Previous Chat
    async ChangeActiveChat(item) {
      this.SelectedParentId = item.userId;
      this.SelectedChatId = item.userId;
      this.Messages = [];
      this.SelectedUserData.userId = item.userId;
      this.SelectedUserData.fullName = item.userName;
      this.SelectedUserData.selfieFileData = item.userPhoto;
      await this.GetConversations(0, 0);
      this.isUserSelected = true;
      this.scrollToBottom();
    },
    GetActiveChatClass(item) {
      if (item) {
        if (item.userId === this.SelectedParentId) {
          return "SingleParentItemActive";
        } else {
          return "";
        }
      }
    },
    //Functions
    scrollToBottom() {
      const container = this.$refs.conversations;
      container.scrollTop = container.scrollHeight;
    },
    async IsDeliverMessages() {
      try {
        await this.$repositories.deliverMessage.deliverMessage();
        this.scrollToBottom();
      } catch (error) {
        console.log(error);
      }
    },
    async IsReadMessages() {
      try {
        await this.$repositories.isMessageReaded.isMessageReaded(
          this.SelectedChatId
        );
      } catch (error) {
        console.log(error);
      }
    },
    handleScroll: function ({
      target: { scrollTop, clientHeight, scrollHeight },
    })
    {
      if (scrollTop + clientHeight >= scrollHeight) {
        this.Atbottom = true;
      } else {
        this.Atbottom = false;
      }

      if (scrollTop === 0) {
        this.hasScrolledToTop = true;
      } else {
        this.hasScrolledToTop = false;
      }
    },
    //Main Methods
    async GetParentConversations() {
      try {
        const response = await this.$repositories.getMenu.getMenu();
        this.ParentChats = response.data;
        await this.IsDeliverMessages();
        this.scrollToBottom();
      } catch (error) {
        console.log(error);
      }
    },
    async SendMessage() {
      if (this.content) {
        let localMessage = {
          MessageId: 0,
          IsDeleted: false,
          Subject:
            this.content.length > 40
              ? this.content.substr(0, 40)
              : this.content,
          MessageBody: this.content,
          ParentMessageId: 0,
          isForwarded: false,
          CreateDate: new Date(),
          userId: 0,
          sent: false,
          CreatorUserId: this.$auth.user.userId,
        };

        this.Messages.push(localMessage);

        this.SelectingEmoji = false;
        const response = await this.$repositories.sendMessage.sendMessage({
          messageId: 0,
          isDeleted: false,
          subject:
            this.content.length > 20
              ? this.content.substr(0, 20)
              : this.content,
          messageBody: this.content,
          parentMessageId: null,
          recipientUserId: this.SelectedChatId,
          recipientGroupId: null,
          isForwarded: false,
          createDate: "2022-04-19T05:46:54.441Z",
          userId: 0,
        });
        const idx = this.Messages.findIndex((e) => e.MessageId === 0);
        this.Messages[idx].MessageId = response.data;
        this.Messages[idx].sent = true;
        this.scrollToBottom();
        this.GetParentConversations();
        this.content = "";
      }
    },
    async GetConversations(gId, mId) {
      if (this.Messages.findIndex((e) => e.MessageId === 0) > -1) {
        return;
      } else {
        try {
          const response =
            await this.$repositories.getConversation.getConversation(
              gId,
              mId,
              this.SelectedChatId
            );
          let tmpMessage = {
            CreateDate: "",
            CreatorUserId: 0,
            IsDelivered: false,
            IsForwarded: false,
            IsRead: 0,
            MessageBody: "",
            MessageId: 0,
            ParentMessageId: 0,
            Subject: "",
            sent: true,
          };
          if (response.data.length > 0) {
            response.data.forEach((item) => {
              if (
                this.Messages.findIndex(
                  (e) => e.MessageId === item.MessageId
                ) === -1
              ) {
                tmpMessage.CreateDate = item.CreateDate;
                tmpMessage.CreatorUserId = item.CreatorUserId;
                tmpMessage.IsDelivered = item.IsDelivered;
                tmpMessage.IsForwarded = item.IsForwarded;
                tmpMessage.IsRead = item.IsRead;
                tmpMessage.MessageBody = item.MessageBody;
                tmpMessage.MessageId = item.MessageId;
                tmpMessage.ParentMessageId = item.ParentMessageId;
                tmpMessage.Subject = item.Subject;
                tmpMessage.sent = true;
                const clone = { ...tmpMessage };
                this.Messages.push(clone);
                tmpMessage.CreateDate = "";
                tmpMessage.CreatorUserId = 0;
                tmpMessage.IsDelivered = false;
                tmpMessage.IsForwarded = false;
                tmpMessage.IsRead = 0;
                tmpMessage.MessageBody = "";
                tmpMessage.MessageId = 0;
                tmpMessage.ParentMessageId = 0;
                tmpMessage.Subject = "";
              } else {
                let idx = this.Messages.findIndex(
                  (e) => e.MessageId === item.MessageId
                );
                this.Messages[idx].IsDelivered = item.IsDelivered;
                this.Messages[idx].IsRead = item.IsRead;
              }
            });
          }
          this.Messages.sort(function (a, b) {
            const key1 = a.CreateDate;
            const key2 = b.CreateDate;

            if (key1 < key2) {
              return -1;
            } else if (key1 === key2) {
              return 0;
            } else {
              return 1;
            }
          });
          if (this.Atbottom) {
            this.scrollToBottom();
            await this.IsReadMessages();
            const secondRes = await this.$repositories.getMenu.getMenu();
            this.ParentChats = secondRes.data;
          }
        } catch (error) {
          console.log(error);
        }
      }
    },
  },
};
</script>

<style>
.chat-width .MessageBox:before {
  content: "";
  width: 0;
  height: 0;
  border-top: 6px solid transparent;
  border-bottom: 6px solid transparent;
  border-right: 6px solid #f4f4f4;
  left: -6px;
  right: auto;
  top: 19px;
  position: absolute;
}
.chat_text{
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  font-size: 14px;
  color: #888;
}
.chat_name{
  font-size: 15px;
  font-weight: 600;
  line-height: 23px;
}
.main-container {
  min-height: max-content !important;
}
.custom-chatbox-radius {
  border-top-left-radius: 0.25rem;
  border-bottom-left-radius: 0.25rem;
}
.custom-discussions-radius {
  border-top-right-radius: 0.25rem;
}
.custom-follower-radius {
  border-bottom-right-radius: 0.25rem;
}
.custom-chat-footer-radius {
  border-bottom-left-radius: 0.25rem;
}
.emoji-mart {
  z-index: 9999999999999999 !important;
}
.chat-actions-height {
  min-height: 3rem;
  max-height: 3rem;
}
.send-button-color {
  background-color: #2a41e8;
  color: #fff;
  border-radius: 6px !important;
}
.readed-color {
  color: #088dcd !important;
}
.delivered-color {
  color: #fff !important;
}
.send-button-color:hover {
  background-color: #0870a0;
  color: #fff;
}
.custom-radius {
  border-radius: 20px !important;
}
.chat-width {
  width: auto;
  min-width: 30% !important;
  max-width: 80% !important;
}
.emoji-mart-bar-preview {
  display: none !important;
}
.custom-z-index {
  z-index: 1 !important;
}
.wrapping {
  word-wrap: break-word !important;
}
.sub-bg {
  background: #f4f9ff;
}
@media only screen and (max-width: 767px) {
  .chat-bg {
    z-index: 0;
    background-image: url("/chat-bg.jpg");
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
  }
}
@media only screen and (min-width: 768px) {
  .chat-bg {
    z-index: 0;
    /*background-image: url("/chat-bg.jpg");*/
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
  }
}
.SingleMessage {
  transform: translateY(40px);
  transition: 1s ease;
}

.SentMessage {
  transform: translateY(80px);
  transition: 1s ease;
}

#right-click-menu {
  background: #fafafa;
  border: 1px solid #bdbdbd;
  box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14), 0 3px 1px -2px rgba(0, 0, 0, 0.2),
    0 1px 5px 0 rgba(0, 0, 0, 0.12);
  display: block;
  list-style: none;
  margin: 0;
  padding: 0;
  position: absolute;
  width: 250px;
  z-index: 999999;
}

#right-click-menu li {
  border-bottom: 1px solid #e0e0e0;
  margin: 0;
  padding: 5px 35px;
}

#right-click-menu li:last-child {
  border-bottom: none;
}

#right-click-menu li:hover {
  background: #1e88e5;
  color: #fafafa;
}

.SendBtn {
  width: 50px;
  height: 50px;
  border-radius: 50px;
  background-color: #3ba58a;
}
.Attachment {
  border-top-left-radius: 0px !important;
  border-bottom-left-radius: 0px !important;
}

.Emoji-Container {
  position: absolute;
  top: 17.5rem;
  z-index: 999999999999999999 !important;
}

.Emojis {
  border-top-right-radius: 0px !important;
  border-bottom-right-radius: 0px !important;
}

.Text {
  border-top-left-radius: 0px !important;
  border-bottom-left-radius: 0px !important;
  border-top-right-radius: 0px !important;
  border-bottom-right-radius: 0px !important;
}

.SendMessageWrapper {
  border-color: rgb(184 206 219);
  line-height: 3rem;
  border-radius: 5px;
  border: none;
}

.Replay {
  border-top: solid 2px #e7e7e7;
}

.ChatTopBtn {
  line-height: 2rem;
  border-radius: 5px;
  background-color: #fff;
  border: none;
}

.UnreadCountWrapper {
  background-color: #efaa86;
  width: 20px;
  height: 20px;
  border-radius: 50px;
}
.chat-my-message {
  color: #8b8b8b !important;
}
.chat-your-message {
  color: #e8faff !important;
}
.MessageBox {
  background-color: #f4f4f4;
  border-radius: 6px;
  color: #333;
}
.MessageBoxOther {
  background: #2a41e8;
  border-radius: 6px;
  color: white;
}
.MessageBoxOther .Forwarded small i {
  color: grey !important;
}

.MessageBoxOther i {
  color: grey !important;
}
.MessageBoxOther .Replay p {
  color: grey !important;
}

.ChatParent {
  border-radius: 5px;
}

.SearchBtn {
  background-color: #fff;
  border: none;
}

.AddBtn {
  width: 50px;
  height: 50px;

  background-color: #fff;
  border: solid 0.5px #e7e7e7;
  line-height: 3rem;
  border-radius: 50px;
  border: none;
}

.SingleParentItem {
  transition: 0.2s ease;
  cursor: pointer;
  border-radius: 5px;
}

/*.SingleParentItemActive {*/
/*  background-color: #68b4ff;*/
/*  border-radius: 0;*/
/*}*/
.SingleParentItemActive small {
  color: #888 !important;
}
.SingleParentItemActive span {
  color: #333;
}
.SingleParentItemActive p {
  color: #333;
}

.SingleParentItem:hover {
  background-color: #fafafa;
  transition: 0.2s ease;
  /*box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);*/
}

.Search {
  border: none;
  /* border-top-left-radius: 0px !important;
  border-bottom-left-radius: 0px !important;

  background-color: #fff;
  border: solid 0.5px #e7e7e7;
  line-height: 3rem;
  border-radius: 5px;
  border-bottom: none;

  border-top: none;
  border-left: none; */
}
</style>
