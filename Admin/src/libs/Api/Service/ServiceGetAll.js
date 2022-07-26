import WebServiceRequest from '../../Api/WebServiceRequest'

class ServiceGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`ServicePack/GetAll?pageNumber=${data.pageNumber}&count=10&searchCommand=${data.search}&status=${data.status}`)
    }
}
export default ServiceGetAllRequest;
