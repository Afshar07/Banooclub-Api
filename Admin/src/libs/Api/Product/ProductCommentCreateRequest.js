import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCommentCreateRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('ProductComment/Create')
    super.setRequestParam('isDeleted',false)
  }
  setProductId(productId){
    super.setRequestParam('productId',productId)
  }
  setMessage(message){
    super.setRequestParam('message',message)
  }
  setParentId(parentId){
    super.setRequestParam('parentId',parentId)
  }
  setProductCommentId(productCommentId){
    super.setRequestParam('productCommentId',productCommentId)
  }

}
export default ProductCommentCreateRequest;
