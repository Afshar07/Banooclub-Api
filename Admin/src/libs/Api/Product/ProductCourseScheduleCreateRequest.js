import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCourseScheduleCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('CourseSchedule/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCourseScheduleCreateRequest;
