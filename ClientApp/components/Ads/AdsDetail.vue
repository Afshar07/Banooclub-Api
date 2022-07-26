<template>
  <div>
<!--    <base-modal v-if="is_show_delete_modal" @close_modal="hideDeleteModal">-->
<!--      <template v-slot:title>-->
<!--        <h5 class="text-right px-3">حذف خدمت</h5>-->
<!--      </template>-->
<!--      <template v-slot:content>-->
<!--        <div class="d-flex flex-column">-->
<!--          <p>آیا از حذف این خدمت اطمینان دارید؟</p>-->
<!--          <div class="d-flex">-->
<!--            <button @click="deleteService(service_details.servicePackId)"-->
<!--                    class="tw-w-1/2 tw-flex tw-items-center tw-justify-center tw-py-2 tw-text-red-500 hover:tw-bg-red-100 hover:tw-text-red-500 tw-rounded-md">-->
<!--              <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6 tw-ml-1" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">-->
<!--                <path stroke-linecap="round" stroke-linejoin="round" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />-->
<!--              </svg>-->
<!--              بله-->
<!--            </button>-->
<!--            <button @click="hideDeleteModal"-->
<!--                    class="tw-w-1/2 tw-flex tw-items-center tw-justify-center tw-py-2 tw-text-green-500 hover:tw-bg-green-100 hover:tw-text-greeb-500 tw-rounded-md">-->
<!--              خیر-->
<!--            </button>-->
<!--          </div>-->
<!--        </div>-->
<!--      </template>-->
<!--    </base-modal>-->
    <div class="custom_card">
      <!-- post header-->
      <div class="tw-flex tw-justify-between tw-items-center lg:tw-p-4 tw-p-2.5">
        <div class="tw-flex tw-items-center">
          <nuxt-link :to="`/user/${AdsDetail.userInfo.userId}/posts`" class="text-decoration-none">
            <img v-if="AdsDetail.userInfo.selfieFileData!==null" :src="`https://banooclubapi.simagar.com/media/gallery/profile/${AdsDetail.userInfo.selfieFileData}`" alt="profile_image" class="tw-bg-gray-200 tw-border tw-border-white tw-rounded-full tw-w-10 tw-h-10">
            <img v-else :src="`/nopicture.jpg`" alt="profile_image" class="tw-bg-gray-200 tw-border tw-border-white tw-rounded-full tw-w-10 tw-h-10">
          </nuxt-link>
          <div class="d-flex flex-column tw-font-semibold tw-capitalize tw-mr-4">
            <h2 class="tw-text-black text-decoration-none service_name">{{ AdsDetail.userInfo.userName }}</h2>
            <div class="d-flex">
              <div class="d-flex justify-content-start align-items-center">
                <AllUsersIcon fill="#374151" style="width: 15px; height: 15px;"/>
                <div class="text-gray-700 tw-flex tw-items-center px-2">
                  {{
                    time_ago(AdsDetail.createDate)
                  }}
                </div>
              </div>
            </div>

          </div>
        </div>
        <div >
          <button  class="tw-text-2xl hover:tw-bg-gray-200 tw-rounded-full tw-p-2 tw-transition tw--mr-1 tw-relative">
            <svg @click.stop="showMoreDiv" xmlns="http://www.w3.org/2000/svg" class="tw-h-6 tw-w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
              <path stroke-linecap="round" stroke-linejoin="round" d="M5 12h.01M12 12h.01M19 12h.01M6 12a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z" />
            </svg>
          </button>

          <div v-if="show_more" class="tw-absolute tw-z-10 tw-left-5 tw-bg-white tw-w-56 tw-shadow-md tw-mx-auto tw-p-2 tw-rounded-md tw-text-gray-500 tw-text-base tw-border tw-border-gray-100">
            <ul class="tw-pl-0 mb-0">
              <li>
                <a href="#share" class="tw-w-full tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                  <ShareIcon  class="tw-ml-1"/>
                  اشتراک گذاری
                </a>
              </li>
              <li>
                <button v-if="!AdsDetail.isFavourite" @click.stop="createWishList(AdsDetail.adsId)" class="tw-w-full tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                  <EmptyStar/>
                  افزودن به علاقمندی ها
                </button>
                <button v-else @click="deleteWishList(AdsDetail.adsId)" class="tw-w-full tw-text-gray-700 text-decoration-none tw-flex tw-items-center tw-px-3 tw-py-2 hover:tw-bg-gray-200 hover:tw-text-gray-800 tw-rounded-md">
                  <SolidSter/>
                  حذف از علاقمندی ها
                </button>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <vueper-slides
        class="my-2 no-shadow"
        ref="vueperslides1"
        @slide="$refs.vueperslides2.goToSlide($event.currentSlide.index, { emit: false })"
        :rtl="true"
        :slide-ratio="1 / 4"
        :bullets="false"
        :touchable="false"
        fade
        :arrows="true"
        :autoplay="false"
        fixed-height="340px">
        <vueper-slide
          v-for="(slide, i) in AdsDetail.photos"
          :key="i"
        >
          <template #content>
            <img v-if="slide.priority == 1 || slide.priority == 2" class="tw-w-full tw-h-full" :src="`https://banooclubapi.simagar.com/${slide.base64}`" :alt="AdsDetail.title"
                 style="object-fit: contain;object-position: center !important;height: 330px;!important; ">
            <video
              v-else-if="slide.priority == 3"
              class="w-100 tw-h-full"
              controls
              :src="`https://banooclubapi.simagar.com/${slide.base64}`"
            ></video>
          </template>
        </vueper-slide>
      </vueper-slides>
      <vueper-slides
        class="no-shadow mb-0"
        ref="vueperslides2"
        :rtl="true"
        :slide-ratio="1 / 8"
        :dragging-distance="50"
        @slide="$refs.vueperslides1.goToSlide($event.currentSlide.index, { emit: false })"
        :visible-slides="3"
        fixed-height="100px"
        :arrows="true"
        :bullets="false"
        :touchable="true"
        :gap="2">
        <vueper-slide
          v-for="(slide, i) in AdsDetail.photos"
          :key="i"
          @click.native="$refs.vueperslides2 && $refs.vueperslides2.goToSlide(i)">
          <template #content>
            <img v-if="slide.priority == 1 || slide.priority == 2" class=" tw-w-full" :src="`https://banooclubapi.simagar.com/${slide.base64}`" :alt="AdsDetail.title" style="object-fit: cover;object-position: center !important; height: 100px;">
            <img v-else-if="slide.priority == 3" class=" tw-w-full"
                 src="/video_player.jpg"
                 :alt="AdsDetail.title" style="object-fit: contain;object-position: center !important; height: 100px;">
          </template>
        </vueper-slide>
      </vueper-slides>

      <div class="tw-p-4 tw-space-y-3">
        <div class="d-flex justify-content-between">
          <h1 class="tw-text-2xl tw-font-semibold tw-text-gray-600 tw-pt-2">{{AdsDetail.title}}</h1>
          <div class="tw-bg-gray-100 tw-text-gray-600 tw-font-semibold tw-px-3 tw-py-1 tw-rounded-full tw-text tw-text-sm d-flex justify-content-center align-items-center">
            {{Intl.NumberFormat('fa-IR').format(AdsDetail.price)}}
            تومان
          </div>
        </div>
        <div class="d-flex mt-0">
          <div class="back_tags p-1 m-1" v-for="(tag,index) in AdsDetail.tags">
            {{ tag.title }}
          </div>
        </div>

        <h2 class="tw-text-base tw-font-semibold tw-text-gray-600 tw-pt-2">توضیحات</h2>
        <p class="tw-text-gray-600 mt-1">
          {{AdsDetail.description}}
        </p>

        <h2 class="tw-text-base tw-font-semibold tw-text-gray-600 tw-pt-2">موقعیت روی نقشه</h2>
        <div class="my-3" id="map-wrap" style="height: 25vh">
          <client-only>
            <l-map :zoom="17" :center="[AdsDetail.latitude,AdsDetail.longitude]" @click="addMarker">
              <l-tile-layer
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
              ></l-tile-layer>
              <l-marker :lat-lng="[AdsDetail.latitude,AdsDetail.longitude]"></l-marker>
            </l-map>
          </client-only>
        </div>

