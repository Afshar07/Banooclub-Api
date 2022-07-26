<template>
  <b-card
      no-body
      class="mb-0"
  >
<!--    <Header />-->
<!--    <VideoCall ref="videoElements" />-->



    <h2>1. Start your Webcam</h2>
    <div class="videos">
      <span>
        <h3>Local Stream</h3>
        <video id="webcamVideo" autoplay playsinline></video>
      </span>
      <span>
        <h3>Remote Stream</h3>
        <video id="remoteVideo" autoplay playsinline></video>
      </span>


    </div>

    <button @click="webCam" type="button" class="btn btn-primary col-2 mx-auto" id="webcamButton">Start webcam</button>
    <h2>2. Create a new Call</h2>
    <button @click="call" type="button" id="callButton" class="btn btn-primary col-2 mx-auto" disabled>Create Call (offer)</button>

    <h2>3. Join a Call</h2>
    <p>Answer the call from a different browser window or device</p>

    <input id="callInput" type="text" />
    <button @click="answer" type="button" id="answerButton" class="btn btn-primary col-2 mx-auto" >Answer</button>

    <h2>4. Hangup</h2>

<!--    <button id="hangupButton" class="btn btn-primary col-2 mx-auto" disabled>Hangup</button>-->



  </b-card>
</template>

<script>
import {
  BAvatar,
  BBadge,
  BButton,
  BCard,
  BCol,
  BDropdown,
  BDropdownItem,
  BFormInput,
  BLink,
  BMedia, BOverlay, BPagination,
  BRow,
  BTable
} from "bootstrap-vue";
import vSelect from "vue-select";
import VideoCall from "@/views/apps/e-commerce/courses/VideoCall";
import Header from "@/views/apps/e-commerce/courses/Header";
import firebase from 'firebase/app';
import 'firebase/firestore';
import firestore from "@/main";


