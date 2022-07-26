

import WebServiceRequest from '../../Api/WebServiceRequest'

class ServicePlanUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Plan/Update');
    }
    setData(data){
        super.setRequestParamDataObj(data)
    }
}
export default ServicePlanUpdateRequest;


