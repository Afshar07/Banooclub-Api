import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCategoryGetAllCourseCategoriesRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('ProductCategory/GetAllCourseCategories?pageNumber='+data.pageNumber+'&count='+data.count+'')
    }
}
export default ProductCategoryGetAllCourseCategoriesRequest;
