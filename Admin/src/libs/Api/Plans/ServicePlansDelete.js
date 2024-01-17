import WebServiceRequest from '../../Api/WebServiceRequest'

class ServicePlanDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Plan/Delete?id='+id)
    }
}
export default ServicePlanDeleteRequest;
