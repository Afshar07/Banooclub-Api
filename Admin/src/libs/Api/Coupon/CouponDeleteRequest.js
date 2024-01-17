import WebServiceRequest from '../../Api/WebServiceRequest'

class CouponDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Coupon/Delete?id='+id)
    }
}
export default CouponDeleteRequest;
