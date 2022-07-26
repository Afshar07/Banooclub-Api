import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Order/CreateByAdmin')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default OrderCreateRequest;
