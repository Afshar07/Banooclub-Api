import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCommentGetByIdRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('ProductComment/GetById?productCommentId='+id)
    }
}
export default ProductCommentGetByIdRequest;
