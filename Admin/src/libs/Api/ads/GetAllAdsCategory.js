import WebServiceRequest from '../../Api/WebServiceRequest'

class GetAllAdsCategory extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('AdsCategory/GetAll?pageNumber='+data.pageNumber+'&count='+data.count+'&search='+data.search)
  }
}
export default GetAllAdsCategory;
