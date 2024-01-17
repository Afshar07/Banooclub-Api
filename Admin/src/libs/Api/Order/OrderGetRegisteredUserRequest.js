import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderGetRegisteredUserRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Order/GetRegisteredUser?productId='+data.productId+'&pageNumber='+data.pageNumber+'&count='+data.count)
    }
}
export default OrderGetRegisteredUserRequest;
