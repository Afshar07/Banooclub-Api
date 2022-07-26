import WebServiceRequest from '../../Api/WebServiceRequest'

class MessageGetMenuRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setRequestParam(search) {
        super.setTag('Message/GetMenu?searchCommand='+search)
    }
}
export default MessageGetMenuRequest;
