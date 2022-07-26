import WebServiceRequest from '../../Api/WebServiceRequest'

class TicketGetAll extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Ticket/GetAll?pageNumber='+data.pageNumber+'&count='+data.count+'')
    }
}
export default TicketGetAll;
