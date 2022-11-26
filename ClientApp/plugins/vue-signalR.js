import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";

export default ({ app, store }, inject) => {
  const hub = new HubConnectionBuilder()
    .withUrl("https://banoclubapi.com/chatHub", {
      accessTokenFactory: function () {
        console.log("Getting token for hub");
      },
    })
    .configureLogging(LogLevel.Information)
    .build();
  const PostHub = new HubConnectionBuilder()
    .withUrl("https://banoclubapi.com/PostHub", {
      accessTokenFactory: function () {
        console.log("Getting token for hub");
      },
    })
    .configureLogging(LogLevel.Information)
    .build();
  inject("hub", hub,PostHub);
  hub.on("SendMessage", (res) => {
    store.commit("sendMessage", res);
    console.log("Message sended by", res);

  });
  hub.on("ReadMessage", (res) => {
    store.commit("sendMessage", res);
    console.log("Message readed by", res);
  });
  hub.on("DeliverMessage", (res) => {
    store.commit("sendMessage", res);
    console.log(`Message Delivered to ${res}`);
  });
  PostHub.on('PostCreated',(res)=>{
    store.commit("PostCreate", res);
    console.log("Post Created By ", res);
  });

  hub.start().catch(function (err) {
    return console.error(err);
  });
  PostHub.start().catch(function (err) {
    return console.error(err);
  });


  // with reconnect capability (async/await, not IE11 compatible)
  async function start() {
    try {
      console.log("Attempting reconnect");
      await hub.start();
      await PostHub.start();
    } catch (err) {
      console.log(err);
      setTimeout(() => start(), 5000);
    }
  }

  hub.onclose(async () => {
    await start();
  });
  PostHub.onclose(async () => {
    await start();
  });

  // sample sending of message
  /*
  hub
    .invoke('SendMessageToOthers', {
      action: action,
      data: Object.assign(
        { user: get(store, 'getters.loggedUser.name') },
        message
      )
    })
    .catch(err => console.error(err.toString()))
  */
};
