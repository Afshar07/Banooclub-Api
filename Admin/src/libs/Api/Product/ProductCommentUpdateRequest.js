import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCommentUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ProductComment/Update')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCommentUpdateRequest;
