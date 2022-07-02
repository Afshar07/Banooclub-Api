export default (context, inject) => {
    const getSsr = (env) =>
      env.toString().match(/^(true|[1-9][0-9]*|[0-9]*[1-9]+|yes)$/i)
        ? true
        : false;
    // Inject $hello(msg) in Vue, context and store.
    inject("getSsr", getSsr);
  };
  