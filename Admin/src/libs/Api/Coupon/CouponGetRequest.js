import WebServiceRequest from '../../Api/WebServiceRequest'

class CouponGetRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Coupon/Get?couponId='+id)
    }
}
export default CouponGetRequest;
