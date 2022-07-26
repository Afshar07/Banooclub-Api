import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryPropertyGetRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('CategoryProperty/Get?categoryPropertyId='+id)
    }
}
export default ProductCategoryPropertyGetRequest;
