import WebServiceRequest from '../../Api/WebServiceRequest'

class MessageDeliverRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Message/DeliverMessage')
    }
}
export default MessageDeliverRequest;
