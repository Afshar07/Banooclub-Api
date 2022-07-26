import WebServiceRequest from '../../Api/WebServiceRequest'

class ProductCourseDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Course/Delete?id='+id)
    }
}
export default ProductCourseDeleteRequest;
