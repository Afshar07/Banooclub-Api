import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryPropertyGetByCategoryIdRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('CategoryProperty/GetByCategoryId?categoryId='+id)
    }
}
export default ProductCategoryPropertyGetByCategoryIdRequest;
