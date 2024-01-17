import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderGetByUserIdRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Order/GetByUserId')
    }
}
export default OrderGetByUserIdRequest;
