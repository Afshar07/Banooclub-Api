import WebServiceRequest from '../../Api/WebServiceRequest'

class GetAdsCount extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Ads/GetAdsCount')
    }

}
export default GetAdsCount;
