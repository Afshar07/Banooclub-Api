

import WebServiceRequest from '../../Api/WebServiceRequest'

class ServiceCategoryUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ServiceCategory/Update');
    }
    setData(data){
        super.setRequestParamDataObj(data)
    }
}
export default ServiceCategoryUpdateRequest;


