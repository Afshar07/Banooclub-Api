import WebServiceRequest from '../../Api/WebServiceRequest'

class GetAllPostNk extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('PostNK/GetAll?search='+data.search+'&pageNumber='+data.pageNumber+'&count='+data.count);
  }
}
export default GetAllPostNk;