<!--        <h2 v-if="AdsDetail.mobile || AdsDetail.phoneNumber1 || AdsDetail.phone_number2" class="tw-text-base tw-font-semibold tw-text-gray-600 tw-pt-2">اطلاعات تماس</h2>-->
<!--        <div class="d-flex justify-content-between align-items-center row mt-1">-->
<!--          <div v-if="AdsDetail.mobile" class="d-flex align-items-center col-md-6 col-12">-->
<!--            <p class="mb-0 tw-text-gray-600 ">شماره همراه : </p>-->
<!--            <a class="text-decoration-none px-1 tw-text-gray-600" :href="`tel:${AdsDetail.mobile}`">{{AdsDetail.mobile}}</a>-->
<!--          </div>-->
<!--          <div v-if="AdsDetail.phoneNumber1" class="d-flex align-items-center col-md-6 col-12">-->
<!--            <p class="mb-0 tw-text-gray-600">شماره تلفن 1 : </p>-->
<!--            <a class="text-decoration-none px-1 tw-text-gray-600" :href="`tel:${AdsDetail.phoneNumber1}`">{{AdsDetail.phoneNumber1}}</a>-->
<!--          </div>-->
<!--          <div v-if="AdsDetail.phoneNumber2" class="d-flex align-items-center col-md-6 col-12">-->
<!--            <p class="mb-0 tw-text-gray-600">شماره تلفن 2 : </p>-->
<!--            <a class="text-decoration-none px-1 tw-text-gray-600" :href="`tel:${AdsDetail.phoneNumber2}`">{{AdsDetail.phoneNumber2}}</a>-->
<!--          </div>-->
<!--        </div>-->
        <div v-if="AdsDetail.tag!==null" class="tw-text-gray-600 d-flex flex-column tw-text-sm md:tw-pt-3">
          <h2 class="tw-text-base tw-font-semibold tw-text-gray-600 tw-pt-2 mb-0">هشتگ ها</h2>
                  <div class="d-flex align-items-center gap-2">
                    <span class="text-secondary"  v-for="item in ArrangedTags(AdsDetail.tag)"> #{{ item}} </span>
                  </div>
        </div>
        <hr>
