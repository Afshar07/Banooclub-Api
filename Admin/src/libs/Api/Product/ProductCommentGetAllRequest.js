import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCommentGetAllRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('ProductComment/GetAll?pageNumber='+data.pageNumber+'&count='+data.count+'')
  }
}
export default ProductCommentGetAllRequest;
