import WebServiceRequest from '../../Api/WebServiceRequest'

class PaymentGetRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setUserId(id){
        super.setTag('Payment/Get?id='+id)
    }
}
export default PaymentGetRequest;
