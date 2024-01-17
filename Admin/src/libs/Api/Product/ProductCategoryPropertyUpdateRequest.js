import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryPropertyUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('CategoryProperty/Update')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCategoryPropertyUpdateRequest;
