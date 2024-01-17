import WebServiceRequest from '../../Api/WebServiceRequest'

class TicketGetAllByParentIdRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Ticket/GetAllByParentId?parentId='+id)
    }
}
export default TicketGetAllByParentIdRequest;
