import WebServiceRequest from '../../Api/WebServiceRequest'

class GetAllAdsNotConfirmed extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('Ads/GetNotConfirmed?search='+data.search+'&pageNumber='+data.pageNumber+'&count='+data.count);
  }
}
export default GetAllAdsNotConfirmed;