<!--        <div class="tw-grid md:tw-grid-cols-2 md:tw-gap-4 tw-mb-5 xs:tw-grid-cols-12">-->
<!--          <a :href="`tel:${AdsDetail.mobile}`" class="text-decoration-none tw-bg-gray-200 tw-flex tw-flex-1 tw-font-semibold tw-h-10 tw-items-center tw-justify-center tw-px-4 tw-rounded-md my-1">-->
<!--            تماس-->
<!--          </a>-->
<!--          <button class="tw-bg-blue-600 tw-flex tw-flex-1 tw-font-semibold tw-h-10 tw-items-center tw-justify-center tw-px-4 tw-rounded-md tw-text-white my-1">-->
<!--            چت-->
<!--          </button>-->
<!--          &lt;!&ndash;          <button class="tw-bg-blue-600 tw-flex tw-flex-1 tw-font-semibold tw-h-10 tw-items-center tw-justify-center tw-px-4 tw-rounded-md tw-text-white my-1">&ndash;&gt;-->
<!--          &lt;!&ndash;            افزودن به سبد خرید&ndash;&gt;-->
<!--          &lt;!&ndash;          </button>&ndash;&gt;-->
<!--        </div>-->
        <div>
<!--          <client-only>-->
<!--            <div class="d-flex align-items-center">-->
<!--              <vue-star-rating :star-size="20" v-model="service_rates_show[0]" :show-rating="false" :read-only="true"	></vue-star-rating>-->
<!--              <div class="tw-tooltip tw-w-full px-2" :data-tip="rates.oneStar + '%'">-->
<!--                <progress class="tw-progress tw-progress-warning w-56 mt-3" :value="(rates.oneStar*100)/100" max="100"></progress>-->
<!--              </div>-->
<!--            </div>-->
<!--            <div class="d-flex align-items-center">-->
<!--              <vue-star-rating :star-size="20" v-model="service_rates_show[1]" :show-rating="false" :read-only="true"	></vue-star-rating>-->
<!--              <div class="tw-tooltip tw-w-full px-2" :data-tip="rates.twoStar + '%'">-->
<!--                <progress class="tw-progress tw-progress-warning w-56 mt-3" :value="(rates.twoStar*100)/100" max="100"></progress>-->
<!--              </div>-->
<!--            </div>-->
<!--            <div class="d-flex align-items-center">-->
<!--              <vue-star-rating :star-size="20" v-model="service_rates_show[2]" :show-rating="false" :read-only="true"	></vue-star-rating>-->
<!--              <div class="tw-tooltip tw-w-full px-2" :data-tip="rates.threeStar + '%'">-->
<!--                <progress class="tw-progress tw-progress-warning w-56 mt-3" :value="(rates.threeStar*100)/100" max="100"></progress>-->
<!--              </div>-->
<!--            </div>-->
<!--            <div class="d-flex align-items-center">-->
<!--              <vue-star-rating :star-size="20" v-model="service_rates_show[3]" :show-rating="false" :read-only="true"	></vue-star-rating>-->
<!--              <div class="tw-tooltip tw-w-full px-2" :data-tip="rates.fourStar + '%'">-->
<!--                <progress class="tw-progress tw-progress-warning w-56 mt-3" :value="(rates.fourStar*100)/100" max="100"></progress>-->
<!--              </div>-->

