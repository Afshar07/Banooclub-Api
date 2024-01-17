import WebServiceRequest from '../../Api/WebServiceRequest'

class TicketDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Ticket/DeleteTicket?ticketId='+id)
    }
}
export default TicketDeleteRequest;
