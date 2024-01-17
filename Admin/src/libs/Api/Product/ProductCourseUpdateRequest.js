import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCourseUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Course/Update')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCourseUpdateRequest;
