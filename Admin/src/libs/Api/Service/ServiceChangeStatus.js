



import WebServiceRequest from '../../Api/WebServiceRequest'

class ServiceChangeStatusRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`ServicePack/ChangeServiceStatus?servicePackId=${data.serviceId}&status=${data.status}`)
    }
}
export default ServiceChangeStatusRequest;
