import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCommentGetAllAsyncByProductIdRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('ProductComment/GetAllAsyncByProductId?productId='+id)
  }
}
export default ProductCommentGetAllAsyncByProductIdRequest;
