import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Order/GetAll?pageNumber='+data.pageNumber+'&count='+data.count+'&searchCommand='+data.search)
    }
}
export default OrderGetAllRequest;
