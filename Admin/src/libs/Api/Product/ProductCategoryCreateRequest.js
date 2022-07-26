import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ProductCategory/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCategoryCreateRequest;
