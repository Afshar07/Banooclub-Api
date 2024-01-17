import WebServiceRequest from '../../Api/WebServiceRequest'

class ForumCategoryCreateRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('ForumCategory/Create')
  }
  setParams(data){
    super.setRequestParamDataObj(data)
  }
}
export default ForumCategoryCreateRequest;
