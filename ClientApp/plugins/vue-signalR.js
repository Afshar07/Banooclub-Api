import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";

export default ({ app, store }, inject) => {
  const hub = new HubConnectionBuilder()
    .withUrl("https://pplusapi.simagar.com/chatHub", {
      accessTokenFactory: function () {
        console.log("Getting token for hub");
      },
    })
    .configureLogging(LogLevel.Information)
    .build();

  inject("hub", hub);

  // hub.on('Connected', message => {
  //   console.log('Connected to SignalR Hub.', message)
  // })
  //
  // hub.on('Disconnected', message => {
  //   console.warn('Disconnected from SignalR Hub.', message)
  // })

  hub.on("SendMessage", (res) => {
    store.commit("sendMessage", res);
    console.log("Message sended by", res);
    // const { audience, group, action, data } = res
    // const { action, data } = res
    // switch (action) {
    //   case 'bookmark_created':
    //   case 'bookmark_updated':
    //     store.commit('addOrUpdateBookmark', data.bookmark)
    //     break
    //   case 'bookmark_deleted':
    //     store.commit('removeBookmark', data.bookmark.id)
    //     break
    //   default:
    //     break
    // }
  });
  hub.on("ReadMessage", (res) => {
    store.commit("sendMessage", res);

    // store.commit("sendMessage", res);
    console.log("Message readed by", res);
    // const { audience, group, action, data } = res
    // const { action, data } = res
    // switch (action) {
    //   case 'bookmark_created':
    //   case 'bookmark_updated':
    //     store.commit('addOrUpdateBookmark', data.bookmark)
    //     break
    //   case 'bookmark_deleted':
    //     store.commit('removeBookmark', data.bookmark.id)
    //     break
    //   default:
    //     break
    // }
  });
  hub.on("DeliverMessage", (res) => {
    store.commit("sendMessage", res);

    console.log(`Message Delivered to ${res}`);
  });

  hub.start().catch(function (err) {
    return console.error(err);
  });

  // with reconnect capability (async/await, not IE11 compatible)
  async function start() {
    try {
      console.log("Attempting reconnect");
      await hub.start();
    } catch (err) {
      console.log(err);
      setTimeout(() => start(), 5000);
    }
  }

  hub.onclose(async () => {
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
