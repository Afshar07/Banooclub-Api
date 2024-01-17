import WebServiceRequest from '../../Api/WebServiceRequest'

class ServiceDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('ServicePack/Delete?id='+id)
    }
}
export default ServiceDeleteRequest;
