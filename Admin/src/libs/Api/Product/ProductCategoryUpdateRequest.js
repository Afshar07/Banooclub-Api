import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ProductCategory/Update')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCategoryUpdateRequest;
