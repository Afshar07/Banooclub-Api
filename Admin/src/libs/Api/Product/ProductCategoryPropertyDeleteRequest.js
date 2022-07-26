import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryPropertyDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('CategoryProperty/Delete?id='+id)
    }
}
export default ProductCategoryPropertyDeleteRequest;
