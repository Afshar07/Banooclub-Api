import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Product/Updateproduct')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductUpdateRequest;
