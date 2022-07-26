import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('ProductCategory/Delete?id='+id)
    }
}
export default ProductCategoryDeleteRequest;
