import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Order/Delete?id='+id)
    }
}
export default OrderDeleteRequest;
