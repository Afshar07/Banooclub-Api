import WebServiceRequest from '../../Api/WebServiceRequest'

class ServiceCategoryDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('ServiceCategory/Delete?id='+id)
    }
}
export default ServiceCategoryDeleteRequest;
