

import WebServiceRequest from '../../Api/WebServiceRequest'

class ForumCategoryUpdateRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('ForumCategory/Update');
  }
  setData(data){
    super.setRequestParamDataObj(data)
  }
}
export default ForumCategoryUpdateRequest;


