import WebServiceRequest from '../../Api/WebServiceRequest'

class ForumCreateRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('Forum/Create')
  }
  setParams(data){
    super.setRequestParamDataObj(data)
  }
}
export default ForumCreateRequest;