<!--            </div>-->
<!--            <div class="d-flex align-items-center">-->
<!--              <vue-star-rating :star-size="20" v-model="service_rates_show[4]" :show-rating="false" :read-only="true"	></vue-star-rating>-->
<!--              <div class="tw-tooltip tw-w-full px-2" :data-tip="rates.fiveStar + '%'">-->
<!--                <progress class="tw-progress tw-progress-warning w-56 mt-3" :value="(rates.fiveStar*100)/100" max="100"></progress>-->
<!--              </div>-->
<!--            </div>-->
<!--          </client-only>-->
        </div>
<!--        <div class="tw-flex lg:tw-font-bold tw-justify-between align-items-center">-->
<!--          <div class="d-flex justify-content-center align-items-center">-->
<!--            <div>امتیاز دهید:</div>-->
<!--            <client-only>-->
<!--              <vue-star-rating class="mb-2 tw-mr-2" @rating-selected="add_rate" :star-size="20" v-model="service_rate"	:show-rating="false"></vue-star-rating>-->
<!--            </client-only>-->
<!--            &lt;!&ndash;            <button class="tw-flex tw-items-center">&ndash;&gt;-->
<!--            &lt;!&ndash;              <LikeIcon fill="black"/>&ndash;&gt;-->
<!--            &lt;!&ndash;              <div class="mx-3">لایک</div>&ndash;&gt;-->
<!--            &lt;!&ndash;            </button>&ndash;&gt;-->
<!--            &lt;!&ndash;            <button class="tw-flex tw-items-center tw-mr-2">&ndash;&gt;-->
<!--            &lt;!&ndash;              <MessageIcon fill="black"/>&ndash;&gt;-->
<!--            &lt;!&ndash;              <div class="mx-3">نظر</div>&ndash;&gt;-->
<!--            &lt;!&ndash;            </button>&ndash;&gt;-->
<!--          </div>-->
<!--          <div class="d-flex" id="share">-->
<!--            <a target="_blank" :href="`https://telegram.me/share/url?url=${site_url+$route.fullPath}`">-->
<!--              <TelegramIcon style="width: 20px; height: 20px;"/>-->
<!--            </a>-->
<!--            <a target="_blank" :href="`https://api.whatsapp.com/send?text=${site_url+$route.fullPath}`">-->
<!--              <WhatsappIcon  style="width: 20px; height: 20px;"/>-->
<!--            </a>-->
<!--            <a target="_blank" :href="`https://www.facebook.com/sharer.php?u=${site_url+$route.fullPath}`">-->
<!--              <FacebookIcon style="width: 20px; height: 20px;"/>-->
<!--            </a>-->
<!--          </div>-->
<!--        </div>-->
<!--        <div class="tw-border-t d-flex" v-if="others_can_add_comments" :class="[service_details.comments.length>0?'comments_section_height':'']">-->
<!--          <div class="d-flex flex-column">-->
<!--            <div class="d-flex flex-column tw-py-3" v-for="comment in service_details.comments.filter(e=> e.baseId === 0)">-->
<!--              <div class="tw-flex tw-py-3">-->
<!--                <div class="tw-w-10 tw-h-10 tw-rounded-full tw-relative tw-flex-shrink-0">-->
<!--                  <img :src="`https://banooclubapi.simagar.com/media/gallery/Profile/${comment.userInfo.selfieFileData}`" alt="" class="tw-absolute tw-h-full tw-rounded-full tw-w-full">-->
<!--                </div>-->
<!--                <div>-->
<!--                  <div class="tw-text-gray-700 tw-py-2 tw-px-3 tw-rounded-md tw-bg-gray-100 tw-relative lg:tw-mr-5 tw-mr-2 lg:tw-ml-12">-->
<!--                    <div class="tw-absolute tw-w-3 tw-h-3 top-3 tw&#45;&#45;right-1 tw-bg-gray-100 tw-transform tw-rotate-45"></div>-->
<!--                    <p class="tw-leading-6 tw-flex justify-content-center align-items-center mb-0">-->
<!--                      {{comment.text}}-->
<!--                    </p>-->
<!--                  </div>-->
<!--                  <div class="tw-text-sm tw-flex tw-items-center tw-mt-2 tw-ml-5 mx-3">-->
<!--                    <button class="tw-flex tw-text-red-600 tw-ml-2">-->
<!--                      <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5 mx-1" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">-->
<!--                        <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />-->
<!--                      </svg>-->
<!--                      لایک-->
<!--                    </button>-->
<!--                    <button @click="replyAComment(comment)" class="mx-2"> پاسخ </button>-->
<!--                    <span class="mx-2"> 3 روز </span>-->
<!--                  </div>-->
<!--                </div>-->
<!--              </div>-->
<!--              <div class="tw-flex tw-py-3 tw-pr-5" v-for="el in service_details.comments.filter(e => e.baseId === comment.serviceCommentId )" >-->
<!--                <div class="tw-w-10 tw-h-10 tw-rounded-full tw-relative tw-flex-shrink-0">-->
<!--                  <img :src="`https://banooclubapi.simagar.com/media/gallery/Profile/${el.userInfo.selfieFileData}`" alt="" class="tw-absolute tw-h-full tw-rounded-full tw-w-full">-->
<!--                </div>-->
<!--                <div>-->
<!--                  <div class="tw-text-gray-700 tw-py-2 tw-px-3 tw-rounded-md tw-bg-gray-100 tw-relative lg:tw-mr-5 tw-mr-2 lg:tw-ml-12">-->
<!--                    <div class="tw-absolute tw-w-3 tw-h-3 top-3 tw&#45;&#45;right-1 tw-bg-gray-100 tw-transform tw-rotate-45"></div>-->
<!--                    <p class="tw-leading-6 tw-flex justify-content-center align-items-center mb-0">-->
<!--                      {{el.text}}-->
<!--                    </p>-->
<!--                  </div>-->
<!--                  <div class="tw-text-sm tw-flex tw-items-center tw-mt-2 tw-ml-5 mx-3">-->
<!--                    <button class="tw-flex tw-text-red-600 tw-ml-2">-->
<!--                      <svg xmlns="http://www.w3.org/2000/svg" class="tw-h-5 tw-w-5 mx-1" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">-->
<!--                        <path stroke-linecap="round" stroke-linejoin="round" d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />-->
<!--                      </svg>-->
<!--                      لایک-->
<!--                    </button>-->
<!--                    <button @click="replyAComment(el)" class="mx-2"> پاسخ </button>-->
<!--                    <span class="mx-2"> 3 روز </span>-->
<!--                  </div>-->
<!--                </div>-->
<!--              </div>-->
<!--            </div>-->

