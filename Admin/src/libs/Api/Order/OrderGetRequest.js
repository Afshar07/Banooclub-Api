import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderGetRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Order/Get?orderId='+id)
    }
}
export default OrderGetRequest;
