import WebServiceRequest from '../../Api/WebServiceRequest'

class UsersUpdateByAdminRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Users/UpdateUserByAdmin')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default UsersUpdateByAdminRequest;
