import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderChangeStatusRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(data){
        super.setTag('Order/ChangeOrderStatus?orderId='+data.orderId+'&status='+data.status)
    }
}
export default OrderChangeStatusRequest;
