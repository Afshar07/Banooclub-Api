import WebServiceRequest from '../../Api/WebServiceRequest'

class PaymentGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Payment/GetAll?pageNumber='+data.pageNumber+'&count='+data.count+'&searchCommand='+data.search)
    }
}
export default PaymentGetAllRequest;
