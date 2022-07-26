import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCourseGetByIdRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Course/Get?courseId='+id)
    }
}
export default ProductCourseGetByIdRequest;
