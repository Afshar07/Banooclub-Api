import Vue from "vue";
import VueQuillEditor from "vue-quill-editor";
import Quill from "quill";
import ImageResize from "quill-image-resize-module";
import ImageCompress from 'quill-image-compress';

// import Quill from 'quill'
import "quill/dist/quill.core.css"; // import styles
import "quill/dist/quill.snow.css"; // for snow theme
import "quill/dist/quill.bubble.css"; // for bubble theme
import "quill-emoji/dist/quill-emoji.css";
Quill.register("modules/imageResize", ImageResize);
Quill.register('modules/imageCompress', ImageCompress);


Vue.use(VueQuillEditor);
