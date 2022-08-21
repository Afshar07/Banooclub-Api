<template>
  <div
    class="tw-bg-stone-200 tw-w-full  tw-rounded tw-flex-wrap tw-shadow tw-flex tw-items-center  tw-overflow-x-scroll ">
    <span class="tw-cursor-pointer tw-p-2 hover:tw-bg-stone-400 hover:tw-shadow hover:tw-rounded"
          @click="SelectedEmoji(item)" v-for="item in Emojis">{{ String.fromCodePoint(item.AsciiCode) }}</span>
  </div>
</template>

<script>
export default {
  name: "EmojiPicker",
  data() {
    return {
      Emojis: [],

    }
  },

  mounted() {
    this.GenerateEmojiArray()
  },

  methods: {
    SelectedEmoji(emoji) {
      // this Event Sends Selected Emoji Object
      //Emoji Object Interface
      //     {
      //   emojiId:0,
      //     emoji:'',
      //     AsciiCode:'' eg. 0x1F929
      // }

      this.$emit('SendEmoji', emoji)

    },
    GenerateEmojiArray() {
      // Creating Emojis List. Note that both Array length Must Be Equal Or Everything Will Get...
      let emojis = ['😀', '😄', '😁', '😆', '😅', '😂', '🤣', '🙂', '🙃', '😉', '😇', '🥰', '😄', '😍', '🤩', '😘', '😗', '☺', '😚', '😙', '😜', '🤪', '😝', '🤫', '🤐', '😑', '😶', '🙄', '😬', '😔',
        '😤', '😴', '🤢', '🤮', '😎', '😕', '🙁', '😲', '😳', '🥺', '😭', '😢', '😥', '😖', '😡', '🤬', '😈', '💩', '😹', '💜', '💙', '💓', '🖤', '💔']
      let AsciiCodes = ['0x1F600', '0x1F604', '0x1F601', '0x1F606', '0x1F605', '0x1F602', '0x1F923', '0x1F642', '0x1F643', '0x1F609', '0x1F607',
        '0x1F970', '0x1F604', '0x1F60D', '0x1F929', '0x1F618', '0x1F617', '0x263A', '0x1F61A', '0x1F619', '0x1F61C', '0x1F92A', '0x1F61D',
        '0x1F92B', '0x1F610', '0x1F611', '0x1F636', '0x1F644', '0x1F62C', '0x1F614', '0x1F624', '0x1F634', '0x1F922', '0x1F92E', '0x1F60E',
        '0x1F615', '0x1F641', '0x1F632', '0x1F633', '0x1F97A', '0x1F62D', '0x1F622', '0x1F625', '0x1F616', '0x1F621', '0x1F92C',
        '0x1F608', '0x1F4A9', '0x1F639', '0x1F49C', '0x1F499', '0x1F493', '0x1F5A4', '0x1F494']
      let tmpEmojis = []

      let tmpEmoji = {
        emojiId: 0,
        emoji: '',
        AsciiCode: ''
      }
      emojis.forEach((el) => {
        tmpEmoji.emoji = el
        tmpEmoji.emojiId++


        const clone = {...tmpEmoji}
        tmpEmojis.push(clone)
        tmpEmoji.emoji = ''
      })
      AsciiCodes.forEach((e, i) => {
        tmpEmojis[i].AsciiCode = e
      })

      this.Emojis = tmpEmojis
    }

  }
}
</script>

<style scoped>

</style>

