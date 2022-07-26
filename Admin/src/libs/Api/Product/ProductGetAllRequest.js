import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductGetAllRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('Product/GetAll?categoryId='+data.categoryId+'&searchByTitle='+data.searchByTitle+'&pageNumber='+data.pageNumber+'&count='+data.count+'&type='+data.type+'&isFree='+data.isFree)
  }
}
export default ProductGetAllRequest;