export default {
  data() {
    return {
      servers : {
        iceServers: [
          {
            urls: ['stun:stun1.l.google.com:19302',
                   // 'stun:stun2.l.google.com:19302'
            ],
          },
        ],
        iceCandidatePoolSize: 10,
      },
      pc : new RTCPeerConnection(this.servers),
      localStream : null,
      remoteStream : null,
    }
  },
  methods: {
    // 1. Setup media sources
    async webCam(){
      let _this =this;
      let webcamVideo = document.getElementById('webcamVideo');
      let remoteVideo = document.getElementById('remoteVideo');
      let callButton = document.getElementById('callButton');
      let answerButton = document.getElementById('answerButton');
      let webcamButton = document.getElementById('webcamButton');

      _this.localStream = await navigator.mediaDevices.getDisplayMedia({ video: true, audio: true });
      // _this.remoteStream = new MediaStream();

      // Push tracks from local stream to peer connection
      _this.localStream.getTracks().forEach((track) => {
        _this.pc.addTrack(track, _this.localStream);
      });

      // Pull tracks from remote stream, add to video stream
      // _this.pc.ontrack = (event) => {
      //   event.streams[0].getTracks().forEach((track) => {
      //     _this.remoteStream.addTrack(track);
      //   });
      // };

      webcamVideo.srcObject = _this.localStream;
      // remoteVideo.srcObject = _this.remoteStream;

      callButton.disabled = false;
      answerButton.disabled = false;
      webcamButton.disabled = true;
    },

    // 2. Create an offer
    async call(){
      let _this = this;
      const callInput = document.getElementById('callInput');
      const hangupButton = document.getElementById('hangupButton');

      // Reference Firestore collections for signaling
      const callDoc = firestore.collection('rooms').doc();
      const offerCandidates = callDoc.collection('offerCandidates');
      const answerCandidates = callDoc.collection('answerCandidates');

      callInput.value = callDoc.id;

      // Get candidates for caller, save to db
      _this.pc.onicecandidate = (event) => {
        event.candidate && offerCandidates.add(event.candidate.toJSON());
      };

      // Create offer
      const offerDescription = await _this.pc.createOffer();
      await _this.pc.setLocalDescription(offerDescription);

      const offer = {
        sdp: offerDescription.sdp,
        type: offerDescription.type,
      };

      await callDoc.set({ offer });

      // Listen for remote answer
      callDoc.onSnapshot((snapshot) => {
        const data = snapshot.data();
        if (!_this.pc.currentRemoteDescription && data?.answer) {
          const answerDescription = new RTCSessionDescription(data.answer);
          _this.pc.setRemoteDescription(answerDescription);
        }
      });

      // When answered, add candidate to peer connection
      answerCandidates.onSnapshot((snapshot) => {
        snapshot.docChanges().forEach((change) => {
          if (change.type === 'added') {
            const candidate = new RTCIceCandidate(change.doc.data());
            _this.pc.addIceCandidate(candidate);
          }
        });
      });

      // hangupButton.disabled = false;
    },

    // 3. Answer the call with the unique ID
    async answer(){
      let _this = this;
      const callInput = document.getElementById('callInput');
      let remoteVideo = document.getElementById('remoteVideo');

      // Test
      _this.remoteStream = new MediaStream();
      // Pull tracks from remote stream, add to video stream
      _this.pc.ontrack = (event) => {
        event.streams[0].getTracks().forEach((track) => {
          _this.remoteStream.addTrack(track);
        });
      };
      remoteVideo.srcObject = _this.remoteStream;



      const callId = callInput.value;
      const callDoc = firestore.collection('rooms').doc(callId);
      const answerCandidates = callDoc.collection('answerCandidates');
      const offerCandidates = callDoc.collection('offerCandidates');

      _this.pc.onicecandidate = (event) => {
        event.candidate && answerCandidates.add(event.candidate.toJSON());
      };

      const callData = (await callDoc.get()).data();

      const offerDescription = callData.offer;
      await _this.pc.setRemoteDescription(new RTCSessionDescription(offerDescription));

      const answerDescription = await _this.pc.createAnswer();
      await _this.pc.setLocalDescription(answerDescription);

      const answer = {
        type: answerDescription.type,
        sdp: answerDescription.sdp,
      };

      await callDoc.update({ answer });

      offerCandidates.onSnapshot((snapshot) => {
        snapshot.docChanges().forEach((change) => {
          console.log(change);
          if (change.type === 'added') {
            let data = change.doc.data();
            _this.pc.addIceCandidate(new RTCIceCandidate(data));
          }
        });
      });
    },




    // startPublish(type) {
    //   if (type) {
    //     LocalStream.getUserMedia({
    //       resolution: "vga",
    //       audio: true,
    //       codec: "vp8"
    //     }).then((stream) => {
    //       this.$refs.videoElements.$refs.pub_video.srcObject = stream;
    //       client.publish(stream);
    //     }).catch(console.error);
    //   } else {
    //     LocalStream.getDisplayMedia({
    //       resolution: "vga",
    //       video: true,
    //       audio: true,
    //       codec: "vp8"
    //     }).then((stream) => {
    //       this.$refs.videoElements.$refs.pub_video.srcObject = stream;
    //       client.publish(stream);
    //     }).catch(console.error);
    //   }
    // }
  },
  name: "CourseStream",
  components:{
    Header,
    VideoCall,

    BCard,
    BRow,
    BCol,
    BFormInput,
    BButton,
    BTable,
    BMedia,
    BAvatar,
    BLink,
    BBadge,
    BDropdown,
    BDropdownItem,
    BPagination,
    BOverlay,

    vSelect,
  },
}
</script>

<style >
video {
  width: 40vw;
  height: 30vw;
  margin: 2rem;
  background: #2c3e50;
}

.videos {
  display: flex;
  align-items: center;
  justify-content: center;
}
@import "https://unpkg.com/tailwindcss@^2/dist/tailwind.min.css";
</style>