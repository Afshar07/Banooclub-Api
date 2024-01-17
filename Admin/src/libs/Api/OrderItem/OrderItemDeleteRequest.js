import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderItemDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('OrderItem/Delete/id='+id)
    }
}
export default OrderItemDeleteRequest;
