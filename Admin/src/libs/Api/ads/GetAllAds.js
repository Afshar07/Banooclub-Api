import WebServiceRequest from '../../Api/WebServiceRequest'

class GetAllAds extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('Ads/GetAll?search='+data.search+'&pageNumber='+data.pageNumber+'&count='+data.count);
  }
}
export default GetAllAds;
