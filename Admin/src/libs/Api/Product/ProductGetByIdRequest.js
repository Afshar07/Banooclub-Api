import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductGetByIdRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('Product/GetById?id='+id)
  }
}
export default ProductGetByIdRequest;
