import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Product/Createproduct')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCreateRequest;
