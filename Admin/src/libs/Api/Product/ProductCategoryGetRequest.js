import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryGetRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('ProductCategory/Get?productCategoryId='+id)
  }
}
export default ProductCategoryGetRequest;
