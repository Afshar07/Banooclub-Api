var webpack = require("webpack");
module.exports = {
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    title: "",
    titleTemplate: "%s  | بانو کلاب",
    htmlAttrs: {
      lang: "fa",
    },
    meta: [
      {charset: "utf-8"},
      {name: "viewport", content: "width=device-width, initial-scale=1"},
      {hid: "description", name: "description", content: ""},
      {name: "format-detection", content: "telephone=no"},
      {name: 'enamad', content: '622778'}
    ],
    link: [
      {rel: "icon", type: "image.png", href: "/favicon.ico"},
      {
        rel: "stylesheet",
        href: "https://fonts.googleapis.com/css2?family=Oxygen&display=swap",
      },
      {rel: 'stylesheet', href: "/bootstrap.css"},
      {
        href: "https://use.fontawesome.com/releases/v5.13.0/css/all.css",
        rel: "stylesheet",
      },
    ],
    script: [
      {src: '/popper.js'},
      {src: '/bootstrap.js'},
    ]
  },

  loading: "~/components/LoadingBar",
  target: "server",

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [

    "~/assets/css/publicFonts.css",
    "~/assets/css/style.css",
    "@fortawesome/fontawesome-svg-core/styles.css",
    "~/node_modules/bootstrap/dist/css/bootstrap.min.css",
    "@fortawesome/fontawesome-svg-core/styles.css",
    "~/node_modules/swiper/css/swiper.css",
    "spinkit/spinkit.min.css",
  ],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
    "~/plugins/getSsr.js",
    "~/plugins/utils.js",
    "~/plugins/auth.js",
    {src: "~/plugins/fontawesome.js", mode: "client"},
    {src: "~/plugins/vue-star-rate.js", mode: "client"},
    "~/plugins/tooltip.js",
    "~/plugins/vue-jalali-moment.js",
    "~/plugins/click-out-to-close",
    "~/plugins/api-repository.js",
    {src: "~/plugins/nuxt-swiper-plugin.js", mode: "client"},
    {src: "~/plugins/datePicker", mode: "client", ssr: false},
    {src: "~/plugins/v-select", mode: "client"},
    {src: "~/plugins/leaflet", mode: "client"},
    {src: "~plugins/vue-awesome-counter", ssr: false, mode: 'client'},
    {src: "~/plugins/vuex-persist", ssr: false},
    {src: "~/plugins/chart.js", mode: "client"},
    {src: "plugins/vue-context", mode: "client", ssr: false},
    {src: "plugins/vue-signalR", ssr: false},
    {src: "plugins/vue-countdown", mode: "client", ssr: false},

  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: [
    {
      path: "~/components",
      pathPrefix: false,
    }
  ],

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules:
    [
      'nuxt-webpack-optimisations',
      "nuxt-compress",
      "@nuxtjs/fontawesome",
      '@nuxt/postcss8',
    ],

  router: {
    middleware: ["auth"],
    extendRoutes(routes, resolve) {
      routes.push(
        {
          path: "/Blog/BlogDetail/:blogId/:seourl",
          components: {
            default: resolve(__dirname, "pages/Blog/BlogDetail/_id"),
          },
        },
        {
          path: "/consultation/PrePay/:id/:consultantId",
          components: {
            default: resolve(__dirname, "pages/consultation/PrePay/_id"),
          },
        },
      );
    },
  },

  modules: [
    "@nuxtjs/recaptcha",
    "@nuxtjs/auth-next",
    "nuxt-lazy-load",
    'nuxt-compress',
    [
      "nuxt-fontawesome",
      {
        imports: [
          {
            set: "@fortawesome/free-solid-svg-icons",
            icons: ["fas"],
          },
          {
            set: "@fortawesome/free-brands-svg-icons",
            icons: ["fab"],
          },
        ],
      },
    ],
    "@nuxtjs/axios",
    "@nuxtjs/toast",

  ],
  'nuxt-compress': {
    gzip: {
      threshold: 8192,
    },
    brotli: {
      threshold: 8192,
    },
  },
  recaptcha: {
    // Recaptcha language (v2)
    siteKey: "6LdvcL0eAAAAADYvJ9mJ4AHh2GFCuFCOaurYguqL",
    version: 2, // Site key for requests
  },
  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
    // extractCSS: true,
    // optimization: {
    //   splitChunks: {
    //     cacheGroups: {
    //       styles: {
    //         name: 'styles',
    //         test: /\.(css|vue)$/,
    //         chunks: 'all',
    //         enforce: true
    //       }
    //     }
    //   }
    // },
    publicPath: "/nuxt/",
    plugins: [
      new webpack.ProvidePlugin({
        "window.Quill": "quill/dist/quill.js",
        Quill: "quill/dist/quill.js",
      }),
    ],
    postcss: {
      plugins: {
        tailwindcss: {},
        autoprefixer: {},
      },
    },
  },
  toast: {
    theme: "outline",
    position: "bottom-right",
    duration: 5000,
  },
  auth: {
    resetOnError: true,
    strategies: {
      local: {
        tokenRequired: true,
        token: {
          property: "data.token",
          global: true,
          type: false,
          maxAge: process.env.TOKEN_MAX_AGE || 15780000, // 6 months in second
        },
        user: {
          property: "",
        },
        endpoints: {
          login: {
            url: "/Account/SignIn",
            method: "POST",
          },
          logout: false,
          user: {
            url: "Common/GetIndexData",
            method: "POST",
          },
        },
      },
    },
    redirect: {
      login: "/",
      home: "/social/accountsetting/MyPage",
      logout: "/",
      admins: false,
    },
  },

  axios: {
    plugins: ["~/plugins/auth.js"],
    credentials: true,
    baseURL: "https://api.banooclub.simagar.com/api/",
    // baseURL: 'https://localhost:44330/',
  },
  env: {
    pic: "https://api.banooclub.simagar.com",
  },
};
