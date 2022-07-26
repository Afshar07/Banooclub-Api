import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductDeleteProductRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Product/Deleteproduct?productId='+id)
    }
}
export default ProductDeleteProductRequest;
