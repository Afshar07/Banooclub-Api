<template>
  <section class="container">
    <client-only>
      <quill-editor
        class="quill-editor"
        ref="editor"
        id="editor"
        :content="this.$props.content"
        v-model="editorContent"
        :options="editorOption"
        @blur="onEditorBlur($event)"
        @focus="onEditorFocus($event)"
        @ready="onEditorReady($event)"
        @change="onEditorChange($event)"
      />
    </client-only>
  </section>
</template>

<script>
import "quill-emoji/dist/quill-emoji.css";

export default {
  name: "quill-example-nuxt",
  data() {
    return {
      editorContent: this.$props.content === null ? "" : this.$props.content,
      editorOption: {
        // Some Quill options...
        theme: "snow",
        modules: {
          imageCompress: {
            quality: 0.7, // default
            maxWidth: 500,
            maxHeight: 500,
            imageType: "image/jpeg", // default
            debug: true, // default
            suppressErrorLogging: false, // default
          },
          imageResize: {
            // See optional "config" below
          },
          toolbar: [
            ["bold", "italic", "underline", "strike"],
            ["blockquote", "code-block"],
            [{ header: 1 }, { header: 2 }],
            [{ list: "ordered" }, { list: "bullet" }],
            [{ script: "sub" }, { script: "super" }],
            [{ indent: "-1" }, { indent: "+1" }],
            [{ direction: "rtl" }],
            [{ size: ["small", false, "large", "huge"] }],
            [{ header: [1, 2, 3, 4, 5, 6, false] }],
            [{ font: [] }],
            [{ color: [] }, { background: [] }],
            [{ align: [] }],
            ["clean"],
            ["link", "image", "video"],
            ["emoji"],
          ],
        },
      },
    };
  },
  props: {
    content: {
      type: String,
      required: false,
    },
  },
  mounted() {
    // console.log('App inited, the Quill instance object is:', this.$refs.editor)
    // console.log('this is Quill instance:', this.$props.content)
    this.editorContent =
      this.$props.content === null ? "" : this.$props.content;
  },
  methods: {
    onEditorChange(editor) {
      // console.log('editor onChange!', editor)
      // this.content = value.html
      // console.log('this.content')
      // console.log(this.content)
      this.$emit("content", this.editorContent);
    },
    onEditorBlur(editor) {
      // console.log('editor blur!', editor)
    },
    onEditorFocus(editor) {
      // console.log('editor focus!', editor)
    },
    onEditorReady(editor) {
      // console.log('editor ready!', editor)
    },
  },
};
</script>

<style scoped>
.container {
  width: 100%;
  margin: 0 auto;
  padding: 0;
}

.container .quill-editor {
  min-height: auto;
  max-height: 400px;
  overflow-y: auto;
  border: none;
}

.ql-blank {
  min-height: 300px !important;
}
</style>
