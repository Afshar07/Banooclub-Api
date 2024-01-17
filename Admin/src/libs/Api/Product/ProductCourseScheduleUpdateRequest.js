import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCourseScheduleUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('CourseSchedule/Update')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default ProductCourseScheduleUpdateRequest;
