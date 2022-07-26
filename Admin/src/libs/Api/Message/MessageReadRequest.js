import WebServiceRequest from '../../Api/WebServiceRequest'

class MessageReadRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Message/ReadMessage?userId='+id)
    }
}
export default MessageReadRequest;
