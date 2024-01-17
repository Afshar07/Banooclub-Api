import WebServiceRequest from '../../Api/WebServiceRequest'

class CloseTicket extends WebServiceRequest {
    constructor (context) {
        super(context)

    }

    setParams(data){
        super.setTag(`Ticket/CloseTicket?parentId=${data.parentId}`)
    }

}
export default CloseTicket;
