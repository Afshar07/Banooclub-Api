import WebServiceRequest from '../../Api/WebServiceRequest'

class ServiceCategoryGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`ServiceCategory/GetAll?pageNumber=${data.pageNumber}&count=10&searchCommand=${data.search}`)
    }
}
export default ServiceCategoryGetAllRequest;
