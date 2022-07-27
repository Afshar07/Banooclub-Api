

import WebServiceRequest from '../../Api/WebServiceRequest'

class GroupGetAll extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('MessageGroup/GetAll')
    }
}
export default GroupGetAll;
