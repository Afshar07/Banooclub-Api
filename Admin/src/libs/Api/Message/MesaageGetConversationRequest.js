import WebServiceRequest from '../../Api/WebServiceRequest'

class MessageGetConversationRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setData(data){
        super.setTag('Message/GetConversation?userId='+data.userId+'&count='+data.count)
    }
}
export default MessageGetConversationRequest;
