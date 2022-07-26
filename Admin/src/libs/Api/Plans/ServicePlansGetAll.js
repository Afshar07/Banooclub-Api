import WebServiceRequest from '../../Api/WebServiceRequest'

class ServicePlansGetAll extends WebServiceRequest {
    constructor (context) {
        super(context)

    }
    setParams(data){
        super.setTag(`Plan/GetAll?pageNumber=${data.pageNumber}&count=10`)
    }
}
export default ServicePlansGetAll;
