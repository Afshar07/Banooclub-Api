

import WebServiceRequest from '../../Api/WebServiceRequest'

class UpdatePostNk extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('PostNk/Update');
  }
  setData(data){
    super.setRequestParamDataObj(data)
  }
}
export default UpdatePostNk;


