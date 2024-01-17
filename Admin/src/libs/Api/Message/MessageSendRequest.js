import WebServiceRequest from '../../Api/WebServiceRequest'

class MessageSendRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Message/SendMessage')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default MessageSendRequest;