<!--          </div>-->
<!--        </div>-->
<!--        <div v-if="others_can_add_comments" class="tw-bg-gray-100 tw-rounded-full tw-relative tw-border-t">-->
<!--          <input v-on:keyup.enter="addComment()" v-model="service_comment" :placeholder="want_to_reply?place_holder:'نظر خود را وارد کنید'" class="tw-bg-transparent tw-max-h-10 tw-shadow-none tw-px-5">-->

<!--          &lt;!&ndash;          <div class="tw&#45;&#45;m-0.5 tw-absolute tw-top-2 tw-flex tw-items-center tw-left-3 tw-text-xl">&ndash;&gt;-->
<!--          &lt;!&ndash;            <nuxt-link to="#">&ndash;&gt;-->
<!--          &lt;!&ndash;              <svg xmlns="http://www.w3.org/2000/svg" class="tw-text-gray-600 tw-h-7 tw-w-7 hover:tw-bg-gray-200 tw-p-1.5 tw-rounded-full"&ndash;&gt;-->
<!--          &lt;!&ndash;                   fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">&ndash;&gt;-->
<!--          &lt;!&ndash;                <path stroke-linecap="round" stroke-linejoin="round" d="M14.828 14.828a4 4 0 01-5.656 0M9 10h.01M15 10h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />&ndash;&gt;-->
<!--          &lt;!&ndash;              </svg>&ndash;&gt;-->
<!--          &lt;!&ndash;            </nuxt-link>&ndash;&gt;-->
<!--          &lt;!&ndash;            <nuxt-link to="#">&ndash;&gt;-->
<!--          &lt;!&ndash;              <svg xmlns="http://www.w3.org/2000/svg" class="tw-text-gray-600 tw-h-7 tw-w-7 hover:tw-bg-gray-200 tw-p-1.5 tw-rounded-full" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">&ndash;&gt;-->
<!--          &lt;!&ndash;                <path stroke-linecap="round" stroke-linejoin="round" d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />&ndash;&gt;-->
<!--          &lt;!&ndash;              </svg>&ndash;&gt;-->
<!--          &lt;!&ndash;            </nuxt-link>&ndash;&gt;-->
<!--          &lt;!&ndash;            <nuxt-link to="#">&ndash;&gt;-->
<!--          &lt;!&ndash;              <svg xmlns="http://www.w3.org/2000/svg" class="tw-text-gray-600 tw-h-7 tw-w-7 hover:tw-bg-gray-200 tw-p-1.5 tw-rounded-full" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">&ndash;&gt;-->
<!--          &lt;!&ndash;                <path stroke-linecap="round" stroke-linejoin="round" d="M15.172 7l-6.586 6.586a2 2 0 102.828 2.828l6.414-6.586a4 4 0 00-5.656-5.656l-6.415 6.585a6 6 0 108.486 8.486L20.5 13" />&ndash;&gt;-->
<!--          &lt;!&ndash;              </svg>&ndash;&gt;-->
<!--          &lt;!&ndash;            </nuxt-link>&ndash;&gt;-->
<!--          &lt;!&ndash;          </div>&ndash;&gt;-->
<!--        </div>-->

      </div>

    </div>
  </div>

