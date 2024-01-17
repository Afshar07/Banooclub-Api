export default ({app}, inject) => {
  inject("utils", {
    makeBase64(file) {
      if (file) {

        return new Promise((resolve, reject) => {
          const fr = new FileReader();
          fr.onerror = reject;

          fr.onload = () => {
            resolve({
              base64: fr.result.split(",")[1],
              pictureUrl: URL.createObjectURL(file)
            });
          };
          fr.readAsDataURL(file);
        });
      }
    },

  });
};
