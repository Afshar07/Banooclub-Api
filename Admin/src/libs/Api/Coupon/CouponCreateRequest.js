import WebServiceRequest from '../../Api/WebServiceRequest'

class CouponCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Coupon/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default CouponCreateRequest;
