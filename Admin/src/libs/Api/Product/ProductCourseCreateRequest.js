import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCourseCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Course/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCourseCreateRequest;
