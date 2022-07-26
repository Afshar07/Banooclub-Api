export const useInputImageRenderer = (inputEl, callback,previewEl) => {
  const inputImageRenderer = () => {
    const file = inputEl.value.files[0]
    previewEl = URL.createObjectURL(file)
    const reader = new FileReader()
    reader.readAsDataURL(file)

    reader.addEventListener(
      'load',
      () => {
        callback(reader.result.split(",")[1],previewEl)
      },
      false,
    )
  }
  return {
    inputImageRenderer,
  }
}

export const _ = null
