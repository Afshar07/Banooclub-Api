<template>
  <div class="col-lg-9 col-12 p-0 m-0 d-md-none">
    <div class="bg-white rounded main-shadow custom-chat-height">
      <header
        class="bg-white main-shadow col-12 custom-z-index py-3 rounded-bottom p-2 d-flex align-items-center justify-content-between main-shadow"
      >
        <div class="d-flex align-items-center">
          <img
            v-if="SelectedUserData.selfieFileData"
            :src="
              BaseUrl +
              SelectedUserData.selfieFileData
            "
            alt="User Avatar"
            width="30px "
            height="30px"
            class="rounded"
          />
          <p class="my-auto me-3 text-capitalize">
            {{ SelectedUserData.fullName }}
          </p>
        </div>
        <svg
          @click="closeChat"
          xmlns="http://www.w3.org/2000/svg"
          class="custom-icon text-dark"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          stroke-width="1.5"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M15 19l-7-7 7-7"
          />
        </svg>
      </header>
      <div class="row col-12 mx-auto position-relative">
        <div class="chat-bg position-absolute top-0 start-0 w-100 h-100"></div>
        <div
          class="col-md-12"
          ref="conversations"
          @scroll="handleScroll($event)"
          :style="{ height: height + 'vh!important' }"
          style="overflow-y: scroll; z-index: 99999 !important"
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
            class="p-lg-4 px-2 mx-1 my-1 user-message"
            :class="{ SingleMessage: item.MessageId === 0 }"
            v-for="(item, index) in Messages"
            :key="index"
          >
            <div
              class="row d-flex align-items-center mt-4"
              :class="{
                'd-flex flex-row-reverse':
                  item.CreatorUserId !== $auth.user.userId,
              }"
              @click.right.prevent="openMenu($event, item)"
            >
              <div
                class="col-md-1 d-flex align-items-md-center flex-column mb-2"
                :class="
                  item.CreatorUserId === $auth.user.userId
                    ? 'align-items-start'
                    : 'align-items-end'
                "
              >
                <img
                  v-if="isMyMessage(item, index)"
                  :src="BaseUrl + $store.state.HeaderData.selfie"
                  width="30px "
                  height="30px"
                  class="rounded-circle"
                  alt=""
                />
                <img
                  v-else-if="isYourMessage(item, index)"
                  :src="
                    BaseUrl +
                    SelectedUserData.selfieFileData
                  "
                  width="30px "
                  height="30px"
                  class="rounded-circle"
                  alt=""
                />
                <small class="mt-2">{{
                  item.CreateDate | moment("HH:mm")
                }}</small>
              </div>
              <div
                class="col-md-11"
                :class="{
                  'd-flex justify-content-end':
                    item.CreatorUserId != $auth.user.userId,
                }"
              >
                <div
                  dir="auto"
                  class="MessageBox col-9 col-md-5 pt-3 pe-3 ps-2"
                  :class="{
                    'MessageBoxOther chat-your-message':
                      item.CreatorUserId !== $auth.user.userId,
                  }"
                >
                  <div v-if="item.IsForwarded" class="Forwarded">
                    <small> <i class="text-muted">Forwarded Message</i></small>
                  </div>

                  <div
                    v-if="item.ParentMessageId && item.ParentMessageId != 0"
                    class="Replay my-3"
                  ></div>
                  <div
                    class="w-100 wrapping"
                    dir="auto"
                    :class="
                      item.CreatorUserId !== $auth.user.userId
                        ? 'chat-your-message'
                        : 'chat-my-message'
                    "
                  >
                    <p dir="auto" class="wrapping">
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
                  <div v-if="item.CreatorUserId === $auth.user.userId">
                    <i
                      v-if="item.sent && !item.IsRead && !item.IsDelivered"
                      class="fas fa-check"
                      style="font-size: 8px"
                    ></i>
                    <i
                      v-if="item.IsRead === true"
                      class="fas fa-check-double"
                      :class="item.IsRead ? 'readed-color' : 'chat-my-message'"
                      style="font-size: 8px"
                    ></i>
                    <i
                      v-if="!item.sent"
                      class="far fa-circle"
                      style="font-size: 8px"
                    ></i>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div
          v-if="SelectedParentId"
          class="col-12 mt-0 SendMessage position-fixed custom-z-index"
          style="bottom: 1rem"
        >
          <div class="row">
            <div
              class="col-lg-12 col-12 bg-white custom-z-index p-0 mt-2"
              dir="auto"
            >
              <div
                class="mt-1 px-1 d-flex align-items-center justify-content-between col-12"
              >
                <button
                  type="button"
                  class="send-button-color col-2 d-flex flex-nowrap align-items-center justify-content-center small btn border rounded-3 ms-2"
                  @click="SendMessage"
                >
                  <i
                    class="fas fa-paper-plane ms-1"
                    style="font-size: 10px"
                  ></i>
                  <span class="small">ارسال</span>
                </button>
                <input
                  class="col-8 border rounded-2 py-1 px-2"
                  type="text"
                  name="content"
                  v-model="content"
                  @keydown.enter="SendMessage"
                  dir="auto"
                />
                <button class="btn col-2" @click="ToggleEmojis">
                  <i class="far fa-smile-beam" style="font-size: 20px"></i>
                </button>
              </div>
            </div>
            <!--
            <div class="col-md-12" v-if="Uploaded">
              <div class="row pe-4">
                <div class="col-md-4">
                  <span
                    class="badge bg-danger text-white"
                    style="cursor: pointer"
                    @click="CancelUpload"
                  >
                    <i class="fas fa-minus"></i
                  ></span>
                  <img :src="BaseImgUrl" class="img-fluid rounded" alt="" />
                </div>
                <div class="col-md-8">
                  <p>{{ content }}</p>
                </div>
              </div>
            </div> -->
          </div>

          <div class="d-flex justify-content-end" ref="Emoji">
            <client-only>
              <Picker
                :data="emojiIndex"
                class="emoji-position"
                v-show="SelectingEmoji"
                @select="showEmoji"
              />
            </client-only>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import data from "emoji-mart-vue-fast/data/all.json";
