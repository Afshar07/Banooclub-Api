

import WebServiceRequest from '../../Api/WebServiceRequest'

class AddPostNk extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('PostNk/Create');
  }
  setData(data){
    super.setRequestParamDataObj(data)
  }
}
export default AddPostNk;


