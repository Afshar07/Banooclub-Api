import WebServiceRequest from '../../Api/WebServiceRequest'

class CouponGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Coupon/GetAll?pageNumber='+data.pageNumber+'&count='+data.count)
    }
}
export default CouponGetAllRequest;
