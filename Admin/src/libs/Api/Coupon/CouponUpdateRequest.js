import WebServiceRequest from '../../Api/WebServiceRequest'

class CouponUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Coupon/Update')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default CouponUpdateRequest;
