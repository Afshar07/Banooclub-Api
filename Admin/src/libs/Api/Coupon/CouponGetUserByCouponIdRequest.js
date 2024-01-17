import WebServiceRequest from '../../Api/WebServiceRequest'

class CouponGetUserByCouponIdRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('UserCoupon/GetUserByCouponId?couponId='+id)
    }
}
export default CouponGetUserByCouponIdRequest;
