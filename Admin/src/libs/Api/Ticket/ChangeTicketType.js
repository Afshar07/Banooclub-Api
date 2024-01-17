import WebServiceRequest from '../../Api/WebServiceRequest'

class TicketCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)

    }

        setParams(data){
            super.setTag(`Ticket/ChangeTicketType?parentId=${data.parentId}&ticketType=${data.ticketType}`)
        }

}
export default TicketCreateRequest;
