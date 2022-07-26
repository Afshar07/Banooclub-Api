import WebServiceRequest from '../../Api/WebServiceRequest'

class TicketCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Ticket/CreateTicket')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default TicketCreateRequest;
