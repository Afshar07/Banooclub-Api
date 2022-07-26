import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCourseGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Course/GetAll?pageNumber='+data.pageNumber+'&searchCommand='+data.searchCommand+'&count='+data.count)
    }
}
export default ProductCourseGetAllRequest;
