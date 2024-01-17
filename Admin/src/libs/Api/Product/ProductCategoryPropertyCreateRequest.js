import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryPropertyCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('CategoryProperty/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCategoryPropertyCreateRequest;
