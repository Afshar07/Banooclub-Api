import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderItemCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('OrderItem/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default OrderItemCreateRequest;
