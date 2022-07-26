import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCourseScheduleDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('CourseSchedule/Delete?id='+id)
    }
}
export default ProductCourseScheduleDeleteRequest;