</template>

<script>
import AllUsersIcon from "../Icons/AllUsersIcon";
import MoreIcon from "../Icons/MoreIcon";
import LikeIcon from "../Icons/LikeIcon";
import MessageIcon from "../Icons/MessageIcon";
import ShareIcon from "../Icons/ShareIcon";
import EditIcon from "../Icons/EditIcon";
import {VueperSlides, VueperSlide} from 'vueperslides'
import 'vueperslides/dist/vueperslides.css';
import BaseModal from "../../components/utilities/BaseModal";
import TelegramIcon from "../Icons/TelegramIcon";
import WhatsappIcon from "../Icons/WhatsappIcon";
import InstagramIcon from "../Icons/InstagramIcon";
import FacebookIcon from "../Icons/FacebookIcon";
import EmptyStar from "../Icons/EmptyStar";
import SolidSter from "../Icons/SolidSter";


export default {
  name: "PDetails",
  components: {
    SolidSter,
    EmptyStar,
    FacebookIcon,
    InstagramIcon,
    WhatsappIcon,
    TelegramIcon,
    ShareIcon, MessageIcon, LikeIcon, MoreIcon, AllUsersIcon, EditIcon,  VueperSlides, VueperSlide, BaseModal },
  data(){
    return{
      // site_url: 'pplus.simagar.com',
      show_more: false,
      images_preview:[],
      serviceVideos:[],
      is_show_delete_modal:false,
      service_comment:'',
      others_can_add_comments:true,
      show_reply_modal:false,
      place_holder:'',
      want_to_reply:false,
      reply_parent_id:0,
      base_id:0,
      parentComments:[],
      service_rate:0,
      rates:[],
      center: [35.757539, 51.409968],
      latlng: [35, 51],
      service_rates_show:[1,2,3,4,5],
      custom_service_rates:[1,2,3,4,5]
    }
  },
  async fetch(){

  },
  methods:{
    ArrangedTags(tags){
      let data = tags.split(',')
      return data
    },
    addMarker(event) {
      this.latlng = event.latlng;
    },
    filteredMedias() {
      this.AdsDetail.photos.filter(item => {
        if (item.priority === 1 || item.priority === 2) {
          this.images_preview.push(item)
        }
        if(item.priority === 3){
          this.serviceVideos.push(item)

        }
      })
    },
    time_ago(time) {
      switch (typeof time) {
        case 'number':
          break;
        case 'string':
          time = +new Date(time);
          break;
        case 'object':
          if (time.constructor === Date) time = time.getTime();
          break;
        default:
          time = +new Date();
      }
      let time_formats = [
        [60, 'ثانیه ', 1], // 60
        [120, '1 دقیقه پیش', 'یک دقیقه از الان'], // 60*2
        [3600, 'دقیقه ', 60], // 60*60, 60
        [7200, '1 ساعت پیش', '1 ساعت از الان'], // 60*60*2
        [86400, 'ساعت ', 3600], // 60*60*24, 60*60
        [172800, 'دیروز', 'فردا'], // 60*60*24*2
        [604800, 'روز ', 86400], // 60*60*24*7, 60*60*24
        [1209600, 'هفته پیش', 'هفته بعد'], // 60*60*24*7*4*2
        [2419200, 'هفته', 604800], // 60*60*24*7*4, 60*60*24*7
        [4838400, 'ماه پیش', 'ماه بعد'], // 60*60*24*7*4*2
        [29030400, 'ماه', 2419200], // 60*60*24*7*4*12, 60*60*24*7*4
        [58060800, 'سال پیش', 'سال بعد'], // 60*60*24*7*4*12*2
        [2903040000, 'سال', 29030400], // 60*60*24*7*4*12*100, 60*60*24*7*4*12

      ];
      let seconds = (+new Date() - time) / 1000,
        token = 'پیش',
        list_choice = 1;

      if (seconds == 0) {
        return 'همین حالا'
      }
      if (seconds < 0) {
        seconds = Math.abs(seconds);
        token = 'از الان';
        list_choice = 2;
      }
      let i = 0,
        format;
      while (format = time_formats[i++])
        if (seconds < format[0]) {
          if (typeof format[2] == 'string')
            return format[list_choice];
          else
            return Math.floor(seconds / format[2]) + ' ' + format[1] + ' ' + token;
        }
      return time;
    },

    async createWishList(id){
      try {
        await this.$repositories.createWishList.createWishList({
          objectId: id,
          type: 3,
          userId: this.$auth.user.userInfo.userId,
          createDate: new Date(Date.now())
        })
        this.$toast.success("خدمت به علاقمندی ها افزوده شد.");
        this.$nuxt.refresh();


      }
      catch (error){
        console.log(error)
      }

    },
    async deleteWishList(id){
      try {
        await this.$repositories.deleteWishList.deleteWishList({
          objectId: id,
          type: 3,
          userId: this.$auth.user.userInfo.userId,
          createDate: new Date(Date.now())
        })
        this.$toast.success("خدمت از علاقمندی ها حذف شد");
        this.$nuxt.refresh();
      }
      catch (error){
        console.log(error)
      }

    },
    showMoreDiv(){
      this.show_more = !this.show_more
    },
  },
  mounted() {
  },
  watch:{
  },
  props:{
    AdsDetail:{
      type: Object,
      required: true
    }
  }
}
</script>

<style>
.comments_section_height{
  height: 300px;
  overflow-y: scroll;
  border-bottom: 1px solid #e5e7eb;
}
.service_name{
  font-size: 16px;
  font-weight: 600;
}
.leaflet-top, .leaflet-bottom {
  z-index: 999 !important;
}

</style>
