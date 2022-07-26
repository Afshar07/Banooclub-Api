import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderItemGetRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('OrderItem/Get/orderItemId='+id)
    }
}
export default OrderItemGetRequest;