import "emoji-mart-vue-fast/css/emoji-mart.css";
import { Picker, EmojiIndex } from "emoji-mart-vue-fast";
let emojiIndex = new EmojiIndex(data);

export default {
  components: { Picker },
  emits: [
    "handleScroll",
    "openMenu",
    "SendMessage",
    "UploadFile",
    "CancelUpload",
    "closeChat",
  ],
  props: {
    Messages: {
      type: Array,
      required: false,
    },
    SelectedParentId: {
      type: Number,
      required: false,
    },
    Uploaded: {
      type: Boolean,
      required: false,
      default: false,
    },
    BaseImgUrl: {
      type: String,
      required: false,
    },
    SelectedUserData: {
      type: Object,
      required: false,
    },
  },

  head() {
    return {
      bodyAttrs: {
        class: "fixed-body",
      },
    };
  },
  mounted() {
    this.$nextTick(async () => {
      await this.scrollToBottom();
    });
  },
  data() {
    return {
      emojiIndex: emojiIndex,
      content: "",
      SelectingEmoji: false,
      height: 81,
      emojisOutput: "",
    };
  },
  methods: {
    isMyMessage(message, index) {
      // Check if last message was from the same user, and don't render profile picture if it is
      if (index > 1) {
        if (
          message.CreatorUserId == this.$auth.user.userId &&
          this.Messages[index - 1].CreatorUserId != message.CreatorUserId
        ) {
          return true;
        } else {
          return false;
        }
      } else {
        return false;
      }
    },
    isYourMessage(message, index) {
      // Check if last message was from the same user, and don't render profile picture if it is
      if (index > 1) {
        if (
          message.CreatorUserId != this.$auth.user.userId &&
          this.Messages[index - 1].CreatorUserId != message.CreatorUserId
        ) {
          return true;
        } else {
          return false;
        }
      } else {
        return false;
      }
    },
    showEmoji(emoji) {
      this.content += emoji.native;
    },
    scrollToBottom() {
      // Set it in a timeout so it runs after the DOM is rendered
      const container = this.$refs.conversations;
      setTimeout(async () => {
        container.scrollTop = await container.scrollHeight;
      }, 100);
    },
    closeChat() {
      this.$emit("closeChat");
    },
    CancelUpload() {
      this.$emit("CancelUpload");
    },
    ToggleEmojis() {
      this.SelectingEmoji = !this.SelectingEmoji;
    },
    handleScroll(event) {
      this.$emit("handleScroll", event);
    },
    openMenu(e, item) {
      this.$emit("openMenu", e, item);
    },
    SendMessage() {
      this.$emit("SendMessage", this.content);
      this.SelectingEmoji = false;
      this.content = "";
    },
    UploadFile() {
      this.$emit("UploadFile", this.$refs.ChatFile.files[0]);
    },
  },
  computed: {
    BaseUrl() {
      return process.env.pic;
    },
  },
};
</script>
<style scoped>
.custom-z-index {
  z-index: 99999 !important;
}
.user-message:last-of-type {
  margin-bottom: 3rem !important;
}
.custom-chat-height {
  min-height: 100vh;
  max-height: 100vh;
}
.chat-my-message {
  color: #636363 !important;
}
.chat-your-message {
  color: #e8faff !important;
}
.emoji-mart {
  z-index: 9999999999999999 !important;
}
/* .chat-actions-height {
  min-height: 3rem;
  max-height: 3rem;
} */
.send-button-color {
  background-color: #fff;
  color: #0870a0;
}
.send-button-color:hover {
  background-color: #fff;
  color: #0870a0;
}
.custom-radius {
  border-radius: 20px !important;
}
.emoji-mart-bar-preview {
  display: none !important;
}
.header-bg {
  background-color: #f5f5f5;
}

.custom-icon {
  width: 1.5rem;
  height: 1.5rem;
}
.emoji-position {
  position: absolute;
  bottom: 5.5rem !important;
  z-index: 99999 !important;
}
</style>
