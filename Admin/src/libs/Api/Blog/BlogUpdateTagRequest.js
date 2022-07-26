

import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogTagUpdateRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('Tag/Update');
  }
  setData(data){
    super.setRequestParamDataObj(data)
  }
}
export default BlogTagUpdateRequest;


