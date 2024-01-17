<template>
  <b-card
      v-if="data"
      no-body
      class="card-statistics"
  >
    <b-card-header>
      <b-card-title>آمار کلی سامانه</b-card-title>
      <b-card-text class="font-small-2 mr-25 mb-0">

      </b-card-text>
    </b-card-header>
    <b-card-body class="statistics-body">
      <b-row>
        <b-col
            xl="3"
            sm="6"
        >
          <b-media no-body>
            <b-media-aside

                class="mr-2"
            >
              <b-avatar
                  size="48"
                  variant="warning"
              >
                <feather-icon
                    size="24"
                    icon="UserIcon"
                />
              </b-avatar>
            </b-media-aside>
            <b-media-body class="my-auto">
              <h4 class="font-weight-bolder mb-0">
                تعداد کاربران ثبت نام شده
              </h4>
              <b-card-text class="font-small-3 mb-0">
                {{ data.registeredUsersCount }}
              </b-card-text>
            </b-media-body>
          </b-media>
        </b-col>
        <b-col
            xl="3"
            sm="6"
        >
          <b-media no-body>
            <b-media-aside

                class="mr-2"
            >
              <b-avatar
                  size="48"
                  variant="primary"
              >
                <feather-icon
                    size="24"
                    icon="BookOpenIcon"
                />
              </b-avatar>
            </b-media-aside>
            <b-media-body class="my-auto">
              <h4 class="font-weight-bolder mb-0">
                تعداد پست های در انتظار تایید
              </h4>
              <b-card-text class="font-small-3 mb-0">
                {{ data.notConfirmedPostsCount }}
              </b-card-text>
            </b-media-body>
          </b-media>
        </b-col>
        <b-col
            xl="3"
            sm="6"
        >
          <b-media no-body>
            <b-media-aside

                class="mr-2"
            >
              <b-avatar
                  size="48"
                  variant="success"
              >
                <feather-icon
                    size="24"
                    icon="MessageCircleIcon"
                />
              </b-avatar>
            </b-media-aside>
            <b-media-body class="my-auto">
              <h4 class="font-weight-bolder mb-0">
                تعداد تیکت های در انتظار پاسخ
              </h4>
              <b-card-text class="font-small-3 mb-0">
                {{ data.ticketsAwaitingResponseCount }}
              </b-card-text>
            </b-media-body>
          </b-media>
        </b-col>
        <b-col
            xl="3"
            sm="6"
        >
          <b-media no-body>
            <b-media-aside

                class="mr-2"
            >
              <b-avatar
                  size="48"
                  variant="info"
              >
                <feather-icon
                    size="24"
                    icon="BriefcaseIcon"
                />
              </b-avatar>
            </b-media-aside>
            <b-media-body class="my-auto">
              <h4 class="font-weight-bolder mb-0">
                تعداد آگهی های در انتظار تایید
              </h4>
              <b-card-text class="font-small-3 mb-0">
                {{ data.adsAwaitingConfirmCount }}
              </b-card-text>
            </b-media-body>
          </b-media>
        </b-col>
        <b-col
            xl="3"
            sm="6"
            class="my-2"
        >
          <b-media no-body>
            <b-media-aside

                class="mr-2"
            >
              <b-avatar
                  size="48"
                  variant="danger"
              >
                <feather-icon
                    size="24"
                    icon="ListIcon"
                />
              </b-avatar>
            </b-media-aside>
            <b-media-body class="my-auto">
              <h4 class="font-weight-bolder mb-0">
                تالار های گزارش شده توسط ربات
              </h4>
              <b-card-text class="font-small-3 mb-0">
                {{data.forumsReportedByBots }}
              </b-card-text>
            </b-media-body>
          </b-media>
        </b-col>
        <b-col
            xl="3"
            sm="6"
            class="my-2"
        >
          <b-media no-body>
            <b-media-aside

                class="mr-2"
            >
              <b-avatar
                  size="48"
                  variant="danger"
              >
                <feather-icon
                    size="24"
                    icon="CopyIcon"
                />
              </b-avatar>
            </b-media-aside>
            <b-media-body class="my-auto">
              <h4 class="font-weight-bolder mb-0">
                 تالار در انتظار تایید
              </h4>
              <b-card-text class="font-small-3 mb-0">
                {{data.forumsAwaitingConfirmCount}}
              </b-card-text>
            </b-media-body>
          </b-media>
        </b-col>
      </b-row>
    </b-card-body>
  </b-card>
</template>

<script>
import {
  BCard, BCardHeader, BCardTitle, BCardText, BCardBody, BRow, BCol, BMedia, BMediaAside, BAvatar, BMediaBody,
} from 'bootstrap-vue'

export default {
  components: {
    BRow,
    BCol,
    BCard,
    BCardHeader,
    BCardTitle,
    BCardText,
    BCardBody,
    BMedia,
    BAvatar,
    BMediaAside,
    BMediaBody,
  },
  props: {
    data: {
      type: Array,
      default: () => [],
    },
  },
}
</script>
