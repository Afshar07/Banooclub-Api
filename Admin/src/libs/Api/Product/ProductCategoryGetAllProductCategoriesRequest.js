import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryGetAllProductCategoriesRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('ProductCategory/GetAllProductCategories?pageNumber='+data.pageNumber+'&count='+data.count+'')
    }
}
export default ProductCategoryGetAllProductCategoriesRequest;
