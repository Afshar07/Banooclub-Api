import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryGetAllRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('ProductCategory/GetAll?pageNumber='+data.pageNumber+'&count='+data.count+'')
  }
}
export default ProductCategoryGetAllRequest;
