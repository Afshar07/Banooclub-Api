import WebServiceRequest from '../../Api/WebServiceRequest'

class PaymentDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setUserId(id){
        super.setTag('Payment/Delete?id='+id)
    }
}
export default PaymentDeleteRequest;
