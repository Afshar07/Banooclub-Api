

import WebServiceRequest from '../../Api/WebServiceRequest'

class UpdateAdCategory extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('AdsCategory/Update');
  }
  setData(data){
    super.setRequestParamDataObj(data)
  }
}
export default UpdateAdCategory;


