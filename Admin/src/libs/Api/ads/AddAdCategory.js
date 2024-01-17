

import WebServiceRequest from '../../Api/WebServiceRequest'

class AddAdCategory extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('AdsCategory/Create');
  }
  setData(data){
    super.setRequestParamDataObj(data)
  }
}
export default AddAdCategory;


