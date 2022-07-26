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
              'stun:stun2.l.google.com:19302'
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
      const roomRef = await firestore.collection('rooms').doc();
      let remoteVideo = document.getElementById('remoteVideo');
      const callerCandidatesCollection = roomRef.collection('callerCandidates');


      const offer = await _this.pc.createOffer();
      await _this.pc.setLocalDescription(offer);

      const roomWithOffer = {
        offer: {
          type: offer.type,
          sdp: offer.sdp
        }
      }
      await roomRef.set(roomWithOffer);
      let roomId = roomRef.id;
      callInput.value = roomId;

      _this.remoteStream = new MediaStream();
      _this.pc.addEventListener('track', event => {
        console.log('Got remote track:', event.streams[0]);
        event.streams[0].getTracks().forEach(track => {
          console.log('Add a track to the remoteStream:', track);
          _this.remoteStream.addTrack(track);
        });
      });
       remoteVideo.srcObject = _this.remoteStream;

      // Listening for remote session description below
      roomRef.onSnapshot(async snapshot => {
        const data = snapshot.data();
        if (!_this.pc.currentRemoteDescription && data && data.answer) {
          console.log('Got remote description: ', data.answer);
          const rtcSessionDescription = new RTCSessionDescription(data.answer);
          await _this.pc.setRemoteDescription(rtcSessionDescription);
        }
      });
      // Listening for remote session description above

      // Listen for remote ICE candidates below
      roomRef.collection('calleeCandidates').onSnapshot(snapshot => {
        snapshot.docChanges().forEach(async change => {
          if (change.type === 'added') {
            let data = change.doc.data();
            console.log(`Got new remote ICE candidate: ${JSON.stringify(data)}`);
            await _this.pc.addIceCandidate(new RTCIceCandidate(data));
          }
        });
      });
      // Listen for remote ICE candidates above
    },

    // 3. Answer the call with the unique ID
    async answer(){
      let _this = this;
      const callInput = document.getElementById('callInput');
      let remoteVideo = document.getElementById('remoteVideo');
      const callId = callInput.value;
      let peerConnection = _this.pc

      const db = firestore;
      const roomRef = db.collection('rooms').doc(callId);
      const roomSnapshot = await roomRef.get();
      console.log('Got room:', roomSnapshot.exists);

      if (roomSnapshot.exists) {
        console.log('Create PeerConnection with configuration: ', _this.servers);
        peerConnection = new RTCPeerConnection(_this.servers);
        // registerPeerConnectionListeners();
        // _this.localStream.getTracks().forEach(track => {
        //   peerConnection.addTrack(track, _this.localStream);
        // });

        // Code for collecting ICE candidates below
        const calleeCandidatesCollection = roomRef.collection('calleeCandidates');
        peerConnection.addEventListener('icecandidate', event => {
          if (!event.candidate) {
            console.log('Got final candidate!');
            return;
          }
          console.log('Got candidate: ', event.candidate);
          calleeCandidatesCollection.add(event.candidate.toJSON());
        });
        // Code for collecting ICE candidates above

        _this.remoteStream = new MediaStream();
        peerConnection.addEventListener('track', event => {
          console.log('Got remote track:', event.streams[0]);
          event.streams[0].getTracks().forEach(track => {
            console.log('Add a track to the remoteStream:', track);
            _this.remoteStream.addTrack(track);
          });
          remoteVideo.srcObject = _this.remoteStream;
        });

        // Code for creating SDP answer below
        const offer = roomSnapshot.data().offer;
        console.log('Got offer:', offer);
        await peerConnection.setRemoteDescription(new RTCSessionDescription(offer));
        const answer = await peerConnection.createAnswer();
        console.log('Created answer:', answer);
        await peerConnection.setLocalDescription(answer);

        const roomWithAnswer = {
          answer: {
            type: answer.type,
            sdp: answer.sdp,
          },
        };
        await roomRef.update(roomWithAnswer);


        // Code for creating SDP answer above

        // Listening for remote ICE candidates below
        roomRef.collection('callerCandidates').onSnapshot(snapshot => {
          snapshot.docChanges().forEach(async change => {
            if (change.type === 'added') {
              let data = change.doc.data();
              console.log(`Got new remote ICE candidate: ${JSON.stringify(data)}`);
              await peerConnection.addIceCandidate(new RTCIceCandidate(data));
            }
          });
        });
        // Listening for remote ICE candidates above
      }
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
  name: "CourseStream2",
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