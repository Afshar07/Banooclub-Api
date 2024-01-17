import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCommentDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('ProductComment/Delete?productCommentId='+id)
    }
}
export default ProductCommentDeleteRequest;
