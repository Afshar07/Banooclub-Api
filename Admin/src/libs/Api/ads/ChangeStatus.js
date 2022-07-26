

import WebServiceRequest from '../../Api/WebServiceRequest'

class ChangeAdStatus extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('Ads/ChangeStatus');
  }
  setData(data){
    super.setRequestParamDataObj(data)
  }
}
export default ChangeAdStatus;


